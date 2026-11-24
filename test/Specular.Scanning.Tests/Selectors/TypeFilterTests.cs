using Microsoft.CodeAnalysis;
using TUnit.Assertions.Should.Extensions;
using static Specular.Scanning.Tests.Selectors.SelectorCompilation;
using static Specular.Scanning.Tests.Selectors.SelectorTestData;

namespace Specular.Scanning.Tests.Selectors;

public class TypeFilterTests
{
    public static IEnumerable<Func<FilterCase>> FilterCases()
    {
        var all = TypeFilterData.All;
        var handler = Library("Library.Contracts.IHandler");
        var openHandler = Library("Library.Contracts.IHandler`1", true);
        var marker = App("App.Handlers.MarkerAttribute");
        var other = App("App.Handlers.OtherAttribute");

        yield return () => new(
            "AssignableTo(symbol)",
            f => f.AssignableTo(Type("Library.Contracts.IHandler")),
            all with { AssignableTo = Array(new AssignableToFilterData(true, Array(handler))) }
        );
        yield return () => new(
            "AssignableTo(unbound generic)",
            f => f.AssignableTo(Unbound("Library.Contracts.IHandler`1")),
            all with { AssignableTo = Array(new AssignableToFilterData(true, Array(openHandler))) }
        );
        yield return () => new(
            "AssignableTo(generic definition)",
            f => f.AssignableTo(Type("Library.Contracts.IHandler`1")),
            all with { AssignableTo = Array(new AssignableToFilterData(true, Array(openHandler))) }
        );
        yield return () => new(
            "AssignableTo(closed generic)",
            f => f.AssignableTo(Type("Library.Contracts.IHandler`1").Construct(SelectorCompilation.Compilation.GetSpecialType(SpecialType.System_Int32))),
            all with { AssignableTo = Array(new AssignableToFilterData(true, Array(Library("Library.Contracts.IHandler`1")))) }
        );
        yield return () => new(
            "AssignableTo(nested in generic)",
            f => f.AssignableTo(Unbound("App.Handlers.Outer`1+INested`1")),
            all with { AssignableTo = Array(new AssignableToFilterData(true, Array(App("App.Handlers.Outer`1+INested`1", true)))) }
        );
        yield return () => new(
            "AssignableTo(type nested in generic)",
            f => f.AssignableTo(Type("App.Handlers.Outer`1+Inner")),
            all with { AssignableTo = Array(new AssignableToFilterData(true, Array(App("App.Handlers.Outer`1+Inner")))) }
        );
        yield return () => new(
            "AssignableTo(string)",
            f => f.AssignableTo("Library.Contracts.IHandler"),
            all with { AssignableTo = Array(new AssignableToFilterData(true, Array(new TypeReference("", "Library.Contracts.IHandler", false)))) }
        );
        yield return () => new(
            "AssignableTo(generic string)",
            f => f.AssignableTo("Library.Contracts.IHandler`1"),
            all with { AssignableTo = Array(new AssignableToFilterData(true, Array(new TypeReference("", "Library.Contracts.IHandler`1", true)))) }
        );
        yield return () => new(
            "AssignableTo(string nested in generic)",
            f => f.AssignableTo("App.Handlers.Outer`1+Inner"),
            all with { AssignableTo = Array(new AssignableToFilterData(true, Array(new TypeReference("", "App.Handlers.Outer`1+Inner", false)))) }
        );
        yield return () => new(
            "AssignableToAny",
            f => f.AssignableToAny(Type("Library.Contracts.IHandler"), Unbound("Library.Contracts.IHandler`1")),
            all with { AssignableTo = Array(new AssignableToFilterData(true, Array(handler, openHandler))) }
        );
        yield return () => new(
            "NotAssignableTo(symbol)",
            f => f.NotAssignableTo(Type("Library.Contracts.IHandler")),
            all with { AssignableTo = Array(new AssignableToFilterData(false, Array(handler))) }
        );
        yield return () => new(
            "NotAssignableTo(string)",
            f => f.NotAssignableTo("Library.Contracts.IHandler`1"),
            all with { AssignableTo = Array(new AssignableToFilterData(false, Array(new TypeReference("", "Library.Contracts.IHandler`1", true)))) }
        );
        yield return () => new(
            "NotAssignableToAny",
            f => f.NotAssignableToAny(Type("Library.Contracts.IHandler"), Unbound("Library.Contracts.IHandler`1")),
            all with { AssignableTo = Array(new AssignableToFilterData(false, Array(handler, openHandler))) }
        );
        yield return () => new(
            "EndsWith",
            f => f.EndsWith("Handler", "Service"),
            all with { Names = Array(new NameFilterData(true, TextDirectionFilter.EndsWith, Array("Handler", "Service"))) }
        );
        yield return () => new(
            "NotEndsWith",
            f => f.NotEndsWith("Handler"),
            all with { Names = Array(new NameFilterData(false, TextDirectionFilter.EndsWith, Array("Handler"))) }
        );
        yield return () => new(
            "StartsWith",
            f => f.StartsWith("Deep"),
            all with { Names = Array(new NameFilterData(true, TextDirectionFilter.StartsWith, Array("Deep"))) }
        );
        yield return () => new(
            "NotStartsWith",
            f => f.NotStartsWith("Deep", "Global"),
            all with { Names = Array(new NameFilterData(false, TextDirectionFilter.StartsWith, Array("Deep", "Global"))) }
        );
        yield return () => new(
            "Contains",
            f => f.Contains("andl"),
            all with { Names = Array(new NameFilterData(true, TextDirectionFilter.Contains, Array("andl"))) }
        );
        yield return () => new(
            "NotContains",
            f => f.NotContains("andl"),
            all with { Names = Array(new NameFilterData(false, TextDirectionFilter.Contains, Array("andl"))) }
        );
        yield return () => new(
            "InExactNamespaceOf",
            f => f.InExactNamespaceOf(Type("App.Handlers.Handler"), Type("App.Services.Service")),
            all with { Namespaces = Array(new NamespaceFilterData(NamespaceFilter.Exact, Array("App.Handlers", "App.Services"))) }
        );
        yield return () => new(
            "InExactNamespaces",
            f => f.InExactNamespaces("App.Handlers", "App.Services"),
            all with { Namespaces = Array(new NamespaceFilterData(NamespaceFilter.Exact, Array("App.Handlers", "App.Services"))) }
        );
        yield return () => new(
            "InNamespaceOf(nested type)",
            f => f.InNamespaceOf(Type("App.Handlers.Outer`1+Inner")),
            all with { Namespaces = Array(new NamespaceFilterData(NamespaceFilter.In, Array("App.Handlers"))) }
        );
        yield return () => new(
            "InNamespaces",
            f => f.InNamespaces("App.Handlers"),
            all with { Namespaces = Array(new NamespaceFilterData(NamespaceFilter.In, Array("App.Handlers"))) }
        );
        yield return () => new(
            "NotInNamespaceOf",
            f => f.NotInNamespaceOf(Type("App.Handlers.Deep.DeepHandler")),
            all with { Namespaces = Array(new NamespaceFilterData(NamespaceFilter.NotIn, Array("App.Handlers.Deep"))) }
        );
        yield return () => new(
            "NotInNamespaces",
            f => f.NotInNamespaces("App.Handlers.Deep"),
            all with { Namespaces = Array(new NamespaceFilterData(NamespaceFilter.NotIn, Array("App.Handlers.Deep"))) }
        );
        yield return () => new(
            "InNamespaceOf(global namespace)",
            f => f.InNamespaceOf(Type("GlobalType")),
            all with { Namespaces = Array(new NamespaceFilterData(NamespaceFilter.In, Array("<global namespace>"))) }
        );
        yield return () => new(
            "WithAttribute(symbol)",
            f => f.WithAttribute(Type("App.Handlers.MarkerAttribute")),
            all with { Attributes = Array(new AttributeFilterData(true, false, Array(marker))) }
        );
        yield return () => new(
            "WithAttribute(string)",
            f => f.WithAttribute("App.Handlers.MarkerAttribute"),
            all with { AttributeNames = Array(new AttributeNameFilterData(true, Array("App.Handlers.MarkerAttribute"))) }
        );
        yield return () => new(
            "WithAnyAttribute(symbols)",
            f => f.WithAnyAttribute(Type("App.Handlers.MarkerAttribute"), Type("App.Handlers.OtherAttribute")),
            all with { Attributes = Array(new AttributeFilterData(true, true, Array(marker, other))) }
        );
        yield return () => new(
            "WithAnyAttribute(strings)",
            f => f.WithAnyAttribute("App.Handlers.MarkerAttribute", "App.Handlers.OtherAttribute"),
            all with { AttributeNames = Array(new AttributeNameFilterData(true, Array("App.Handlers.MarkerAttribute", "App.Handlers.OtherAttribute"))) }
        );
        yield return () => new(
            "WithoutAttribute(symbol)",
            f => f.WithoutAttribute(Type("App.Handlers.MarkerAttribute")),
            all with { Attributes = Array(new AttributeFilterData(false, false, Array(marker))) }
        );
        yield return () => new(
            "WithoutAttribute(string)",
            f => f.WithoutAttribute("App.Handlers.MarkerAttribute"),
            all with { AttributeNames = Array(new AttributeNameFilterData(false, Array("App.Handlers.MarkerAttribute"))) }
        );
        yield return () => new(
            "KindOf",
            f => f.KindOf(TypeKind.Class, TypeKind.Interface),
            all with { TypeKinds = Array(new TypeKindFilterData(true, Array(TypeKind.Class, TypeKind.Interface))) }
        );
        yield return () => new(
            "NotKindOf",
            f => f.NotKindOf(TypeKind.Struct),
            all with { TypeKinds = Array(new TypeKindFilterData(false, Array(TypeKind.Struct))) }
        );
        yield return () => new(
            "InfoOf",
            f => f.InfoOf(TypeInfoFilter.Abstract, TypeInfoFilter.Sealed),
            all with { TypeInfos = Array(new TypeInfoFilterData(true, Array(TypeInfoFilter.Abstract, TypeInfoFilter.Sealed))) }
        );
        yield return () => new(
            "NotInfoOf",
            f => f.NotInfoOf(TypeInfoFilter.Static),
            all with { TypeInfos = Array(new TypeInfoFilterData(false, Array(TypeInfoFilter.Static))) }
        );
        yield return () => new(
            "Where",
            f => f.Where(new NameLengthFilter(7)),
            all with { CustomFilters = Array<CustomTypeFilter>(new NameLengthFilter(7)) }
        );
        yield return () => new(
            "repeated values in one clause",
            f => f.EndsWith("Handler", "Handler", "Service"),
            all with { Names = Array(new NameFilterData(true, TextDirectionFilter.EndsWith, Array("Handler", "Service"))) }
        );
    }

