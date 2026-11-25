using Microsoft.CodeAnalysis;

namespace Specular.Scanning;

/// <summary>A type a scan matched, described by value so it can be cached and compared.</summary>
/// <param name="AssemblyName">The containing assembly's metadata name.</param>
/// <param name="MetadataName">The full metadata name (nested types use <c>+</c>, generics carry their arity).</param>
/// <param name="Kind">The type kind.</param>
/// <param name="DeclaredAccessibility">The declared accessibility.</param>
/// <param name="IsAccessible">Whether the compilation can reference the type (declared accessibility, containing types and InternalsVisibleTo).</param>
/// <param name="Arity">The number of generic type parameters.</param>
#if SPECULAR_SCANNING_SOURCES
internal
#else
public
#endif
sealed record ScannedType(
    string AssemblyName,
    string MetadataName,
    TypeKind Kind,
    Accessibility DeclaredAccessibility,
    bool IsAccessible,
    int Arity
)
{
    /// <summary>
    ///     The type's name as C# source refers to it, <c>global::</c>-qualified, so a generator can emit it without resolving the result
    ///     back to symbols: <see cref="SymbolDisplayFormat.FullyQualifiedFormat" /> applied to the type's definition, except that
    ///     special types are named like any other type (<c>global::System.Int32</c>, <c>global::System.Nullable&lt;T&gt;</c> and
    ///     <c>global::System.ValueTuple&lt;T1, T2&gt;</c>, not <c>int</c>, <c>T?</c> and <c>(T1, T2)</c>).
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         Nested types are joined with <c>.</c> and keyword-named types are escaped: <c>global::Ns.Outer.Inner</c>,
    ///         <c>global::Ns.@class</c>. Generic types, and types nested in them, list their type parameter names:
    ///         <c>global::Ns.Handler&lt;TRequest, TResponse&gt;</c>, <c>global::Ns.Outer&lt;T&gt;.Inner</c>. That is the form to close with
    ///         your own type arguments, or to use inside the generic type's declaration.
    ///     </para>
    ///     <para>
    ///         <c>typeof</c> of an open generic needs the unbound form instead (<c>typeof(global::Ns.Handler&lt;,&gt;)</c>): keep only the
    ///         commas between each <c>&lt;</c> and <c>&gt;</c>. Type parameter names never contain angle brackets, so the brackets don't
    ///         nest.
    ///     </para>
    ///     <para>
    ///         Every scan sets it. A <see cref="ScannedType" /> created with the positional constructor has an empty name until it is set.
    ///     </para>
    /// </remarks>
    public string FullyQualifiedName { get; init; } = string.Empty;
}
