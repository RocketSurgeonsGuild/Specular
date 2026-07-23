using Microsoft.CodeAnalysis;

namespace Specular.Analyzers.AssemblyProviders;

internal interface ICompiledTypeFilter<in TSymbol>
{
    string Hash { get; }
    bool IsMatch(Compilation compilation, TSymbol targetSymbol);
}
