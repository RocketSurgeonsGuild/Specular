using TUnit.Assertions.Should.Extensions;
using static Specular.Scanning.Tests.Caching.CacheTestHelpers;

namespace Specular.Scanning.Tests.Caching;

public class ScanCacheFileTests
{
    [Test]
    public async Task File_Name_Is_The_Consumer_Id_With_The_Ctpjson_Extension() => await ScanCacheFile.GetFileName("SpecularProvider").Should().BeEqualTo("SpecularProvider.ctpjson");

    [Test]
    [Arguments("SpecularProvider")]
    [Arguments("My.Generator-2_x")]
    public async Task Valid_Consumer_Ids_Are_Accepted(string consumerId) => await ScanCacheFile.GetFileName(consumerId).Should().BeEqualTo(consumerId + ".ctpjson");

    [Test]
    [Arguments("")]
    [Arguments("a/b")]
    [Arguments("a\\b")]
    [Arguments("a b")]
    [Arguments("a:b")]
    [Arguments("é")]
    public async Task Invalid_Consumer_Ids_Are_Rejected(string consumerId)
    {
        await Act(() => ScanCacheFile.GetFileName(consumerId)).Should().Throw<ArgumentException>();
        await Act(() => ScanCache.IsCacheFile(CacheText("Valid", "{}"), consumerId)).Should().Throw<ArgumentException>();
    }

    [Test]
    public async Task Null_Consumer_Id_Is_Rejected() => await Act(() => ScanCacheFile.GetFileName(null!)).Should().Throw<ArgumentException>();

    [Test]
    public async Task Cache_File_Is_Matched_By_Exact_File_Name_Ignoring_Case()
    {
        await ScanCache.IsCacheFile(CacheText("SpecularProvider", "{}"), "SpecularProvider").Should().BeTrue();
        await ScanCache.IsCacheFile(CacheText("specularprovider", "{}"), "SpecularProvider").Should().BeTrue();
        await ScanCache.IsCacheFile(CacheText("Other", "{}"), "SpecularProvider").Should().BeFalse();
        await ScanCache.IsCacheFile(CacheText("XSpecularProvider", "{}"), "SpecularProvider").Should().BeFalse();
        await ScanCache.IsCacheFile(CacheText("SpecularProvider.ctpjson", "{}"), "SpecularProvider").Should().BeFalse();
    }

    [Test]
    [Arguments(null)]
    [Arguments("")]
    [Arguments("   ")]
    [Arguments("{")]
    [Arguments("not json at all")]
    [Arguments("[]")]
    [Arguments("null")]
    [Arguments("{\"schema\":\"one\"}")]
    [Arguments("{\"schema\":1,\"scans\":[1,2,3]}")]
    [Arguments("{\"schema\":1,\"scans\":{\"r\":{\"Lib\":{\"mvid\":\"not-a-guid\"}}}}")]
    public async Task Unreadable_Text_Parses_As_Empty(string? text)
    {
        var data = ScanCacheFile.Parse(text);

        await data.Scans.Should().BeEmpty();
        await data.Store.Should().BeEmpty();
    }

    [Test]
    public async Task Schema_Mismatch_Parses_As_Empty()
    {
        var mvid = Guid.NewGuid();
        var current = Json($"{{'schema':{ScanCacheFile.SchemaVersion},'scans':{{'r':{{'Lib':{{'mvid':'{mvid}','types':[{{'name':'A'}}]}}}}}}}}");
        var other = Json($"{{'schema':{ScanCacheFile.SchemaVersion + 1},'scans':{{'r':{{'Lib':{{'mvid':'{mvid}','types':[{{'name':'A'}}]}}}}}}}}");

        await ScanCacheFile.Parse(current).Scans.Should().NotBeEmpty();
        await ScanCacheFile.Parse(other).Scans.Should().BeEmpty();
    }
}