    [Test]
    [MethodDataSource(nameof(FilterCases))]
    public async Task Each_filter_method_maps_to_its_clause(FilterCase filterCase)
    {
        var request = Build(s => s.FromAssemblies().GetTypes(filterCase.Filter));

        await request.Kind.Should().BeEqualTo(ScanKind.Types);
        await request.Types.Should().BeEqualTo(filterCase.Expected);
    }

    [Test]
    public async Task Clauses_are_appended_in_call_order()
    {
        var types = Types(f => f
                              .EndsWith("Handler")
                              .AssignableTo("B")
                              .StartsWith("Deep")
                              .AssignableTo("A")
                              .InNamespaces("Second")
                              .InExactNamespaces("First")
        );

        await types.Should().BeEqualTo(
            TypeFilterData.All with
            {
                AssignableTo = Array(
                    new AssignableToFilterData(true, Array(new TypeReference("", "B", false))),
                    new AssignableToFilterData(true, Array(new TypeReference("", "A", false)))
                ),
                Names = Array(
                    new NameFilterData(true, TextDirectionFilter.EndsWith, Array("Handler")),
                    new NameFilterData(true, TextDirectionFilter.StartsWith, Array("Deep"))
                ),
                Namespaces = Array(
                    new NamespaceFilterData(NamespaceFilter.In, Array("Second")),
                    new NamespaceFilterData(NamespaceFilter.Exact, Array("First"))
                ),
            }
        );
    }

