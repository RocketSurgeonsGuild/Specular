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
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-431-merge.Sha.216015f7483e4a22dec13765e68d4ac57c482acf/src/Specular/Abstractions/SpecularHashAttribute.cs#L13)

The generated hash to be used for cache busting

```csharp
public string GeneratedHash { get; }
```
