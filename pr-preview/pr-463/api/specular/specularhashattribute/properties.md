# SpecularHashAttribute Properties

- Package: [Specular](/Specular/pr-preview/pr-463/api/specular.md)
- Type: [SpecularHashAttribute](/Specular/pr-preview/pr-463/api/specular/specularhashattribute.md)
- Kind: `Properties`
- Members: `1`

Attribute used to define the compiled type provider for a given assembly

<a id="generatedhash"></a>

## GeneratedHash

- Name: `GeneratedHash`
- Modifiers: `get`
- Returns: `string`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-463-merge.Sha.e15c1ea5e8128370add189f972645c6989282090/src/Specular/Abstractions/SpecularHashAttribute.cs#L13)

The generated hash to be used for cache busting

```csharp
public string GeneratedHash { get; }
```
