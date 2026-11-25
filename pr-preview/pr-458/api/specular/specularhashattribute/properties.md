# SpecularHashAttribute Properties

- Package: [Specular](/Specular/pr-preview/pr-458/api/specular.md)
- Type: [SpecularHashAttribute](/Specular/pr-preview/pr-458/api/specular/specularhashattribute.md)
- Kind: `Properties`
- Members: `1`

Attribute used to define the compiled type provider for a given assembly

<a id="generatedhash"></a>

## GeneratedHash

- Name: `GeneratedHash`
- Modifiers: `get`
- Returns: `string`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-458-merge.Sha.20db85ca70c7d1c6b702a37793b1ebebbea6d081/src/Specular/Abstractions/SpecularHashAttribute.cs#L13)

The generated hash to be used for cache busting

```csharp
public string GeneratedHash { get; }
```
