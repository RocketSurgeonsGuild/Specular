using Microsoft.CodeAnalysis;

namespace Specular.Scanning.Tests.Scanning;

// What the scanner finds in the real TestAssembly, as a snapshot per scenario, so a change in matching shows up as a readable diff.
public class ScanResultSnapshotTests
{
    private static readonly Compilation compilation = ScanCompilation.Create();

    private static INamedTypeSymbol Type(string metadataName) =>
        compilation.GetTypeByMetadataName(metadataName) ?? throw new InvalidOperationException($"{metadataName} is missing.");

    public static IEnumerable<Func<Scenario>> Scenarios()
    {
        // Its own namespace only: TestAssembly also compiles in JetBrains' annotation types, which would churn with that package.
        yield return () => new(
            "test-assembly-own-types-of-any-accessibility",
            static s => s.FromAssembly(ScanCompilation.TestAssemblyName).GetTypes(f => f.InExactNamespaces("TestAssembly"))
        );
        yield return () => new("test-assembly-public-types", static s => s.FromAssembly(ScanCompilation.TestAssemblyName).GetTypes(true));
        yield return () => new("assignable-to-service", static s => s.FromAssemblies().GetTypes(f => f.AssignableTo(Type("TestAssembly.IService"))));
        yield return () => new(
            "generic-service-definition",
            static s => s.FromAssemblies().GetTypes(f => f.AssignableTo(Type("TestAssembly.IGenericService`1")))
        );
        yield return () => new(
            "generic-service-of-string",
            static s => s.FromAssemblies()
                         .GetTypes(f => f.AssignableTo(Type("TestAssembly.IGenericService`1").Construct(compilation.GetSpecialType(SpecialType.System_String))))
        );
        yield return () => new("dependents-of-specular", static s => s.DependenciesFromAssembly("Specular"));
        yield return () => new("unresolved-type", static s => s.FromAssemblies().GetTypes(f => f.AssignableTo("Missing.IService")));
    }

    [Test]
    [MethodDataSource(nameof(Scenarios))]
    public Task Scan_finds(Scenario scenario) =>
        Verify(Scanner.Scan(compilation, ScanRequest.Build(compilation, scenario.Select))).UseParameters(scenario.Name);

    public sealed record Scenario(string Name, Action<IAssemblySymbolSelector> Select)
    {
        public override string ToString() => Name;
    }
}
