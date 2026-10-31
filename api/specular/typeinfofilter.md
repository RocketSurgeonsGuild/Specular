# TypeInfoFilter

- Kind: `enum`
- Package: [Specular](/Specular/api/specular.md)
- Version: `0.4.1-beta.1`
- Namespace: `Specular.Abstractions`
- Target framework: `net10.0`

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
