# SpecularHashAttribute Constructors

- Package: [Specular](/Specular/pr-preview/pr-427/api/specular.md)
- Type: [SpecularHashAttribute](/Specular/pr-preview/pr-427/api/specular/specularhashattribute.md)
- Kind: `Constructors`
- Members: `1`

Attribute used to define the compiled type provider for a given assembly

<a id="constructor"></a>
<a id="constructor-string"></a>

## SpecularHashAttribute(string)

- Name: `Constructor(string)`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-427-merge.Sha.a4c159b423ec8329cb17d6f587fab98a92167670/src/Specular/Abstractions/SpecularHashAttribute.cs#L8-L13)

Attribute used to define the compiled type provider for a given assembly

```csharp
public sealed class SpecularHashAttribute
{
    public SpecularHashAttribute(
        string generatedHash)
    {
        // ...
    }
}
```

## Parameters

- `generatedHash` (`string`)
