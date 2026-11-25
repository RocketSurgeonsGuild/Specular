# SpecularHashAttribute

- Kind: `class`
- Package: [Specular](/Specular/pr-preview/pr-459/api/specular.md)
- Version: `0.4.3-pr.12`
- Namespace: `Specular.Abstractions`
- Target framework: `net10.0`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-459-merge.Sha.ca8deea26180382e1d58a2391f0e4bcbcc0b2c9d/src/Specular/Abstractions/SpecularHashAttribute.cs)
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

- [SpecularHashAttribute(string)](/Specular/pr-preview/pr-459/api/specular/specularhashattribute/constructors.md#constructor-string) -- Attribute used to define the compiled type provider for a given assembly

## Properties

- [GeneratedHash](/Specular/pr-preview/pr-459/api/specular/specularhashattribute/properties.md#generatedhash) : `string` `get` -- The generated hash to be used for cache busting
