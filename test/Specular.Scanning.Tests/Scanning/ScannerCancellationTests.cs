using TUnit.Assertions.Should.Extensions;
using static Specular.Scanning.Tests.Scanning.Scans;

namespace Specular.Scanning.Tests.Scanning;

public class ScannerCancellationTests
{
    [Test]
    public async Task A_cancelled_token_stops_the_scan_before_it_starts()
    {
        using var source = new CancellationTokenSource();
        await source.CancelAsync();
        var compilation = ScanCompilation.Create();

        await ( (Action)( () => Scanner.Scan(compilation, Types(AllAssemblies), source.Token) ) ).Should().Throw<OperationCanceledException>();
        await ( (Action)( () => Scanner.Scan(compilation, Assemblies(AllAssemblies), source.Token) ) ).Should().Throw<OperationCanceledException>();
    }

    [Test]
    public async Task Cancelling_during_a_walk_stops_it_promptly()
    {
        using var source = new CancellationTokenSource();
        var cancelling = new RecordingNameFilter(
            "cancel",
            _ =>
            {
                source.Cancel();
                return true;
            }
        );
        var compilation = ScanCompilation.Create();
        var request = Types(FromAssemblies("TestAssembly"), TypeFilterData.All with { CustomFilters = Array<CustomTypeFilter>(cancelling) });

        await ( (Action)( () => Scanner.Scan(compilation, request, source.Token) ) ).Should().Throw<OperationCanceledException>();
        await cancelling.Visited.Count.Should().BeEqualTo(1);
    }
}
