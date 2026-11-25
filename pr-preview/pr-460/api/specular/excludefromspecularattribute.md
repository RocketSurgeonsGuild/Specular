# ExcludeFromSpecularAttribute

- Kind: `class`
- Package: [Specular](/Specular/pr-preview/pr-460/api/specular.md)
- Version: `0.4.3-pr.13`
- Namespace: `Specular.Abstractions`
- Target framework: `net10.0`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-460-merge.Sha.d41c5fa5888078044d4dab2d9f0a772bbcacc924/src/Specular/Abstractions/ExcludeFromSpecularAttribute.cs)
- Inherits: `Attribute`

Exclude the given assembly from compiled type provider resolution.

## Definition

```csharp
namespace Specular.Abstractions;

public class ExcludeFromSpecularAttribute
    : System.Attribute
{
    // ...
}
```

## Remarks

This assembly will still have access to compiled types, but nothing will be resolved internally.

## Constructors

- [ExcludeFromSpecularAttribute](/Specular/pr-preview/pr-460/api/specular/excludefromspecularattribute/constructors.md#constructor) -- Exclude the given assembly from compiled type provider resolution.
