using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Specular.Tests;

/// <summary>The global options a generator test ran with, for driving the generator directly with <c>CSharpGeneratorDriver</c>.</summary>
internal sealed class GlobalOptionsProvider(ImmutableDictionary<string, string> globalOptions) : AnalyzerConfigOptionsProvider
{
    public override AnalyzerConfigOptions GlobalOptions { get; } = new Options(globalOptions);
    public override AnalyzerConfigOptions GetOptions(SyntaxTree tree) => Options.Empty;
    public override AnalyzerConfigOptions GetOptions(AdditionalText textFile) => Options.Empty;

    private sealed class Options(ImmutableDictionary<string, string> values) : AnalyzerConfigOptions
    {
        public static Options Empty { get; } = new([]);
        public override bool TryGetValue(string key, [NotNullWhen(true)] out string? value) => values.TryGetValue(key, out value);
    }
}
