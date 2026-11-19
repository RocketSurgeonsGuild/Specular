# SpecularHashAttribute Properties

- Package: [Specular](/Specular/pr-preview/pr-441/api/specular.md)
- Type: [SpecularHashAttribute](/Specular/pr-preview/pr-441/api/specular/specularhashattribute.md)
- Kind: `Properties`
- Members: `1`

Attribute used to define the compiled type provider for a given assembly

<a id="generatedhash"></a>

## GeneratedHash

- Name: `GeneratedHash`
- Modifiers: `get`
- Returns: `string`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-441-merge.Sha.483f529455000ea72a283b9f607726cf95ec03ef/src/Specular/Abstractions/SpecularHashAttribute.cs#L13)

The generated hash to be used for cache busting

```csharp
public string GeneratedHash { get; }
```
