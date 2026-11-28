#if SPECULAR_SCANNING_EXPLICIT_USINGS
using System;
using System.Collections.Generic;
using System.Linq;
#endif
using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Specular.Scanning;

/// <summary>
///     Which assemblies a project's scans look in when a selector does not name them: its assembly scope. By default every assembly but
///     the framework's: those signed with one of Microsoft's framework keys, and those named <c>netstandard</c>, <c>mscorlib</c>,
///     <c>System</c>, <c>System.*</c>, <c>Microsoft.*</c> or <c>WindowsBase</c>. A project adds exclude and include patterns on top
///     (<c>*</c> and <c>?</c> wildcards over the whole assembly name, case-insensitive), and an include always wins, so <c>Include="*"</c>
///     scans everything. The compilation's own assembly is always in scope.
/// </summary>
/// <param name="Exclude">The project's exclude patterns.</param>
/// <param name="Include">The project's include patterns; an assembly matching one is in scope whatever excludes it.</param>
/// <param name="ExcludeFramework">Whether the framework default applies; false only for <see cref="Everything" />.</param>
#if SPECULAR_SCANNING_SOURCES
internal
#else
public
#endif
sealed record ScanAssemblyScope(EquatableArray<string> Exclude, EquatableArray<string> Include, bool ExcludeFramework = true)
{
    /// <summary>The framework default and nothing else.</summary>
    public static ScanAssemblyScope Default { get; } = new(EquatableArray<string>.Empty, EquatableArray<string>.Empty);

    /// <summary>Every assembly, framework included.</summary>
    public static ScanAssemblyScope Everything { get; } = new(EquatableArray<string>.Empty, EquatableArray<string>.Empty, false);

    /// <summary>The names the framework default excludes besides the signing keys: assemblies built without them, or for tests.</summary>
    public static ImmutableArray<string> FrameworkPatterns { get; } = ["netstandard", "mscorlib", "System", "System.*", "Microsoft.*", "WindowsBase"];

    /// <summary>
    ///     The scope a project gives the generator <paramref name="consumerId" />: its patterns from the MSBuild properties
    ///     <c>&lt;consumerId&gt;ExcludeAssemblies</c> and <c>&lt;consumerId&gt;IncludeAssemblies</c> (semicolon- or comma-separated, made
    ///     visible to the compiler), on top of the framework default. The generator's package sets them from what its users write.
    /// </summary>
    public static ScanAssemblyScope FromOptions(AnalyzerConfigOptions globalOptions, string consumerId)
    {
        if (globalOptions is null) throw new ArgumentNullException(nameof(globalOptions));

        var exclude = Patterns(globalOptions, $"build_property.{consumerId}ExcludeAssemblies");
        var include = Patterns(globalOptions, $"build_property.{consumerId}IncludeAssemblies");
        return exclude.Count == 0 && include.Count == 0 ? Default : new(exclude, include);
    }

    /// <summary>Whether a broad scan (one that does not name the assembly) looks in <paramref name="assembly" />.</summary>
    public bool Contains(Compilation compilation, IAssemblySymbol assembly) => Decide(compilation, assembly) == Decision.InScope;

    /// <summary>
    ///     Whether a scan with <paramref name="selection" /> looks in <paramref name="assembly" />, as far as the scope decides: an assembly
    ///     the selection names, or one only the framework default leaves out when the selection includes system assemblies, is scanned
    ///     for that selection whatever the scope says. Whether the selection itself matches the assembly is the caller's question.
    /// </summary>
    public bool Allows(Compilation compilation, IAssemblySymbol assembly, AssemblyFilterData selection)
    {
        return selection is null
            ? throw new ArgumentNullException(nameof(selection))
            : Decide(compilation, assembly) switch
            {
                Decision.InScope => true,
                Decision.FrameworkDefault => selection.IncludeSystemAssemblies || Names(selection, assembly),
                Decision.Excluded => throw new NotImplementedException(),
                _ => Names(selection, assembly),
            };
    }

