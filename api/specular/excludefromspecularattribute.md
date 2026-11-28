# ExcludeFromSpecularAttribute

- Kind: `class`
- Package: [Specular](/Specular/api/specular.md)
- Version: `0.4.3-beta.11`
- Namespace: `Specular.Abstractions`
- Target framework: `net10.0`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.main.Sha.7198f229aa17bd9ae0e7bbde62dbec64e80e7d0a/src/Specular/Abstractions/ExcludeFromSpecularAttribute.cs)
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
