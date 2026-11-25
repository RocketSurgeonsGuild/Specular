using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using TUnit.Assertions.Should.Extensions;
using static Specular.Scanning.Tests.Scanning.Scans;

namespace Specular.Scanning.Tests.Scanning;

public class ClosedGenericScanTests
{
    private const string Source = """
        namespace Handlers
        {
            public interface IHandler<T>;
            public interface IVariant<out T>;

            [System.AttributeUsage(System.AttributeTargets.Class)]
            public sealed class TagAttribute<T> : System.Attribute;

            public class Outer<T>
            {
                public class Inner;
            }

            public class IntHandler : IHandler<int>;
            public class StringHandler : IHandler<string>;
            public class BothHandler : IHandler<int>, IHandler<string>;
            public class OpenHandler<T> : IHandler<T>;
            public class DerivedIntHandler : OpenHandler<int>;
            public class NestedArgumentHandler : IHandler<Outer<int>.Inner>;
            public class ClosedArgumentHandler : IHandler<IHandler<int>>;
            public class StringVariant : IVariant<string>;
            public class ObjectVariant : IVariant<object>;

            [Tag<int>]
            public class TaggedInt;

            [Tag<string>]
            public class TaggedString;
        }
        """;

    private static readonly Compilation compilation = ScanCompilation.Create(Source);

    private static readonly TypeReference @int = new("", "System.Int32", false);
    private static readonly TypeReference @string = new("", "System.String", false);
    private static readonly TypeReference @object = new("", "System.Object", false);

    private static TypeReference Project(string metadataName, params TypeReference[] typeArguments) =>
        new(ScanCompilation.ProjectAssemblyName, metadataName, false) { TypeArguments = Array(typeArguments) };

    private static ImmutableArray<string> Assignable(bool include, params TypeReference[] types) =>
        ProjectTypes(compilation, TypeFilterData.All with { AssignableTo = Array(new AssignableToFilterData(include, Array(types))) });

    private static ImmutableArray<string> WithAttributes(bool include, bool any, params TypeReference[] attributes) =>
        ProjectTypes(compilation, TypeFilterData.All with { Attributes = Array(new AttributeFilterData(include, any, Array(attributes))) });

    private static ScanResult ScanProject(TypeFilterData types) =>
        Scanner.Scan(compilation, Types(FromAssemblies(ScanCompilation.ProjectAssemblyName), types));

    [Test]
    public async Task A_closed_generic_matches_only_types_assignable_to_that_closed_type()
    {
        await Assignable(true, Project("Handlers.IHandler`1", @int))
             .Should()
             .BeEquivalentTo(["Handlers.BothHandler", "Handlers.DerivedIntHandler", "Handlers.IntHandler"]);
        await Assignable(true, Project("Handlers.IHandler`1", @string)).Should().BeEquivalentTo(["Handlers.BothHandler", "Handlers.StringHandler"]);
    }

    [Test]
    public async Task Not_assignable_to_a_closed_generic_keeps_other_closed_forms_and_open_generics()
    {
        var result = Assignable(false, Project("Handlers.IHandler`1", @int));

        await result.Should().Contain("Handlers.StringHandler");
        await result.Should().Contain("Handlers.OpenHandler`1");
        await result.Should().Contain("Handlers.IHandler`1");
        await result.Should().NotContain("Handlers.IntHandler");
        await result.Should().NotContain("Handlers.BothHandler");
        await result.Should().NotContain("Handlers.DerivedIntHandler");
    }

    [Test]
    public async Task Assignable_to_any_of_two_closed_forms()
    {
        await Assignable(true, Project("Handlers.IHandler`1", @int), Project("Handlers.IHandler`1", @string))
             .Should()
             .BeEquivalentTo(["Handlers.BothHandler", "Handlers.DerivedIntHandler", "Handlers.IntHandler", "Handlers.StringHandler"]);
    }

    [Test]
    public async Task Variance_applies_because_assignability_is_the_compilers_implicit_conversion()
    {
        await Assignable(true, Project("Handlers.IVariant`1", @object)).Should().BeEquivalentTo(["Handlers.ObjectVariant", "Handlers.StringVariant"]);
        await Assignable(true, Project("Handlers.IVariant`1", @string)).Should().BeEquivalentTo(["Handlers.StringVariant"]);
    }

