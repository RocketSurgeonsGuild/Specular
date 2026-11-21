# SpecularHashAttribute

- Kind: `class`
- Package: [Specular](/Specular/pr-preview/pr-451/api/specular.md)
- Version: `0.4.3-pr.3`
- Namespace: `Specular.Abstractions`
- Target framework: `net10.0`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-451-merge.Sha.3976afc911b40cb4a244b415afe8ec88dc2bcb05/src/Specular/Abstractions/SpecularHashAttribute.cs)
- Inherits: `Attribute`

Attribute used to define the compiled type provider for a given assembly

## Definition

```csharp
namespace Specular.Abstractions;

public sealed class SpecularHashAttribute
    : System.Attribute
{
    // ...
}
```

## Constructors

- [SpecularHashAttribute(string)](/Specular/pr-preview/pr-451/api/specular/specularhashattribute/constructors.md#constructor-string) -- Attribute used to define the compiled type provider for a given assembly

## Properties

- [GeneratedHash](/Specular/pr-preview/pr-451/api/specular/specularhashattribute/properties.md#generatedhash) : `string` `get` -- The generated hash to be used for cache busting
