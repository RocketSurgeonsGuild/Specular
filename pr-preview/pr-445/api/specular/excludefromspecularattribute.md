# ExcludeFromSpecularAttribute

- Kind: `class`
- Package: [Specular](/Specular/pr-preview/pr-445/api/specular.md)
- Version: `0.4.2-pr.64`
- Namespace: `Specular.Abstractions`
- Target framework: `net10.0`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-445-merge.Sha.148444cdd6691571fa2f9e5421c54e07d5203dfc/src/Specular/Abstractions/ExcludeFromSpecularAttribute.cs)
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

- [ExcludeFromSpecularAttribute](/Specular/pr-preview/pr-445/api/specular/excludefromspecularattribute/constructors.md#constructor) -- Exclude the given assembly from compiled type provider resolution.
