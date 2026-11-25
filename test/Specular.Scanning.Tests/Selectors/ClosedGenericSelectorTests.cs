using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using TUnit.Assertions.Should.Extensions;
using static Specular.Scanning.Tests.Selectors.SelectorCompilation;
using static Specular.Scanning.Tests.Selectors.SelectorTestData;

namespace Specular.Scanning.Tests.Selectors;

public class ClosedGenericSelectorTests
{
    private static INamedTypeSymbol Int32 => SelectorCompilation.Compilation.GetSpecialType(SpecialType.System_Int32);

    private static INamedTypeSymbol String => SelectorCompilation.Compilation.GetSpecialType(SpecialType.System_String);

    private static INamedTypeSymbol Handler => Type("Library.Contracts.IHandler`1");

    private static TypeReference Recorded(ITypeSymbol type) => Types(f => f.AssignableTo(type)).AssignableTo[0].Types[0];

    [Test]
    public async Task A_type_argument_that_is_itself_a_closed_generic_is_recorded_recursively()
    {
        var reference = Recorded(Handler.Construct(Handler.Construct(Int32)));

        await reference.Should().BeEqualTo(Closed(Library("Library.Contracts.IHandler`1"), Closed(Library("Library.Contracts.IHandler`1"), Special(SpecialType.System_Int32))));
    }

    [Test]
    public async Task A_type_argument_nested_in_a_closed_generic_records_its_containing_types_arguments()
    {
        var inner = Type("App.Handlers.Outer`1").Construct(String).GetTypeMembers("Inner").Single();

        var reference = Recorded(Handler.Construct(inner));

        await reference.Should().BeEqualTo(Closed(Library("Library.Contracts.IHandler`1"), Closed(App("App.Handlers.Outer`1+Inner"), Special(SpecialType.System_String))));
    }

    [Test]
    public async Task A_type_nested_in_a_closed_generic_lists_every_type_argument_outermost_first()
    {
        var nested = Type("App.Handlers.Outer`1").Construct(Int32).GetTypeMembers("INested").Single().Construct(String);

        var reference = Recorded(nested);

        await reference.Should().BeEqualTo(Closed(App("App.Handlers.Outer`1+INested`1"), Special(SpecialType.System_Int32), Special(SpecialType.System_String)));
    }

    [Test]
    public async Task Generic_definitions_and_unbound_generics_record_no_type_arguments()
    {
        await Recorded(Handler).TypeArguments.Count.Should().BeEqualTo(0);
        await Recorded(Handler.ConstructUnboundGenericType()).TypeArguments.Count.Should().BeEqualTo(0);
        await Recorded(Type("App.Handlers.Outer`1+Inner")).TypeArguments.Count.Should().BeEqualTo(0);
    }

    [Test]
    public async Task Closed_generics_with_different_arguments_are_different_requests()
    {
        var forInt = Build(s => s.FromAssemblies().GetTypes(f => f.AssignableTo(Handler.Construct(Int32))));
        var forIntAgain = Build(s => s.FromAssemblies().GetTypes(f => f.AssignableTo(Handler.Construct(Int32))));
        var forString = Build(s => s.FromAssemblies().GetTypes(f => f.AssignableTo(Handler.Construct(String))));
        var forDefinition = Build(s => s.FromAssemblies().GetTypes(f => f.AssignableTo(Handler)));

        await forIntAgain.Should().BeEqualTo(forInt);
        await forIntAgain.CacheKey.Should().BeEqualTo(forInt.CacheKey);
        await forString.Should().NotBeEqualTo(forInt);
        await forString.CacheKey.Should().NotBeEqualTo(forInt.CacheKey);
        await forDefinition.Should().NotBeEqualTo(forInt);
        await forDefinition.CacheKey.Should().NotBeEqualTo(forInt.CacheKey);
    }

    [Test]
    public async Task The_same_closed_generic_twice_in_an_any_clause_is_recorded_once()
    {
        var types = Types(f => f.AssignableToAny(Handler.Construct(Int32), Handler.Construct(String), Handler.Construct(Int32)));

        await types.AssignableTo[0].Types.Count.Should().BeEqualTo(2);
    }

    public static IEnumerable<Func<UnsupportedArgument>> UnsupportedArguments()
    {
        yield return () => new("pointer", SelectorCompilation.Compilation.CreatePointerTypeSymbol(Int32));
        yield return () => new("type parameter", Type("App.Handlers.Outer`1").TypeParameters[0]);
        yield return () => new("error type", SelectorCompilation.Compilation.CreateErrorTypeSymbol(null, "Missing", 0));
        yield return () => new("dynamic", SelectorCompilation.Compilation.DynamicType);
        yield return () => new("generic definition", Handler);
        yield return () => new("array of a type parameter", SelectorCompilation.Compilation.CreateArrayTypeSymbol(Type("App.Handlers.Outer`1").TypeParameters[0]));
        yield return () => new("array of a pointer", SelectorCompilation.Compilation.CreateArrayTypeSymbol(SelectorCompilation.Compilation.CreatePointerTypeSymbol(Int32)));
    }

