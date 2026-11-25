using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using TUnit.Assertions.Should.Extensions;
using static Specular.Scanning.Tests.Scanning.Scans;

namespace Specular.Scanning.Tests.Scanning;

// Closed generics whose type arguments are arrays: IHandler<int[]> names exactly that closed type.
public class ArrayTypeArgumentScanTests
{
    private const string Source = """
        namespace Arrays
        {
            public interface IHandler<T>;
            public class IntHandler : IHandler<int>;
            public class IntArrayHandler : IHandler<int[]>;
            public class IntMatrixHandler : IHandler<int[,]>;
            public class JaggedHandler : IHandler<int[][,]>;
            public class ClosedArrayHandler : IHandler<IHandler<int>[]>;
            public class StringArrayHandler : IHandler<string[]>;
        }
        """;

    private static readonly Compilation compilation = ScanCompilation.Create(Source);

    private static readonly TypeReference @int = new("", "System.Int32", false);

    private static TypeReference Handler(TypeReference argument) =>
        new(ScanCompilation.ProjectAssemblyName, "Arrays.IHandler`1", false) { TypeArguments = Array(argument) };

    private static EquatableArray<int> Ranks(params int[] ranks) => ranks.ToImmutableArray();

    private static ScanResult Assignable(params TypeReference[] types) =>
        Scanner.Scan(
            compilation,
            Types(FromAssemblies(ScanCompilation.ProjectAssemblyName), TypeFilterData.All with { AssignableTo = Array(new AssignableToFilterData(true, Array(types))) })
        );

    [Test]
    public async Task An_array_argument_matches_only_that_array_type()
    {
        await TypeNames(Assignable(Handler(@int with { ArrayRanks = Ranks(1) }))).Should().BeEquivalentTo(["Arrays.IntArrayHandler"]);
        await TypeNames(Assignable(Handler(@int with { ArrayRanks = Ranks(2) }))).Should().BeEquivalentTo(["Arrays.IntMatrixHandler"]);
    }

    [Test]
    public async Task Jagged_arrays_resolve_with_their_ranks_outermost_first() => await TypeNames(Assignable(Handler(@int with { ArrayRanks = Ranks(1, 2) }))).Should().BeEquivalentTo(["Arrays.JaggedHandler"]);

    [Test]
    public async Task An_array_of_a_closed_generic_resolves() => await TypeNames(Assignable(Handler(Handler(@int) with { ArrayRanks = Ranks(1) }))).Should().BeEquivalentTo(["Arrays.ClosedArrayHandler"]);

    [Test]
    public async Task The_selector_and_the_scanner_round_trip_an_array_argument()
    {
        var handler = compilation.GetTypeByMetadataName("Arrays.IHandler`1")!;
        var jagged = compilation.CreateArrayTypeSymbol(compilation.CreateArrayTypeSymbol(compilation.GetSpecialType(SpecialType.System_Int32), 2), 1);
        var request = ScanRequest.Build(compilation, s => s.FromAssemblies().GetTypes(f => f.AssignableTo(handler.Construct(jagged)).InNamespaces("Arrays")));

        var result = Scanner.Scan(compilation, request);

        await TypeNames(result).Should().BeEquivalentTo(["Arrays.JaggedHandler"]);
        await result.UnresolvedTypes.Count.Should().BeEqualTo(0);
    }

    public static IEnumerable<Func<UnresolvableArrayReference>> UnresolvableArrayReferences()
    {
        // A filter target is a named type, so an array can only appear as a type argument.
        yield return () => new("a top-level array", @int with { ArrayRanks = Ranks(1) });
        yield return () => new("an array of a missing type", Handler(new TypeReference("", "Missing.Type", false) { ArrayRanks = Ranks(1) }));
        yield return () => new("a rank of zero", Handler(@int with { ArrayRanks = Ranks(0) }));
        yield return () => new("a rank above the runtime's limit", Handler(@int with { ArrayRanks = Ranks(33) }));
    }

    [Test]
    [MethodDataSource(nameof(UnresolvableArrayReferences))]
    public async Task An_array_reference_that_cannot_be_built_is_unresolved(UnresolvableArrayReference unresolvable)
    {
        var result = Assignable(unresolvable.Reference);

        await result.Types.Count.Should().BeEqualTo(0);
        await result.UnresolvedTypes.Should().BeEqualTo(Array(unresolvable.Reference));
    }

    public sealed record UnresolvableArrayReference(string Name, TypeReference Reference)
    {
        public override string ToString() => Name;
    }
}
