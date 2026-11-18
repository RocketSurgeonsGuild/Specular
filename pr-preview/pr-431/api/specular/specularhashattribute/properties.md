# SpecularHashAttribute Properties

- Package: [Specular](/Specular/pr-preview/pr-431/api/specular.md)
- Type: [SpecularHashAttribute](/Specular/pr-preview/pr-431/api/specular/specularhashattribute.md)
- Kind: `Properties`
- Members: `1`

Attribute used to define the compiled type provider for a given assembly

<a id="generatedhash"></a>

## GeneratedHash

- Name: `GeneratedHash`
- Modifiers: `get`
- Returns: `string`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-431-merge.Sha.1fc516ea1b9500fed12a71dad9d2c95bf1783a47/src/Specular/Abstractions/SpecularHashAttribute.cs#L13)

The generated hash to be used for cache busting

```csharp
public string GeneratedHash { get; }
```
