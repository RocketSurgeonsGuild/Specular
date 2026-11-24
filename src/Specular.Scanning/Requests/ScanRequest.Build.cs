using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using Microsoft.CodeAnalysis;

namespace Specular.Scanning;

public partial record ScanRequest
{
    // Bump when the canonical form below changes, so old cache entries can never alias new requests.
    private const string CacheKeyFormat = "specular-scan-request/1";

    private static partial ScanRequest BuildCore(Compilation compilation, Action<IAssemblySymbolSelector> select)
    {
        SelectorArguments.ThrowIfNull(compilation, nameof(compilation));
        SelectorArguments.ThrowIfNull(select, nameof(select));

        var selector = new SymbolSelector(compilation);
        select(selector);
        return selector.Complete();
    }

    // The key must survive process restarts, so it hashes a canonical text form rather than using GetHashCode (randomized per
    // process for strings). Strings are length-prefixed and arrays count-prefixed, so no two different requests share a form.
    private partial string GetCacheKeyCore()
    {
        var key = new CanonicalWriter();
        key.Text(CacheKeyFormat);
        key.Number((int)Kind);

        key.Flag(Assemblies.AllAssemblies);
        key.Flag(Assemblies.IncludeSystemAssemblies);
        key.Texts(Assemblies.Assemblies);
        key.Texts(Assemblies.NotAssemblies);
        key.Texts(Assemblies.DependenciesOf);

        if (Types is not { } types)
        {
            key.Marker('-');
        }
        else
        {
            key.Marker('+');
            WriteTypes(key, types);
        }

        using var sha = SHA256.Create();
        var hash = sha.ComputeHash(Encoding.UTF8.GetBytes(key.ToString()));
        // Base64url without padding: safe in file names, URLs and JSON keys.
        return Convert.ToBase64String(hash).TrimEnd('=').Replace('+', '-').Replace('/', '_');
    }

    private static void WriteTypes(CanonicalWriter key, TypeFilterData types)
    {
        key.Flag(types.PublicOnly);

        key.Count(types.AssignableTo.Count);
        foreach (var clause in types.AssignableTo)
        {
            key.Flag(clause.Include);
            WriteTypeReferences(key, clause.Types);
        }

        key.Count(types.Attributes.Count);
        foreach (var clause in types.Attributes)
        {
            key.Flag(clause.Include);
            key.Flag(clause.Any);
            WriteTypeReferences(key, clause.Attributes);
        }

        key.Count(types.AttributeNames.Count);
        foreach (var clause in types.AttributeNames)
        {
            key.Flag(clause.Include);
            key.Texts(clause.AttributeNames);
        }

        key.Count(types.Namespaces.Count);
        foreach (var clause in types.Namespaces)
        {
            key.Number((int)clause.Filter);
            key.Texts(clause.Namespaces);
        }

        key.Count(types.Names.Count);
        foreach (var clause in types.Names)
        {
            key.Flag(clause.Include);
            key.Number((int)clause.Filter);
            key.Texts(clause.Names);
        }

        key.Count(types.TypeKinds.Count);
        foreach (var clause in types.TypeKinds)
        {
            key.Flag(clause.Include);
            key.Count(clause.TypeKinds.Count);
            foreach (var kind in clause.TypeKinds) key.Number((int)kind);
        }

        key.Count(types.TypeInfos.Count);
        foreach (var clause in types.TypeInfos)
        {
            key.Flag(clause.Include);
            key.Count(clause.TypeInfos.Count);
            foreach (var info in clause.TypeInfos) key.Number((int)info);
        }

        key.Count(types.CustomFilters.Count);
        foreach (var filter in types.CustomFilters)
        {
            // The type takes part because CustomTypeFilter equality does: two filter classes may share a key text.
            var filterType = filter.GetType();
            key.Text(filterType.FullName ?? filterType.Name);
            key.Text(filter.CacheKey);
        }
    }

    private static void WriteTypeReferences(CanonicalWriter key, EquatableArray<TypeReference> references)
    {
        key.Count(references.Count);
        foreach (var reference in references)
        {
            key.Text(reference.AssemblyName);
            key.Text(reference.MetadataName);
            key.Flag(reference.UnboundGeneric);
        }
    }

    private sealed class CanonicalWriter
    {
        private readonly StringBuilder _builder = new();

        public void Marker(char marker) => _builder.Append(marker).Append(';');

        public void Flag(bool value) => Marker(value ? '1' : '0');

        public void Number(int value) => _builder.Append(value.ToString(CultureInfo.InvariantCulture)).Append(';');

        public void Count(int count) => _builder.Append('#').Append(count.ToString(CultureInfo.InvariantCulture)).Append(';');

        public void Text(string value) =>
            _builder.Append(value.Length.ToString(CultureInfo.InvariantCulture)).Append(':').Append(value).Append(';');

        public void Texts(EquatableArray<string> values)
        {
            Count(values.Count);
            foreach (var value in values) Text(value);
        }

        public override string ToString() => _builder.ToString();
    }
}
