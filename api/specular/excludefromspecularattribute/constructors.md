# ExcludeFromSpecularAttribute Constructors

- Package: [Specular](/Specular/api/specular.md)
- Type: [ExcludeFromSpecularAttribute](/Specular/api/specular/excludefromspecularattribute.md)
- Kind: `Constructors`
- Members: `1`

Exclude the given assembly from compiled type provider resolution.

<a id="constructor"></a>

## ExcludeFromSpecularAttribute

- Name: `Constructor`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.main.Sha.d4a702693c72a74c7d2f15946a641a7a58285f09/src/Specular/Abstractions/ExcludeFromSpecularAttribute.cs#L9)

Exclude the given assembly from compiled type provider resolution.

```csharp
public class ExcludeFromSpecularAttribute
{
    public ExcludeFromSpecularAttribute()
    {
        // ...
    }
}
```

## Remarks

This assembly will still have access to compiled types, but nothing will be resolved internally.
