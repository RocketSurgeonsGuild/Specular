using Microsoft.CodeAnalysis;
using Specular.Scanning.Tests.Scanning;
using TUnit.Assertions.Should.Extensions;
using static Specular.Scanning.Tests.Scanning.Scans;

namespace Specular.Scanning.Tests.Results;

public class ScannedTypeTests
{
    private const string GenericsSource = """
                                          public class TopLevel;
                                          namespace Ns
                                          {
                                              public class Plain;
                                              public interface IHandler<TRequest, TResponse>;
                                              public class Outer<T>
                                              {
                                                  public class Inner;
                                                  public class Pair<TFirst, TSecond>;
                                              }
                                              public class @class;
                                          }
                                          """;

    [Test]
    [Arguments("TopLevel", "global::TopLevel")]
    [Arguments("Ns.Plain", "global::Ns.Plain")]
    [Arguments("Ns.IHandler`2", "global::Ns.IHandler<TRequest, TResponse>")]
    [Arguments("Ns.Outer`1", "global::Ns.Outer<T>")]
    [Arguments("Ns.Outer`1+Inner", "global::Ns.Outer<T>.Inner")]
    [Arguments("Ns.Outer`1+Pair`2", "global::Ns.Outer<T>.Pair<TFirst, TSecond>")]
    [Arguments("Ns.class", "global::Ns.@class")]
    public async Task Referenced_types_carry_a_csharp_ready_fully_qualified_name(string metadataName, string fullyQualifiedName)
    {
        var library = ScanCompilation.EmitReference(GenericsSource, "GenericsLib");

        var result = Scanner.Scan(ScanCompilation.Create(additionalReferences: library), Types(FromAssemblies("GenericsLib")));

        await result.Types.Single(z => z.MetadataName == metadataName).FullyQualifiedName.Should().BeEqualTo(fullyQualifiedName);
    }

    [Test]
    public async Task The_compilations_own_types_carry_a_fully_qualified_name()
    {
        var compilation = ScanCompilation.Create(GenericsSource);

        var result = Scanner.Scan(compilation, Types(FromAssemblies(ScanCompilation.ProjectAssemblyName)));

        await result.Types.Single(static z => z.MetadataName == "Ns.Outer`1+Inner").FullyQualifiedName.Should().BeEqualTo("global::Ns.Outer<T>.Inner");
    }

    [Test]
    [Arguments("System.Object", "global::System.Object")]
    [Arguments("System.Int32", "global::System.Int32")]
    [Arguments("System.Nullable`1", "global::System.Nullable<T>")]
    [Arguments("System.ValueTuple`2", "global::System.ValueTuple<T1, T2>")]
    public async Task Special_types_are_named_like_any_other_type(string metadataName, string fullyQualifiedName)
    {
        // Keywords and the T? and (T1, T2) shorthands would break the global:: prefix and the unbound typeof(...<,>) form.
        var corlib = typeof(object).Assembly.GetName().Name!;
        var request = Types(
            FromAssemblies(corlib) with { IncludeSystemAssemblies = true },
            TypeFilterData.All with { Names = Array(new NameFilterData(true, TextDirectionFilter.StartsWith, Array("Object", "Int32", "Nullable", "ValueTuple"))) }
        );

        var result = Scanner.Scan(ScanCompilation.Create(), request);

        await result.Types.Single(z => z.MetadataName == metadataName).FullyQualifiedName.Should().BeEqualTo(fullyQualifiedName);
    }

    [Test]
    public async Task The_positional_constructor_leaves_it_empty()
    {
        var type = new ScannedType("Lib", "Lib.A", TypeKind.Class, Accessibility.Public, true, 0);

        await type.FullyQualifiedName.Should().BeEqualTo(string.Empty);
    }

    [Test]
    public async Task It_takes_part_in_equality()
    {
        var type = new ScannedType("Lib", "Lib.A", TypeKind.Class, Accessibility.Public, true, 0) { FullyQualifiedName = "global::Lib.A" };

        await ( type == type with { FullyQualifiedName = "global::Lib.A" } ).Should().BeTrue();
        await ( type == type with { FullyQualifiedName = "global::Lib.B" } ).Should().BeFalse();
        await ( type.GetHashCode() == ( type with { FullyQualifiedName = "global::Lib.A" } ).GetHashCode() ).Should().BeTrue();
    }
}
