using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using TUnit.Assertions.Should.Extensions;
using static Specular.Scanning.Tests.Scanning.Scans;

namespace Specular.Scanning.Tests.Scanning;

public class TypeFilterTests
{
    internal static readonly ImmutableArray<string> AllTestAssemblyTypes =
    [
        "TestAssembly.GenericService",
        "TestAssembly.GenericServiceB",
        "TestAssembly.IGenericService`1",
        "TestAssembly.IKeyedService",
        "TestAssembly.IOther",
        "TestAssembly.IRequestHandler`2",
        "TestAssembly.IRequest`1",
        "TestAssembly.IService",
        "TestAssembly.IServiceB",
        "TestAssembly.IValidator",
        "TestAssembly.IValidator`1",
        "TestAssembly.KeyedService",
        "TestAssembly.Nested",
        "TestAssembly.Nested+GenericServiceA",
        "TestAssembly.Nested+MyRecord",
        "TestAssembly.Nested+ServiceA",
        "TestAssembly.Nested+Validator",
        "TestAssembly.Request",
        "TestAssembly.RequestHandler",
        "TestAssembly.Response",
        "TestAssembly.Service",
        "TestAssembly.ServiceB",
    ];

    internal static readonly ImmutableArray<string> TestAssemblyInterfaces =
    [
        "TestAssembly.IGenericService`1",
        "TestAssembly.IKeyedService",
        "TestAssembly.IOther",
        "TestAssembly.IRequestHandler`2",
        "TestAssembly.IRequest`1",
        "TestAssembly.IService",
        "TestAssembly.IServiceB",
        "TestAssembly.IValidator",
        "TestAssembly.IValidator`1",
    ];

    private static readonly ImmutableArray<string> serviceImplementations =
        ["TestAssembly.IService", "TestAssembly.Nested+ServiceA", "TestAssembly.Service", "TestAssembly.ServiceB"];

    private static readonly ImmutableArray<string> otherImplementations =
        ["TestAssembly.IOther", "TestAssembly.GenericService", "TestAssembly.GenericServiceB", "TestAssembly.Nested+GenericServiceA"];

    private static readonly ImmutableArray<string> registeredServices =
        ["TestAssembly.GenericService", "TestAssembly.GenericServiceB", "TestAssembly.RequestHandler"];

    private static TypeFilterData AssignableTo(bool include, params TypeReference[] types) =>
        TypeFilterData.All with { AssignableTo = Array(new AssignableToFilterData(include, Array(types))) };

    private static TypeFilterData Attributes(bool include, bool any, params TypeReference[] types) =>
        TypeFilterData.All with { Attributes = Array(new AttributeFilterData(include, any, Array(types))) };

    private static TypeFilterData Names(bool include, TextDirectionFilter filter, params string[] names) =>
        TypeFilterData.All with { Names = Array(new NameFilterData(include, filter, Array(names))) };

    private static TypeFilterData Kinds(bool include, params TypeKind[] kinds) =>
        TypeFilterData.All with { TypeKinds = Array(new TypeKindFilterData(include, Array(kinds))) };

    private static TypeFilterData Infos(bool include, params TypeInfoFilter[] infos) =>
        TypeFilterData.All with { TypeInfos = Array(new TypeInfoFilterData(include, Array(infos))) };

    private static TypeFilterData Namespaces(NamespaceFilter filter, params string[] namespaces) =>
        TypeFilterData.All with { Namespaces = Array(new NamespaceFilterData(filter, Array(namespaces))) };

    private static ImmutableArray<string> Except(ImmutableArray<string> all, ImmutableArray<string> excluded) => [.. all.Except(excluded)];

    [Test]
    public async Task No_clauses_return_every_type_including_nested_and_private_ones() => await TestAssemblyTypes(TypeFilterData.All).Should().BeEquivalentTo(AllTestAssemblyTypes);

    [Test]
    public async Task Public_only_keeps_types_declared_public()
    {
        var result = TestAssemblyTypes(TypeFilterData.All with { PublicOnly = true });

        await result.Should()
                    .BeEquivalentTo(
                         Except(
                             AllTestAssemblyTypes,
                             [
                                 "TestAssembly.GenericService",
                                 "TestAssembly.Nested+MyRecord",
                                 "TestAssembly.Nested+Validator",
                                 "TestAssembly.Request",
                                 "TestAssembly.RequestHandler",
                                 "TestAssembly.Response",
                                 "TestAssembly.ServiceB",
                             ]
                         )
                     );
    }

