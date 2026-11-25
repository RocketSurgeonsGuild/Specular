using Microsoft.CodeAnalysis;
using static Specular.Scanning.Tests.Selectors.SelectorCompilation;

namespace Specular.Scanning.Tests.Selectors;

// What the selector builds, as a snapshot per scenario, so a change to the request model shows up as a readable diff.
public class ScanRequestSnapshotTests
{
    private static INamedTypeSymbol Int32 => SelectorCompilation.Compilation.GetSpecialType(SpecialType.System_Int32);

    public static IEnumerable<Func<Scenario>> Scenarios()
    {
        yield return () => new("all-assemblies", static s => s.FromAssemblies());
        yield return () => new("entry-and-dependents-of-library", static s => s.EntryAssembly().DependenciesFromAssembly(LibraryAssembly));
        yield return () => new("all-but-library-with-system", static s => s.FromAssemblies().NotFromAssembly(LibraryAssembly).IncludeSystemAssemblies());
        yield return () => new("public-handlers", static s => s.FromAssemblies().GetTypes(true, f => f.AssignableTo(Type("Library.Contracts.IHandler"))));
        yield return () => new(
            "closed-generic-handlers",
            static s => s.FromAssemblies()
                         .GetTypes(
                              f => f
                                  .AssignableToAny(Type("Library.Contracts.IHandler`1").Construct(Int32), Type("Library.Contracts.IHandler`1").Construct(SelectorCompilation.Compilation.CreateArrayTypeSymbol(Int32)))
                                  .NotAssignableTo(Type("Library.Contracts.IHandler`1"))
                          )
        );
        yield return () => new(
            "attributes",
            static s => s.FromAssemblyOf(Type("App.Handlers.Handler"))
                         .GetTypes(
                              f => f
                                  .WithAttribute(Type("App.Handlers.MarkerAttribute"))
                                  .WithoutAttribute("App.Handlers.OtherAttribute")
                                  .WithAnyAttribute(Type("Library.Contracts.LibraryAttribute"), Type("Library.Contracts.TagAttribute`1"))
                          )
        );
        yield return () => new(
            "names-namespaces-kinds",
            static s => s.FromAssemblyOf(Type("App.Handlers.Handler"))
                         .GetTypes(
                              f => f
                                  .InNamespaces("App.Handlers")
                                  .NotInNamespaceOf(Type("App.Handlers.Deep.DeepHandler"))
                                  .EndsWith("Handler")
                                  .KindOf(TypeKind.Class)
                                  .NotInfoOf(TypeInfoFilter.Abstract, TypeInfoFilter.Static)
                          )
        );
    }

    [Test]
    [MethodDataSource(nameof(Scenarios))]
    public Task Selector_builds_request(Scenario scenario) => Verify(Build(scenario.Select)).UseParameters(scenario.Name);

    public sealed record Scenario(string Name, Action<IAssemblySymbolSelector> Select)
    {
        public override string ToString() => Name;
    }
}
