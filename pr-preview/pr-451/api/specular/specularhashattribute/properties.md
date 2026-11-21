# SpecularHashAttribute Properties

- Package: [Specular](/Specular/pr-preview/pr-451/api/specular.md)
- Type: [SpecularHashAttribute](/Specular/pr-preview/pr-451/api/specular/specularhashattribute.md)
- Kind: `Properties`
- Members: `1`

Attribute used to define the compiled type provider for a given assembly

<a id="generatedhash"></a>

## GeneratedHash

- Name: `GeneratedHash`
- Modifiers: `get`
- Returns: `string`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-451-merge.Sha.3976afc911b40cb4a244b415afe8ec88dc2bcb05/src/Specular/Abstractions/SpecularHashAttribute.cs#L13)

The generated hash to be used for cache busting

```csharp
public string GeneratedHash { get; }
```