    [Test]
    public async Task Symbol_and_name_overloads_agree_on_the_type()
    {
        var bySymbol = Types(f => f.AssignableTo(Unbound("App.Handlers.Outer`1+INested`1"))).AssignableTo[0].Types[0];
        var byName = Types(f => f.AssignableTo("App.Handlers.Outer`1+INested`1")).AssignableTo[0].Types[0];

        await byName.Should().BeEqualTo(bySymbol with { AssemblyName = "" });
    }

    [Test]
    public async Task Namespace_symbol_and_name_overloads_are_equal()
    {
        var handler = Type("App.Handlers.Handler");

        await Types(f => f.InNamespaceOf(handler)).Should().BeEqualTo(Types(f => f.InNamespaces("App.Handlers")));
        await Types(f => f.InExactNamespaceOf(handler)).Should().BeEqualTo(Types(f => f.InExactNamespaces("App.Handlers")));
        await Types(f => f.NotInNamespaceOf(handler)).Should().BeEqualTo(Types(f => f.NotInNamespaces("App.Handlers")));
    }

    [Test]
    [Arguments(false)]
    [Arguments(true)]
    public async Task GetTypes_overloads_set_PublicOnly(bool publicOnly)
    {
        var expected = TypeFilterData.All with { PublicOnly = publicOnly };

        await Build(s => s.FromAssemblies().GetTypes(publicOnly)).Types.Should().BeEqualTo(expected);
        await Build(s => s.FromAssemblies().GetTypes(publicOnly, _ => { })).Types.Should().BeEqualTo(expected);
    }