    [Test]
    public async Task Assignable_to_includes_the_type_itself_and_its_implementations() => await TestAssemblyTypes(AssignableTo(true, Type("TestAssembly.IService"))).Should().BeEquivalentTo(serviceImplementations);

    [Test]
    public async Task Not_assignable_to_excludes_the_implementations()
    {
        await TestAssemblyTypes(AssignableTo(false, Type("TestAssembly.IService")))
             .Should()
             .BeEquivalentTo(Except(AllTestAssemblyTypes, serviceImplementations));
    }

    [Test]
    public async Task Assignable_to_any_and_none()
    {
        var any = TestAssemblyTypes(AssignableTo(true, Type("TestAssembly.IService"), Type("TestAssembly.IOther")));
        var none = TestAssemblyTypes(AssignableTo(false, Type("TestAssembly.IService"), Type("TestAssembly.IOther")));

        await any.Should().BeEquivalentTo([.. serviceImplementations, .. otherImplementations]);
        await none.Should().BeEquivalentTo(Except(AllTestAssemblyTypes, [.. serviceImplementations, .. otherImplementations]));
    }

    [Test]
    public async Task An_unbound_generic_matches_every_closed_implementation()
    {
        await TestAssemblyTypes(AssignableTo(true, Type("TestAssembly.IGenericService`1", unboundGeneric: true)))
             .Should()
             .BeEquivalentTo(
                  [
                      "TestAssembly.GenericService",
                      "TestAssembly.GenericServiceB",
                      "TestAssembly.IGenericService`1",
                      "TestAssembly.Nested+GenericServiceA",
                  ]
              );
    }

    [Test]
    public async Task A_generic_definition_that_is_not_unbound_only_matches_itself() => await TestAssemblyTypes(AssignableTo(true, Type("TestAssembly.IGenericService`1"))).Should().BeEquivalentTo(["TestAssembly.IGenericService`1"]);

    [Test]
    public async Task A_reference_without_an_assembly_resolves_through_the_compilation() => await TestAssemblyTypes(AssignableTo(true, Type("TestAssembly.IService", assemblyName: ""))).Should().BeEquivalentTo(serviceImplementations);

    [Test]
    public async Task A_reference_naming_the_wrong_assembly_falls_back_to_the_compilation() => await TestAssemblyTypes(AssignableTo(true, Type("TestAssembly.IService", assemblyName: "Specular"))).Should().BeEquivalentTo(serviceImplementations);

    [Test]
    public async Task An_unresolvable_reference_drops_its_clause_like_Specular()
    {
        var single = TestAssemblyTypes(AssignableTo(true, Type("TestAssembly.IMissing")));
        var partlyResolved = TestAssemblyTypes(AssignableTo(true, Type("TestAssembly.IMissing"), Type("TestAssembly.IService")));
        var attribute = TestAssemblyTypes(Attributes(true, false, Type("Specular.MissingAttribute", "Specular")));

        await single.Should().BeEquivalentTo(AllTestAssemblyTypes);
        await partlyResolved.Should().BeEquivalentTo(serviceImplementations);
        await attribute.Should().BeEquivalentTo(AllTestAssemblyTypes);
    }

    [Test]
    public async Task With_and_without_an_attribute_symbol()
    {
        var attribute = Type("Specular.ServiceRegistrationAttribute", "Specular");

        await TestAssemblyTypes(Attributes(true, false, attribute)).Should().BeEquivalentTo(registeredServices);
        await TestAssemblyTypes(Attributes(false, false, attribute)).Should().BeEquivalentTo(Except(AllTestAssemblyTypes, registeredServices));
    }

    [Test]
    public async Task With_any_attribute_compares_name_and_namespace_so_an_unbound_generic_matches_closed_usages()
    {
        var compilation = ScanCompilation.Create(
            """
            namespace Tags
            {
                [System.AttributeUsage(System.AttributeTargets.Class)]
                public sealed class TagAttribute<T> : System.Attribute;

                [Tag<int>]
                public class Tagged;

                public class Untagged;
            }
            """
        );
        var tag = Type("Tags.TagAttribute`1", ScanCompilation.ProjectAssemblyName, unboundGeneric: true);

        await ProjectTypes(compilation, Attributes(true, true, tag)).Should().BeEquivalentTo(["Tags.Tagged"]);
        await ProjectTypes(compilation, Attributes(true, false, tag)).Should().BeEmpty();
        await ProjectTypes(compilation, Attributes(false, true, tag)).Should().BeEquivalentTo(["Tags.TagAttribute`1", "Tags.Untagged"]);
    }

