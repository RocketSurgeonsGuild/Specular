# TypeKindFilter

- Kind: `enum`
- Package: [Specular](/Specular/pr-preview/pr-458/api/specular.md)
- Version: `0.4.3-pr.51`
- Namespace: `Specular.Abstractions`
- Target framework: `net10.0`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-458-merge.Sha.e349b4d07697a27d2f7d2f0edd92c5e34f9b8f68/src/Specular/Abstractions/TypeKindFilter.cs)

Enumeration for possible kinds of type symbols.

## Definition

```csharp
namespace Specular.Abstractions;

public enum TypeKindFilter
```

## Enum Members

| Name | Value | Description |
| --- | --- | --- |
| Array | 1 | Type is an array type. |
| Class | 2 | Type is a class. |
| Delegate | 3 | Type is a delegate. |
| Enum | 5 | Type is an enumeration. |
| Interface | 7 | Type is an interface. |
| Struct | 10 | Type is a C# struct or VB Structure |