    [Test]
    public async Task Type_arguments_can_be_nested_and_closed_generics()
    {
        await Assignable(true, Project("Handlers.IHandler`1", Project("Handlers.Outer`1+Inner", @int)))
             .Should()
             .BeEquivalentTo(["Handlers.NestedArgumentHandler"]);
        await Assignable(true, Project("Handlers.IHandler`1", Project("Handlers.IHandler`1", @int)))
             .Should()
             .BeEquivalentTo(["Handlers.ClosedArgumentHandler"]);
    }

    [Test]
    public async Task A_generic_definition_still_matches_every_closed_form()
    {
        await Assignable(true, new TypeReference(ScanCompilation.ProjectAssemblyName, "Handlers.IHandler`1", true))
             .Should()
             .BeEquivalentTo(
                  [
                      "Handlers.BothHandler",
                      "Handlers.ClosedArgumentHandler",
                      "Handlers.DerivedIntHandler",
                      "Handlers.IHandler`1",
                      "Handlers.IntHandler",
                      "Handlers.NestedArgumentHandler",
                      "Handlers.OpenHandler`1",
                      "Handlers.StringHandler",
                  ]
              );
    }

    [Test]
    public async Task A_closed_attribute_type_matches_exactly_including_with_any_attribute()
    {
        var tagInt = Project("Handlers.TagAttribute`1", @int);
        var unboundTag = new TypeReference(ScanCompilation.ProjectAssemblyName, "Handlers.TagAttribute`1", true);

        await WithAttributes(true, false, tagInt).Should().BeEquivalentTo(["Handlers.TaggedInt"]);
        await WithAttributes(true, true, tagInt).Should().BeEquivalentTo(["Handlers.TaggedInt"]);
        await WithAttributes(true, true, unboundTag).Should().BeEquivalentTo(["Handlers.TaggedInt", "Handlers.TaggedString"]);
        await WithAttributes(false, false, tagInt).Should().Contain("Handlers.TaggedString");
        await WithAttributes(false, false, tagInt).Should().NotContain("Handlers.TaggedInt");
    }

    [Test]
    public async Task The_selector_and_the_scanner_round_trip_a_closed_generic_symbol()
    {
        var handler = compilation.GetTypeByMetadataName("Handlers.IHandler`1")!;
        var closed = handler.Construct(compilation.GetSpecialType(SpecialType.System_Int32));
        var request = ScanRequest.Build(compilation, s => s.FromAssemblies().GetTypes(f => f.AssignableTo(closed).InNamespaces("Handlers")));

        var result = Scanner.Scan(compilation, request);

        await TypeNames(result).Should().BeEquivalentTo(["Handlers.BothHandler", "Handlers.DerivedIntHandler", "Handlers.IntHandler"]);
        await result.UnresolvedTypes.Count.Should().BeEqualTo(0);
    }

    public static IEnumerable<Func<UnresolvableClosedReference>> UnresolvableClosedReferences()
    {
        yield return () => new("missing type argument", Project("Handlers.IHandler`1", new TypeReference("", "Missing.Type", false)));
        yield return () => new("too many type arguments", Project("Handlers.IHandler`1", @int, @int));
        yield return () => new("type arguments on a non-generic type", Project("Handlers.IntHandler", @int));
        yield return () => new("type arguments on an unbound generic", Project("Handlers.IHandler`1", @int) with { UnboundGeneric = true });
        yield return () => new("an open generic definition as an argument", Project("Handlers.IHandler`1", Project("Handlers.IHandler`1")));
        yield return () => new("an unbound generic as an argument", Project("Handlers.IHandler`1", Project("Handlers.IHandler`1") with { UnboundGeneric = true }));
        yield return () => new("a missing definition", Project("Handlers.IMissing`1", @int));
    }

    [Test]
    [MethodDataSource(nameof(UnresolvableClosedReferences))]
    public async Task A_closed_reference_that_cannot_be_constructed_is_unresolved(UnresolvableClosedReference unresolvable)
    {
        var result = ScanProject(TypeFilterData.All with { AssignableTo = Array(new AssignableToFilterData(true, Array(unresolvable.Reference))) });

        await result.Types.Count.Should().BeEqualTo(0);
        await result.UnresolvedTypes.Should().BeEqualTo(Array(unresolvable.Reference));
    }

    public sealed record UnresolvableClosedReference(string Name, TypeReference Reference)
    {
        public override string ToString() => Name;
    }
}
