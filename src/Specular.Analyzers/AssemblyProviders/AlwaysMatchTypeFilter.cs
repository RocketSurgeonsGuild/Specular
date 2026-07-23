using Microsoft.CodeAnalysis;

namespace Specular.Analyzers.AssemblyProviders;

[SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "Instantiated via the generic type-filter pipeline.")]
internal sealed class AlwaysMatchTypeFilter<TSymbol> : ICompiledTypeFilter<TSymbol>
{
    public bool IsMatch(Compilation compilation, TSymbol targetSymbol) => true;

    public string Hash => "AlwaysMatch";
}
