# SpecularHashAttribute Constructors

- Package: [Specular](/Specular/api/specular.md)
- Type: [SpecularHashAttribute](/Specular/api/specular/specularhashattribute.md)
- Kind: `Constructors`
- Members: `1`

Attribute used to define the compiled type provider for a given assembly

<a id="constructor"></a>
<a id="constructor-string"></a>

## SpecularHashAttribute(string)

- Name: `Constructor(string)`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.main.Sha.b164b04e5bca8fcc8720179d844e43e9b939eae8/src/Specular/Abstractions/SpecularHashAttribute.cs#L8-L13)

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
