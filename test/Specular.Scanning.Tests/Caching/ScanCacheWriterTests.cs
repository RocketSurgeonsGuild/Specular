using TUnit.Assertions.Should.Extensions;
using static Specular.Scanning.Tests.Caching.CacheTestHelpers;

namespace Specular.Scanning.Tests.Caching;

public class ScanCacheWriterTests
{
    private const string IntermediateOutputPath = "build_property.IntermediateOutputPath";
    private const string ProjectDir = "build_property.ProjectDir";

    [Test]
    public async Task Cache_Directory_Is_Ctp_Under_An_Absolute_Intermediate_Output_Path()
    {
        using var temp = CreateTempDirectory();
        var obj = Path.Combine(temp.Path, "obj", "Debug");

        var directory = ScanCacheWriter.GetCacheDirectory(Options((IntermediateOutputPath, obj)));

        await directory.Should().BeEqualTo(Path.Combine(obj, "ctp"));
    }

    [Test]
    public async Task Relative_Intermediate_Output_Path_Is_Resolved_Against_The_Project_Directory()
    {
        using var temp = CreateTempDirectory();

        var directory = ScanCacheWriter.GetCacheDirectory(Options((IntermediateOutputPath, Path.Combine("obj", "Debug")), (ProjectDir, temp.Path)));

        await directory.Should().BeEqualTo(Path.Combine(temp.Path, "obj", "Debug", "ctp"));
    }

    [Test]
    public async Task No_Cache_Directory_Without_Usable_Options()
    {
        await ScanCacheWriter.GetCacheDirectory(Options()).Should().BeNull();
        await ScanCacheWriter.GetCacheDirectory(Options((IntermediateOutputPath, ""))).Should().BeNull();
        await ScanCacheWriter.GetCacheDirectory(Options((IntermediateOutputPath, "obj"))).Should().BeNull();
        await ScanCacheWriter.GetCacheDirectory(Options((ProjectDir, Path.GetTempPath()))).Should().BeNull();
    }

    [Test]
    public async Task Writes_The_Consumer_File_Creating_The_Directory()
    {
        using var temp = CreateTempDirectory();
        var obj = Path.Combine(temp.Path, "obj");

        var written = ScanCacheWriter.Write(Options((IntermediateOutputPath, obj)), "Consumer", "{\"schema\":1}");

        await written.Should().BeTrue();
        await File.ReadAllText(Path.Combine(obj, "ctp", "Consumer.ctpjson")).Should().BeEqualTo("{\"schema\":1}");
    }

    [Test]
    public async Task Does_Not_Rewrite_Unchanged_Content()
    {
        using var temp = CreateTempDirectory();
        var options = Options((IntermediateOutputPath, temp.Path));
        _ = ScanCacheWriter.Write(options, "Consumer", "{\"schema\":1}");
        var path = Path.Combine(temp.Path, "ctp", "Consumer.ctpjson");
        var stamp = new DateTime(2020, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        File.SetLastWriteTimeUtc(path, stamp);

        var written = ScanCacheWriter.Write(options, "Consumer", "{\"schema\":1}");

        await written.Should().BeFalse();
        await File.GetLastWriteTimeUtc(path).Should().BeEqualTo(stamp);
    }

    [Test]
    public async Task Rewrites_Changed_Content()
    {
        using var temp = CreateTempDirectory();
        var options = Options((IntermediateOutputPath, temp.Path));
        _ = ScanCacheWriter.Write(options, "Consumer", "{\"schema\":1}");

        var written = ScanCacheWriter.Write(options, "Consumer", "{\"schema\":1,\"scans\":{}}");

        await written.Should().BeTrue();
        await File.ReadAllText(Path.Combine(temp.Path, "ctp", "Consumer.ctpjson")).Should().BeEqualTo("{\"schema\":1,\"scans\":{}}");
    }

    [Test]
    public async Task Does_Nothing_Without_Options()
    {
        var written = ScanCacheWriter.Write(Options(), "Consumer", "{}");

        await written.Should().BeFalse();
    }

    [Test]
    public async Task Swallows_Io_Failures()
    {
        using var temp = CreateTempDirectory();
        var file = Path.Combine(temp.Path, "not-a-directory");
        File.WriteAllText(file, "");

        var written = ScanCacheWriter.Write(Options((IntermediateOutputPath, file)), "Consumer", "{}");

        await written.Should().BeFalse();
    }

    [Test]
    public async Task Rejects_Invalid_Consumer_Ids()
    {
        using var temp = CreateTempDirectory();

        await Act(() => ScanCacheWriter.Write(Options((IntermediateOutputPath, temp.Path)), "../escape", "{}")).Should().Throw<ArgumentException>();
    }
}
