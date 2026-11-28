# TypeInfoFilter

- Kind: `enum`
- Package: [Specular](/Specular/pr-preview/pr-474/api/specular.md)
- Version: `0.4.3-pr.25`
- Namespace: `Specular.Abstractions`
- Target framework: `net10.0`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-474-merge.Sha.1aeada8e35256c38d58c0d80324e7d9dd7e6c8f3/src/Specular/Abstractions/TypeInfoFilter.cs)

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
