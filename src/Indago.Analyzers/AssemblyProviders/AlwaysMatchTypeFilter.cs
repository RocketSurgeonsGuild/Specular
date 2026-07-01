using Microsoft.CodeAnalysis;

namespace Indago.Analyzers.AssemblyProviders;

[SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "Instantiated via the generic type-filter pipeline.")]
internal sealed class AlwaysMatchTypeFilter<TSymbol> : ICompiledTypeFilter<TSymbol>
{
    public bool IsMatch(Compilation compilation, TSymbol targetType) => true;

    public string Hash => "AlwaysMatch";
}
