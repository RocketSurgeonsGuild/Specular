using Microsoft.CodeAnalysis;

namespace Indago.Analyzers.AssemblyProviders;

internal class AlwaysMatchTypeFilter<TSymbol> : ICompiledTypeFilter<TSymbol>
{
    public bool IsMatch(Compilation compilation, TSymbol targetType) => true;

    public string Hash => "AlwaysMatch";
}