    [Test]
    [MethodDataSource(nameof(UnsupportedArguments))]
    public async Task Unsupported_type_arguments_are_rejected_with_a_pointer_to_the_generic_definition(UnsupportedArgument argument)
    {
        var closed = Handler.Construct(argument.Type);

        await Act(() => Types(f => f.AssignableTo(closed))).Should().Throw<ArgumentException>().WithMessageContaining("IHandler<>");
        await Act(() => Types(f => f.NotAssignableToAny(Handler, closed))).Should().Throw<ArgumentException>();
        await Act(() => Types(f => f.WithAttribute(Type("Library.Contracts.TagAttribute`1").Construct(argument.Type)))).Should().Throw<ArgumentException>();
    }

    [Test]
    public async Task An_array_type_argument_is_recorded_as_its_element_type_and_rank()
    {
        var reference = Recorded(Handler.Construct(ArrayOf(Int32)));

        await reference.Should().BeEqualTo(Closed(Library("Library.Contracts.IHandler`1"), Special(SpecialType.System_Int32) with { ArrayRanks = Ranks(1) }));
    }

    [Test]
    public async Task Jagged_and_multidimensional_arrays_record_every_rank_outermost_first()
    {
        // int[][,]: an array (rank 1) of int[,] (rank 2).
        var reference = Recorded(Handler.Construct(ArrayOf(ArrayOf(Int32, 2), 1)));

        await reference.TypeArguments[0].Should().BeEqualTo(Special(SpecialType.System_Int32) with { ArrayRanks = Ranks(1, 2) });
    }

    [Test]
    public async Task An_array_of_a_closed_generic_keeps_the_elements_type_arguments()
    {
        var reference = Recorded(Handler.Construct(ArrayOf(Handler.Construct(Int32))));

        await reference.TypeArguments[0]
                       .Should()
                       .BeEqualTo(Closed(Library("Library.Contracts.IHandler`1"), Special(SpecialType.System_Int32)) with { ArrayRanks = Ranks(1) });
    }

    [Test]
    public async Task Array_shapes_are_different_requests()
    {
        string[] keys =
        [
            Build(s => s.FromAssemblies().GetTypes(f => f.AssignableTo(Handler.Construct(Int32)))).CacheKey,
            Build(s => s.FromAssemblies().GetTypes(f => f.AssignableTo(Handler.Construct(ArrayOf(Int32))))).CacheKey,
            Build(s => s.FromAssemblies().GetTypes(f => f.AssignableTo(Handler.Construct(ArrayOf(Int32, 2))))).CacheKey,
            Build(s => s.FromAssemblies().GetTypes(f => f.AssignableTo(Handler.Construct(ArrayOf(ArrayOf(Int32)))))).CacheKey,
        ];

        await keys.Distinct().Count().Should().BeEqualTo(keys.Length);
    }

    [Test]
    public async Task A_top_level_array_is_still_not_selectable() => await Act(() => Types(f => f.AssignableTo(ArrayOf(Int32)))).Should().Throw<ArgumentException>();

    [Test]
    public async Task ArrayRanks_default_to_empty_and_take_part_in_equality()
    {
        var element = new TypeReference("", "System.Int32", false);

        await element.ArrayRanks.Count.Should().BeEqualTo(0);
        await ( element with { ArrayRanks = Ranks(1) } ).Should().NotBeEqualTo(element);
        await ( element with { ArrayRanks = Ranks(1) } ).Should().BeEqualTo(element with { ArrayRanks = Ranks(1) });
        await ( element with { ArrayRanks = Ranks(1, 2) } ).Should().NotBeEqualTo(element with { ArrayRanks = Ranks(2, 1) });
    }

    private static IArrayTypeSymbol ArrayOf(ITypeSymbol element, int rank = 1) => SelectorCompilation.Compilation.CreateArrayTypeSymbol(element, rank);

    private static EquatableArray<int> Ranks(params int[] ranks) => ranks.ToImmutableArray();

    [Test]
    public async Task TypeArguments_default_to_empty_and_take_part_in_equality()
    {
        var plain = new TypeReference("Library", "Library.Contracts.IHandler`1", false);
        var closed = plain with { TypeArguments = Array(new TypeReference("", "System.Int32", false)) };
        var closedAgain = plain with { TypeArguments = Array(new TypeReference("", "System.Int32", false)) };

        await plain.TypeArguments.Count.Should().BeEqualTo(0);
        await plain.Should().BeEqualTo(new TypeReference("Library", "Library.Contracts.IHandler`1", false) { TypeArguments = EquatableArray<TypeReference>.Empty });
        await closed.Should().NotBeEqualTo(plain);
        await closedAgain.Should().BeEqualTo(closed);
        await closedAgain.GetHashCode().Should().BeEqualTo(closed.GetHashCode());
    }

    public sealed record UnsupportedArgument(string Name, ITypeSymbol Type)
    {
        public override string ToString() => Name;
    }
}
