# TypeInfoFilter

- Kind: `enum`
- Package: [Specular](/Specular/api/specular.md)
- Version: `0.4.2-beta.64`
- Namespace: `Specular.Abstractions`
- Target framework: `net10.0`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.main.Sha.92ad0972fc551fb7fe4da29ef78029c66b11b489/src/Specular/Abstractions/TypeInfoFilter.cs)

Enumeration for possible type information filters.

## Definition

```csharp
namespace Specular.Abstractions;

public enum TypeInfoFilter
```

## Enum Members

| Name | Value | Description |
| --- | --- | --- |
| Unknown | 0 | The type is unknown. |
| Abstract | 1 | The type is abstract. |
| Visible | 2 | The type is visible. |
| ValueType | 3 | The type is a value type. |
| Sealed | 5 | The type is sealed. |
| GenericType | 6 | The type is a generic type. |
| Static | -2 | The type is static. |
