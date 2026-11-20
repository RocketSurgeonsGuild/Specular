# SpecularHashAttribute Constructors

- Package: [Specular](/Specular/pr-preview/pr-444/api/specular.md)
- Type: [SpecularHashAttribute](/Specular/pr-preview/pr-444/api/specular/specularhashattribute.md)
- Kind: `Constructors`
- Members: `1`

Attribute used to define the compiled type provider for a given assembly

<a id="constructor"></a>
<a id="constructor-string"></a>

## SpecularHashAttribute(string)

- Name: `Constructor(string)`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-444-merge.Sha.91db23f0a6dd2aea896fb5788d96dd2d2a023c25/src/Specular/Abstractions/SpecularHashAttribute.cs#L8-L13)

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