    [Test]
    public async Task GetTypes_without_arguments_returns_every_type()
    {
        var request = Build(s => s.FromAssemblies().GetTypes());
        var withEmptyFilter = Build(s => s.FromAssemblies().GetTypes(_ => { }));

        await request.Should().BeEqualTo(new ScanRequest(ScanKind.Types, AssemblyFilterData.Empty with { AllAssemblies = true }, TypeFilterData.All));
        await withEmptyFilter.Should().BeEqualTo(request);
    }

    [Test]
    public async Task GetTypes_twice_throws()
    {
        await Act(() => Build(
                    s =>
                    {
                        var types = s.FromAssemblies();
                        types.GetTypes();
                        types.GetTypes(true);
                    }
                )
           ).Should().Throw<InvalidOperationException>();
        await Act(() => Build(s => s.FromAssemblies().GetTypes(_ => s.FromAssemblies().GetTypes()))).Should().Throw<InvalidOperationException>();
    }

    [Test]
    public async Task Filter_methods_reject_null_and_empty_arguments()
    {
        await Act(() => Types(f => f.AssignableTo((ITypeSymbol)null!))).Should().Throw<ArgumentNullException>();
        await Act(() => Types(f => f.AssignableTo(""))).Should().Throw<ArgumentException>();
        await Act(() => Types(f => f.AssignableToAny(Type("App.Handlers.Handler"), null!))).Should().Throw<ArgumentNullException>();
        await Act(() => Types(f => f.AssignableToAny(Type("App.Handlers.Handler"), [null!]))).Should().Throw<ArgumentException>();
        await Act(() => Types(f => f.EndsWith(null!))).Should().Throw<ArgumentNullException>();
        await Act(() => Types(f => f.EndsWith("Handler", ""))).Should().Throw<ArgumentException>();
        await Act(() => Types(f => f.InNamespaces(""))).Should().Throw<ArgumentException>();
        await Act(() => Types(f => f.InNamespaceOf(null!))).Should().Throw<ArgumentNullException>();
        await Act(() => Types(f => f.WithAttribute((string)null!))).Should().Throw<ArgumentNullException>();
        await Act(() => Types(f => f.WithAnyAttribute("A", [""]))).Should().Throw<ArgumentException>();
        await Act(() => Types(f => f.KindOf(TypeKind.Class, null!))).Should().Throw<ArgumentNullException>();
        await Act(() => Types(f => f.Where(null!))).Should().Throw<ArgumentNullException>();
    }

    [Test]
    public async Task Type_symbols_must_be_named_types()
    {
        var array = SelectorCompilation.Compilation.CreateArrayTypeSymbol(Type("App.Handlers.Handler"));

        await Act(() => Types(f => f.AssignableTo(array))).Should().Throw<ArgumentException>();
    }

    [Test]
    public async Task The_filter_cannot_be_used_after_GetTypes_returns()
    {
        ITypeSymbolFilter? captured = null;
        _ = Build(s => s.FromAssemblies().GetTypes(f => captured = f));

        await Act(() => captured!.EndsWith("Handler")).Should().Throw<InvalidOperationException>();
    }

    public sealed record FilterCase(string Name, Action<ITypeSymbolFilter> Filter, TypeFilterData Expected)
    {
        public override string ToString() => Name;
    }
}
