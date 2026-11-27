using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using TUnit.Assertions.Should.Extensions;
using static Specular.Scanning.Tests.Scanning.Scans;

namespace Specular.Scanning.Tests.Scanning;

/// <summary>
///     Assignability to an interface is decided without the compiler when no interface a type is or implements shares the target's
///     definition; these keep that shortcut agreeing with the compiler's implicit conversions.
/// </summary>
public class InterfaceAssignabilityScanTests
{
    private const string Source = """
        namespace Shapes
        {
            public interface IBase;
            public interface IDerived : IBase;
            public interface IUnrelated;

            public class ClassService : IDerived;
            public struct ValueService : IBase;
            public class BaseClass : IBase;
            public class InheritsInterface : BaseClass;
            public class Plain;
            public struct PlainValue;
            public enum Kind { A }
            public record Message(int Value) : IUnrelated;
        }
        """;

    private static readonly Compilation compilation = ScanCompilation.Create(Source);

    private static TypeReference Project(string metadataName) => new(ScanCompilation.ProjectAssemblyName, metadataName, false);

    private static ImmutableArray<string> Assignable(bool include, params TypeReference[] types) =>
        ProjectTypes(compilation, TypeFilterData.All with { AssignableTo = Array(new AssignableToFilterData(include, Array(types))) });

    [Test]
    public async Task An_interface_matches_itself_derived_interfaces_classes_through_their_bases_and_boxing_structs()
    {
        await Assignable(true, Project("Shapes.IBase"))
             .Should()
             .BeEquivalentTo(["Shapes.BaseClass", "Shapes.ClassService", "Shapes.IBase", "Shapes.IDerived", "Shapes.InheritsInterface", "Shapes.ValueService"]);
    }

    [Test]
    public async Task Types_without_the_interface_are_left_out_and_kept_by_the_exclude_clause()
    {
        await Assignable(true, Project("Shapes.IUnrelated")).Should().BeEquivalentTo(["Shapes.IUnrelated", "Shapes.Message"]);

        var excluded = Assignable(false, Project("Shapes.IBase"));
        await excluded.Should().Contain("Shapes.Plain");
        await excluded.Should().Contain("Shapes.PlainValue");
        await excluded.Should().Contain("Shapes.Kind");
        await excluded.Should().NotContain("Shapes.ValueService");
    }
}