    private Decision Decide(Compilation compilation, IAssemblySymbol assembly)
    {
        if (SymbolEqualityComparer.Default.Equals(assembly, compilation.Assembly)) return Decision.InScope;

        var name = assembly.Identity.Name;
        foreach (var pattern in Include.AsImmutableArray())
        {
            if (Glob.IsMatch(pattern, name)) return Decision.InScope;
        }

        foreach (var pattern in Exclude.AsImmutableArray())
        {
            if (Glob.IsMatch(pattern, name)) return Decision.Excluded;
        }

        return ExcludeFramework && IsFramework(assembly) ? Decision.FrameworkDefault : Decision.InScope;
    }

    private static bool IsFramework(IAssemblySymbol assembly)
    {
        if (IsFrameworkKey(assembly.Identity.PublicKeyToken)) return true;

        foreach (var pattern in FrameworkPatterns)
        {
            if (Glob.IsMatch(pattern, assembly.Identity.Name)) return true;
        }

        return false;
    }

    // ECMA (mscorlib, System.Core), Microsoft (System.*), netstandard and System.Runtime, System.Private.CoreLib, and the two keys
    // Microsoft.*, ASP.NET Core and Microsoft.Extensions.* are signed with.
    private static readonly string[] frameworkKeyTokens =
    [
        "b77a5c561934e089",
        "b03f5f7f11d50a3a",
        "cc7b13ffcd2ddd51",
        "7cec85d7bea7798e",
        "31bf3856ad364e35",
        "adb9793829ddae60",
    ];

    private static bool IsFrameworkKey(ImmutableArray<byte> token)
    {
        if (token.IsDefaultOrEmpty || token.Length != 8) return false;

        var hex = string.Concat(token.Select(static z => z.ToString("x2", System.Globalization.CultureInfo.InvariantCulture)));
        foreach (var known in frameworkKeyTokens)
        {
            if (string.Equals(known, hex, StringComparison.Ordinal)) return true;
        }

        return false;
    }

    private static bool Names(AssemblyFilterData selection, IAssemblySymbol assembly)
    {
        foreach (var name in selection.Assemblies)
        {
            if (string.Equals(name, assembly.MetadataName, StringComparison.Ordinal)) return true;
        }

        return false;
    }

    private static EquatableArray<string> Patterns(AnalyzerConfigOptions options, string key)
    {
        if (!options.TryGetValue(key, out var value) || string.IsNullOrWhiteSpace(value)) return EquatableArray<string>.Empty;

        var patterns = ImmutableArray.CreateBuilder<string>();
        foreach (var part in value.Split([';', ','], StringSplitOptions.RemoveEmptyEntries))
        {
            var pattern = part.Trim();
            if (pattern.Length > 0) patterns.Add(pattern);
        }

        return new(patterns.ToImmutable());
    }

    private enum Decision
    {
        InScope,
        FrameworkDefault,
        Excluded,
    }
}

/// <summary>Wildcard matching over a whole name: <c>*</c> is any run of characters, <c>?</c> exactly one, ordinal and case-insensitive.</summary>
internal static class Glob
{
    public static bool IsMatch(string pattern, string text)
    {
        int p = 0, t = 0, star = -1, resume = 0;
        while (t < text.Length)
        {
            if (p < pattern.Length && ( pattern[p] == '?' || char.ToUpperInvariant(pattern[p]) == char.ToUpperInvariant(text[t]) ))
            {
                p++;
                t++;
            }
            else if (p < pattern.Length && pattern[p] == '*')
            {
                star = p++;
                resume = t;
            }
            else if (star >= 0)
            {
                p = star + 1;
                t = ++resume;
            }
            else
            {
                return false;
            }
        }

        while (p < pattern.Length && pattern[p] == '*') p++;

        return p == pattern.Length;
    }
}
