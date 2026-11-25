#if SPECULAR_SCANNING_EXPLICIT_USINGS
using System;
#endif
using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using static Specular.Scanning.SelectorArguments;

namespace Specular.Scanning;

/// <summary>Records an assembly selection and, when <c>GetTypes</c> is called, its type filter.</summary>
internal sealed class SymbolSelector(Compilation compilation) : ITypeSymbolSelector
{
    private readonly ImmutableArray<string>.Builder _assemblies = ImmutableArray.CreateBuilder<string>();
    private readonly ImmutableArray<string>.Builder _notAssemblies = ImmutableArray.CreateBuilder<string>();
    private readonly ImmutableArray<string>.Builder _dependenciesOf = ImmutableArray.CreateBuilder<string>();
    private bool _allAssemblies;
    private bool _includeSystemAssemblies;
    private bool _typesRequested;
    private TypeFilterData? _types;
    private bool _completed;

    public ITypeSymbolSelector EntryAssembly() => Add(_assemblies, compilation.Assembly.MetadataName);

    public ITypeSymbolSelector FromAssemblies()
    {
        EnsureOpen();
        _allAssemblies = true;
        return this;
    }

    public ITypeSymbolSelector FromAssembly(IAssemblySymbol assembly) => Add(_assemblies, AssemblyName(assembly));

    public ITypeSymbolSelector FromAssembly(string assemblyName) => Add(_assemblies, NotNullOrEmpty(assemblyName, nameof(assemblyName)));

    public ITypeSymbolSelector FromAssemblyOf(ITypeSymbol type) => Add(_assemblies, ContainingAssembly(type, nameof(type)).MetadataName);

    public ITypeSymbolSelector NotFromAssembly(IAssemblySymbol assembly) => Add(_notAssemblies, AssemblyName(assembly));

    public ITypeSymbolSelector NotFromAssembly(string assemblyName) => Add(_notAssemblies, NotNullOrEmpty(assemblyName, nameof(assemblyName)));

    public ITypeSymbolSelector NotFromAssemblyOf(ITypeSymbol type) => Add(_notAssemblies, ContainingAssembly(type, nameof(type)).MetadataName);

    public ITypeSymbolSelector DependenciesFromAssembly(IAssemblySymbol assembly) => Add(_dependenciesOf, AssemblyName(assembly));

    public ITypeSymbolSelector DependenciesFromAssembly(string assemblyName) =>
        Add(_dependenciesOf, NotNullOrEmpty(assemblyName, nameof(assemblyName)));

    public ITypeSymbolSelector DependenciesFromAssemblyOf(ITypeSymbol type) =>
        Add(_dependenciesOf, ContainingAssembly(type, nameof(type)).MetadataName);

    public ITypeSymbolSelector IncludeSystemAssemblies()
    {
        EnsureOpen();
        _includeSystemAssemblies = true;
        return this;
    }

    public void GetTypes() => SelectTypes(false, static _ => { });

    public void GetTypes(bool publicOnly) => SelectTypes(publicOnly, static _ => { });

    public void GetTypes(Action<ITypeSymbolFilter> action) => SelectTypes(false, action);

    public void GetTypes(bool publicOnly, Action<ITypeSymbolFilter> action) => SelectTypes(publicOnly, action);

    /// <summary>Snapshots the selection; later calls throw so a selector captured past <c>Build</c> cannot silently do nothing.</summary>
    public ScanRequest Complete()
    {
        _completed = true;
        return new(
            _types is null ? ScanKind.Assemblies : ScanKind.Types,
            new(
                _allAssemblies,
                _includeSystemAssemblies,
                _assemblies.ToImmutable(),
                _notAssemblies.ToImmutable(),
                _dependenciesOf.ToImmutable()
            ),
            _types
        );
    }

    private static string AssemblyName(IAssemblySymbol assembly)
    {
        ThrowIfNull(assembly, nameof(assembly));
        return assembly.MetadataName;
    }

    private void SelectTypes(bool publicOnly, Action<ITypeSymbolFilter> action)
    {
        ThrowIfNull(action, nameof(action));
        EnsureOpen();
        // Set before running the callback so a GetTypes nested inside it is rejected too.
        if (_typesRequested) throw new InvalidOperationException("GetTypes can only be called once per selection.");
        _typesRequested = true;

        var filter = new TypeSymbolFilter();
        action(filter);
        _types = filter.Complete(publicOnly);
    }

    private SymbolSelector Add(ImmutableArray<string>.Builder names, string name)
    {
        EnsureOpen();
        if (!names.Contains(name)) names.Add(name);
        return this;
    }

    private void EnsureOpen()
    {
        if (_completed) throw new InvalidOperationException("The selector can only be used inside the ScanRequest.Build callback.");
    }
}
