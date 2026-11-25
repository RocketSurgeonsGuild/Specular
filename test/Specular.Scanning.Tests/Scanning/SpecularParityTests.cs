using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using TUnit.Assertions.Should.Extensions;
using static Specular.Scanning.Tests.Scanning.Scans;

namespace Specular.Scanning.Tests.Scanning;

/// <summary>
///     Cases ported from <c>test/Specular.Tests/GetTypesTestsData.cs</c>; the expected TestAssembly types are the ones Specular's
///     verified scan report lists for the same selector.
/// </summary>
public class SpecularParityTests
{
    private const string EmbeddedAttribute = "Microsoft.CodeAnalysis.EmbeddedAttribute";

    private static readonly AssemblyFilterData notFromSpecular = AllAssemblies with { NotAssemblies = Array("Specular") };

    // .NotAssignableTo<ISpecularProvider>() ... .NotInNamespaces("JetBrains.Annotations", "Polyfills", "System").NotStartsWith("Polyfill")
    private static readonly TypeFilterData commonClauses = TypeFilterData.All with
    {
        AssignableTo = Array(new AssignableToFilterData(false, Array(Type("Specular.ISpecularProvider", "Specular")))),
        Namespaces = Array(new NamespaceFilterData(NamespaceFilter.NotIn, Array("JetBrains.Annotations", "Polyfills", "System"))),
        Names = Array(new NameFilterData(false, TextDirectionFilter.StartsWith, Array("Polyfill"))),
    };

    private static ImmutableArray<string> TestAssemblyMatches(AssemblyFilterData assemblies, TypeFilterData types)
    {
        var result = Scanner.Scan(ScanCompilation.Create(), Types(assemblies, types));
        return [.. result.Types.Where(static z => z.AssemblyName == "TestAssembly").Select(static z => z.MetadataName)];
    }

    [Test]
    public async Task Not_kind_of_delegate_or_class()
    {
        var types = commonClauses with { TypeKinds = Array(new TypeKindFilterData(false, Array(TypeKind.Delegate, TypeKind.Class))) };

        await TestAssemblyMatches(notFromSpecular, types).Should().BeEquivalentTo(TypeFilterTests.TestAssemblyInterfaces);
    }

    [Test]
    public async Task Info_of_visible()
    {
        var types = commonClauses with { TypeInfos = Array(new TypeInfoFilterData(true, Array(TypeInfoFilter.Visible))) };

        await TestAssemblyMatches(notFromSpecular, types)
             .Should()
             .BeEquivalentTo(
                  [
                      "TestAssembly.GenericServiceB",
                      .. TypeFilterTests.TestAssemblyInterfaces,
                      "TestAssembly.KeyedService",
                      "TestAssembly.Nested",
                      "TestAssembly.Nested+GenericServiceA",
                      "TestAssembly.Nested+ServiceA",
                      "TestAssembly.Service",
                  ]
              );
    }

    [Test]
    public async Task Info_of_static_and_not_static()
    {
        var isStatic = commonClauses with { TypeInfos = Array(new TypeInfoFilterData(true, Array(TypeInfoFilter.Static))) };
        var notStatic = commonClauses with { TypeInfos = Array(new TypeInfoFilterData(false, Array(TypeInfoFilter.Static))) };

        await TestAssemblyMatches(notFromSpecular, isStatic).Should().BeEquivalentTo(["TestAssembly.Nested"]);
        await TestAssemblyMatches(notFromSpecular, notStatic)
             .Should()
             .BeEquivalentTo([EmbeddedAttribute, .. TypeFilterTests.AllTestAssemblyTypes.Where(static z => z != "TestAssembly.Nested")]);
    }

    [Test]
    public async Task Info_of_generic_type()
    {
        var types = commonClauses with { TypeInfos = Array(new TypeInfoFilterData(true, Array(TypeInfoFilter.GenericType))) };

        await TestAssemblyMatches(notFromSpecular, types)
             .Should()
             .BeEquivalentTo(
                  ["TestAssembly.IGenericService`1", "TestAssembly.IRequestHandler`2", "TestAssembly.IRequest`1", "TestAssembly.IValidator`1"]
              );
    }

    [Test]
    public async Task Not_info_of_visible_and_not_assignable_to_attribute()
    {
        var types = commonClauses with
        {
            AssignableTo = Array(
                new AssignableToFilterData(false, Array(Type("Specular.ISpecularProvider", "Specular"))),
                new AssignableToFilterData(false, Array(Type("System.Attribute", "System.Runtime")))
            ),
            TypeInfos = Array(new TypeInfoFilterData(false, Array(TypeInfoFilter.Visible))),
        };

        await TestAssemblyMatches(notFromSpecular, types)
             .Should()
             .BeEquivalentTo(
                  [
                      "TestAssembly.GenericService",
                      "TestAssembly.Nested+MyRecord",
                      "TestAssembly.Nested+Validator",
                      "TestAssembly.Request",
                      "TestAssembly.RequestHandler",
                      "TestAssembly.Response",
                      "TestAssembly.ServiceB",
                  ]
              );
    }

    [Test]
    public async Task Dependencies_from_assembly_of_with_any_attribute()
    {
        var assemblies = AllAssemblies with { DependenciesOf = Array("Specular") };
        var types = TypeFilterData.All with
        {
            Attributes = Array(new AttributeFilterData(true, true, Array(Type("Specular.ServiceRegistrationAttribute", "Specular")))),
        };

        var result = Scanner.Scan(ScanCompilation.Create(), Types(assemblies, types));

        await AssemblyNames(result).Should().BeEquivalentTo(["TestAssembly", "TestProject"]);
        await TypeNames(result)
             .Should()
             .BeEquivalentTo(["TestAssembly.GenericService", "TestAssembly.GenericServiceB", "TestAssembly.RequestHandler"]);
    }
}