    [Test]
    public async Task Attribute_names_compare_full_metadata_names()
    {
        var include = TypeFilterData.All with
        {
            AttributeNames = Array(new AttributeNameFilterData(true, Array("Specular.ServiceRegistrationAttribute", "Not.ThereAttribute"))),
        };
        var exclude = TypeFilterData.All with
        {
            AttributeNames = Array(new AttributeNameFilterData(false, Array("Specular.ServiceRegistrationAttribute"))),
        };

        await TestAssemblyTypes(include).Should().BeEquivalentTo(registeredServices);
        await TestAssemblyTypes(exclude).Should().BeEquivalentTo(Except(AllTestAssemblyTypes, registeredServices));
    }

    [Test]
    public async Task Namespace_filters_compare_the_containing_namespace()
    {
        var compilation = ScanCompilation.Create(
            """
            namespace A { public class InA; }
            namespace A.B { public class InAB; }
            namespace AB { public class InABWithoutDot; }
            public class InGlobal;
            """
        );

        await ProjectTypes(compilation, Namespaces(NamespaceFilter.Exact, "A")).Should().BeEquivalentTo(["A.InA"]);
        // Specular compares with StartsWith, so "A" also takes in "AB".
        await ProjectTypes(compilation, Namespaces(NamespaceFilter.In, "A")).Should().BeEquivalentTo(["A.InA", "A.B.InAB", "AB.InABWithoutDot"]);
        await ProjectTypes(compilation, Namespaces(NamespaceFilter.NotIn, "A")).Should().BeEquivalentTo(["InGlobal"]);
    }

    [Test]
    public async Task Name_filters_compare_the_simple_name()
    {
        await TestAssemblyTypes(Names(true, TextDirectionFilter.StartsWith, "IRequest"))
             .Should()
             .BeEquivalentTo(["TestAssembly.IRequestHandler`2", "TestAssembly.IRequest`1"]);
        await TestAssemblyTypes(Names(true, TextDirectionFilter.EndsWith, "ServiceB"))
             .Should()
             .BeEquivalentTo(["TestAssembly.GenericServiceB", "TestAssembly.IServiceB", "TestAssembly.ServiceB"]);
        await TestAssemblyTypes(Names(true, TextDirectionFilter.Contains, "Validator", "Keyed"))
             .Should()
             .BeEquivalentTo(
                  [
                      "TestAssembly.IKeyedService",
                      "TestAssembly.IValidator",
                      "TestAssembly.IValidator`1",
                      "TestAssembly.KeyedService",
                      "TestAssembly.Nested+Validator",
                  ]
              );
        await TestAssemblyTypes(Names(false, TextDirectionFilter.Contains, "Service"))
             .Should()
             .BeEquivalentTo(
                  [
                      "TestAssembly.IOther",
                      "TestAssembly.IRequestHandler`2",
                      "TestAssembly.IRequest`1",
                      "TestAssembly.IValidator",
                      "TestAssembly.IValidator`1",
                      "TestAssembly.Nested",
                      "TestAssembly.Nested+MyRecord",
                      "TestAssembly.Nested+Validator",
                      "TestAssembly.Request",
                      "TestAssembly.RequestHandler",
                      "TestAssembly.Response",
                  ]
              );
        await TestAssemblyTypes(Names(false, TextDirectionFilter.StartsWith, "I", "N", "R", "S", "G", "K"))
             .Should()
             .BeEquivalentTo(["TestAssembly.Nested+MyRecord", "TestAssembly.Nested+Validator"]);
        await TestAssemblyTypes(Names(false, TextDirectionFilter.EndsWith, "Service", "B"))
             .Should()
             .BeEquivalentTo(
                  [
                      "TestAssembly.IOther",
                      "TestAssembly.IRequestHandler`2",
                      "TestAssembly.IRequest`1",
                      "TestAssembly.IValidator",
                      "TestAssembly.IValidator`1",
                      "TestAssembly.Nested",
                      "TestAssembly.Nested+GenericServiceA",
                      "TestAssembly.Nested+MyRecord",
                      "TestAssembly.Nested+ServiceA",
                      "TestAssembly.Nested+Validator",
                      "TestAssembly.Request",
                      "TestAssembly.RequestHandler",
                      "TestAssembly.Response",
                  ]
              );
    }

