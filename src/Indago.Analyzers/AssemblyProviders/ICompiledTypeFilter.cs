using Microsoft.CodeAnalysis;

namespace Indago.Analyzers.AssemblyProviders;

internal interface ICompiledTypeFilter<in TSymbol>
{
    string Hash { get; }
    bool IsMatch(Compilation compilation, TSymbol targetType);
}
