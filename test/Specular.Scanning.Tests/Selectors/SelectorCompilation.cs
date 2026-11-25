using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Specular.Scanning.Tests.Selectors;

/// <summary>
///     A two-assembly compilation for selector tests: <c>App</c> references <c>Library</c>, so assembly selectors have a type from
///     another assembly to point at.
/// </summary>
internal static class SelectorCompilation
{
    public const string AppAssemblyName = "App";
    public const string LibraryAssemblyName = "Library";

    private const string LibrarySource = """
        namespace Library.Contracts
        {
            public interface IHandler { }
            public interface IHandler<T> { }
            public sealed class LibraryAttribute : System.Attribute { }
            public sealed class TagAttribute<T> : System.Attribute { }
        }
        """;

    private const string AppSource = """
        namespace App.Handlers
        {
            public sealed class MarkerAttribute : System.Attribute { }
            public sealed class OtherAttribute : System.Attribute { }
            public class Handler : Library.Contracts.IHandler { }
            public class Outer<T>
            {
                public class Inner { }
                public interface INested<U> { }
            }
        }

        namespace App.Handlers.Deep
        {
            public class DeepHandler { }
        }

        namespace App.Services
        {
            public class Service { }
        }

        public class GlobalType { }
        """;

    private static readonly ImmutableArray<MetadataReference> FrameworkReferences = [.. ( (string)AppContext.GetData("TRUSTED_PLATFORM_ASSEMBLIES")! )
                                                                                   .Split(Path.PathSeparator)
                                                                                   .Where(path => Path.GetFileName(path) is "System.Runtime.dll" or "System.Private.CoreLib.dll" or "netstandard.dll")
                                                                                   .Select(path => (MetadataReference)MetadataReference.CreateFromFile(path))];

    public static Compilation Compilation { get; } = CreateApp();

    public static IAssemblySymbol LibraryAssembly => Type("Library.Contracts.IHandler").ContainingAssembly;

    public static INamedTypeSymbol Type(string metadataName) =>
        Compilation.GetTypeByMetadataName(metadataName) ?? throw new InvalidOperationException($"{metadataName} is not in the test compilation.");

    public static INamedTypeSymbol Unbound(string metadataName) => Type(metadataName).ConstructUnboundGenericType();

    public static ScanRequest Build(Action<IAssemblySymbolSelector> select) => ScanRequest.Build(Compilation, select);

    public static TypeFilterData Types(Action<ITypeSymbolFilter> filter) =>
        Build(s => s.FromAssemblies().GetTypes(filter)).Types ?? throw new InvalidOperationException("Expected a types scan.");

    private static Compilation CreateApp()
    {
        var library = CSharpCompilation.Create(
            LibraryAssemblyName,
            [CSharpSyntaxTree.ParseText(LibrarySource)],
            FrameworkReferences,
            new(OutputKind.DynamicallyLinkedLibrary)
        );
        var app = CSharpCompilation.Create(
            AppAssemblyName,
            [CSharpSyntaxTree.ParseText(AppSource)],
            FrameworkReferences.Add(library.ToMetadataReference()),
            new(OutputKind.DynamicallyLinkedLibrary)
        );

        var errors = app.GetDiagnostics().Where(d => d.Severity == DiagnosticSeverity.Error).ToArray();
        return errors.Length == 0
            ? app
            : throw new InvalidOperationException(string.Join(Environment.NewLine, errors.Select(e => e.ToString())));
    }
}