    [Test]
    public async Task Kind_filters()
    {
        await TestAssemblyTypes(Kinds(true, TypeKind.Interface)).Should().BeEquivalentTo(TestAssemblyInterfaces);
        await TestAssemblyTypes(Kinds(false, TypeKind.Interface)).Should().BeEquivalentTo(Except(AllTestAssemblyTypes, TestAssemblyInterfaces));
    }

    [Test]
    public async Task Info_filters()
    {
        await TestAssemblyTypes(Infos(true, TypeInfoFilter.Static)).Should().BeEquivalentTo(["TestAssembly.Nested"]);
        await TestAssemblyTypes(Infos(false, TypeInfoFilter.Static)).Should().BeEquivalentTo(Except(AllTestAssemblyTypes, ["TestAssembly.Nested"]));
        // Roslyn reports a static class as neither abstract nor sealed.
        await TestAssemblyTypes(Infos(true, TypeInfoFilter.Abstract)).Should().BeEquivalentTo(TestAssemblyInterfaces);
        await TestAssemblyTypes(Infos(true, TypeInfoFilter.Sealed, TypeInfoFilter.ValueType)).Should().BeEmpty();
        await TestAssemblyTypes(Infos(true, TypeInfoFilter.Visible)).Should().BeEquivalentTo(TestAssemblyTypes(TypeFilterData.All with { PublicOnly = true }));
        await TestAssemblyTypes(Infos(true, TypeInfoFilter.GenericType))
             .Should()
             .BeEquivalentTo(
                  ["TestAssembly.IGenericService`1", "TestAssembly.IRequestHandler`2", "TestAssembly.IRequest`1", "TestAssembly.IValidator`1"]
              );
    }

    [Test]
    public async Task An_unknown_type_info_never_matches_instead_of_throwing()
    {
        await TestAssemblyTypes(Infos(true, TypeInfoFilter.Unknown)).Should().BeEmpty();
        await TestAssemblyTypes(Infos(false, TypeInfoFilter.Unknown)).Should().BeEquivalentTo(AllTestAssemblyTypes);
    }

    [Test]
    public async Task Every_clause_must_match()
    {
        var types = AssignableTo(true, Type("TestAssembly.IService")) with
        {
            PublicOnly = true,
            TypeKinds = Array(new TypeKindFilterData(false, Array(TypeKind.Interface))),
        };

        await TestAssemblyTypes(types).Should().BeEquivalentTo(["TestAssembly.Nested+ServiceA", "TestAssembly.Service"]);
    }

    [Test]
    public async Task Custom_filters_run_last_and_only_for_types_that_passed_every_other_clause()
    {
        var custom = new RecordingNameFilter("short-names", static type => type.Name.Length <= 6);
        var types = Kinds(true, TypeKind.Class) with { CustomFilters = Array<CustomTypeFilter>(custom) };

        var result = TestAssemblyTypes(types);

        await result.Should().BeEquivalentTo(["TestAssembly.Nested"]);
        await custom.Visited.Should().Contain("ServiceB");
        await custom.Visited.Should().NotContain("IService");
    }

    [Test]
    public async Task Compiler_generated_types_that_cannot_be_referenced_by_name_are_skipped()
    {
        var library = ScanCompilation.EmitReference(
            """
            namespace Lib
            {
                public class WithLambda
                {
                    public System.Func<int, int> Increment = x => x + 1;
                    public System.Collections.Generic.IEnumerable<int> Numbers() { yield return 1; }
                }
            }
            """,
            "LambdaLib"
        );
        var result = Scanner.Scan(ScanCompilation.Create(additionalReferences: library), Types(FromAssemblies("LambdaLib")));

        await TypeNames(result).Should().Contain("Lib.WithLambda");
        await TypeNames(result).Where(static z => z.Contains('<', StringComparison.Ordinal)).Should().BeEmpty();
    }
}
