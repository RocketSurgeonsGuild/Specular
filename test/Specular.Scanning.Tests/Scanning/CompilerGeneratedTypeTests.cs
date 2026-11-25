using TUnit.Assertions.Should.Extensions;
using static Specular.Scanning.Tests.Scanning.Scans;

namespace Specular.Scanning.Tests.Scanning;

public class CompilerGeneratedTypeTests
{
    // Collection expressions make the compiler emit its own types into the assembly (<>z__ReadOnlyArray<T>,
    // <>z__ReadOnlySingleElementList<T> with a nested Enumerator), as Specular's generated provider now does in every project.
    private const string LibrarySource = """
        using System.Collections.Generic;

        namespace Lib
        {
            public class Holder
            {
                public IEnumerable<int> One() => [1];
                public IEnumerable<int> Many() => [1, 2, 3];
            }
        }
        """;

    [Test]
    public async Task Types_nested_in_a_compiler_generated_type_are_not_scanned()
    {
        var library = ScanCompilation.EmitReference(LibrarySource, "Lib");
        var compilation = ScanCompilation.Create(additionalReferences: library);

        var names = TypeNames(Scanner.Scan(compilation, Types(FromAssemblies("Lib"))));

        await names.Should().BeEquivalentTo(["Lib.Holder"]);
    }
}
