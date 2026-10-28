# ExcludeFromSpecularAttribute

- Kind: `class`
- Package: [Specular](/Specular/pr-preview/pr-340/api/specular.md)
- Version: `0.3.2-pr.4`
- Namespace: `Specular.Abstractions`
- Target framework: `net10.0`
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

- [ExcludeFromSpecularAttribute](/Specular/pr-preview/pr-340/api/specular/excludefromspecularattribute/constructors.md#constructor) -- Exclude the given assembly from compiled type provider resolution.
