# ExcludeFromSpecularAttribute

- Kind: `class`
- Package: [Specular](/Specular/api/specular.md)
- Version: `0.4.2-beta.52`
- Namespace: `Specular.Abstractions`
- Target framework: `net10.0`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.main.Sha.8bcc62d19c09284f6923e54e588877e41baf4070/src/Specular/Abstractions/ExcludeFromSpecularAttribute.cs)
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

- [ExcludeFromSpecularAttribute](/Specular/api/specular/excludefromspecularattribute/constructors.md#constructor) -- Exclude the given assembly from compiled type provider resolution.
