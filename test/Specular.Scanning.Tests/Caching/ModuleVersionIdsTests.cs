using Microsoft.CodeAnalysis;
using TUnit.Assertions.Should.Extensions;
using static Specular.Scanning.Tests.Caching.CacheTestHelpers;

namespace Specular.Scanning.Tests.Caching;

public class ModuleVersionIdsTests
{
    [Test]
    public async Task Returns_The_Module_Version_Id_Of_A_Portable_Executable_Reference()
    {
        var image = EmitLibrary("public class A { }");
        var reference = MetadataReference.CreateFromImage(image);
        var compilation = CreateConsumer(reference);
        using var expected = ModuleMetadata.CreateFromImage(image);

        var mvid = ModuleVersionIds.Get(compilation, GetAssembly(compilation, reference));

        await mvid.Should().BeEqualTo(expected.GetModuleVersionId());
    }

    [Test]
    public async Task Returns_The_Same_Module_Version_Id_For_The_Same_Image()
    {
        var image = EmitLibrary("public class A { }");
        var first = CreateConsumer(MetadataReference.CreateFromImage(image));
        var secondReference = MetadataReference.CreateFromImage(image);
        var second = CreateConsumer(secondReference);

        var firstMvid = ModuleVersionIds.Get(first, GetAssembly(first, first.References.Last()));
        var secondMvid = ModuleVersionIds.Get(second, GetAssembly(second, secondReference));

        await firstMvid.HasValue.Should().BeTrue();
        await secondMvid.Should().BeEqualTo(firstMvid);
    }

    [Test]
    public async Task Returns_A_Different_Module_Version_Id_When_The_Assembly_Is_Rebuilt_With_Different_Source()
    {
        var before = MetadataReference.CreateFromImage(EmitLibrary("public class A { }"));
        var after = MetadataReference.CreateFromImage(EmitLibrary("public class A { } public class B { }"));
        var beforeCompilation = CreateConsumer(before);
        var afterCompilation = CreateConsumer(after);

        var beforeMvid = ModuleVersionIds.Get(beforeCompilation, GetAssembly(beforeCompilation, before));
        var afterMvid = ModuleVersionIds.Get(afterCompilation, GetAssembly(afterCompilation, after));

        await afterMvid.HasValue.Should().BeTrue();
        await afterMvid.Should().NotBeEqualTo(beforeMvid);
    }

    [Test]
    public async Task Returns_A_Module_Version_Id_For_A_Reference_Loaded_From_File()
    {
        var compilation = CreateConsumer();
        var corlib = compilation.GetAssemblyOrModuleSymbol(compilation.References.First()) as IAssemblySymbol;

        var mvid = ModuleVersionIds.Get(compilation, corlib!);

        await mvid.HasValue.Should().BeTrue();
    }

    [Test]
    public async Task Returns_Null_For_A_Compilation_Reference()
    {
        var reference = CreateLibraryCompilation("public class A { }").ToMetadataReference();
        var compilation = CreateConsumer(reference);

        var mvid = ModuleVersionIds.Get(compilation, GetAssembly(compilation, reference));

        await mvid.Should().BeNull();
    }

    [Test]
    public async Task Returns_Null_For_The_Compilations_Own_Assembly()
    {
        var compilation = CreateConsumer();

        var mvid = ModuleVersionIds.Get(compilation, compilation.Assembly);

        await mvid.Should().BeNull();
    }

    [Test]
    public async Task Returns_Null_For_An_Assembly_From_Another_Compilation()
    {
        var reference = MetadataReference.CreateFromImage(EmitLibrary("public class A { }"));
        var compilation = CreateConsumer(reference);
        var unrelated = CreateConsumer(MetadataReference.CreateFromImage(EmitLibrary("public class A { }", "Other")));

        var mvid = ModuleVersionIds.Get(compilation, GetAssembly(unrelated, unrelated.References.Last()));

        await mvid.Should().BeNull();
    }
}
