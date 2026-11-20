# ExcludeFromSpecularAttribute Constructors

- Package: [Specular](/Specular/pr-preview/pr-445/api/specular.md)
- Type: [ExcludeFromSpecularAttribute](/Specular/pr-preview/pr-445/api/specular/excludefromspecularattribute.md)
- Kind: `Constructors`
- Members: `1`

Exclude the given assembly from compiled type provider resolution.

<a id="constructor"></a>

## ExcludeFromSpecularAttribute

- Name: `Constructor`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-445-merge.Sha.148444cdd6691571fa2f9e5421c54e07d5203dfc/src/Specular/Abstractions/ExcludeFromSpecularAttribute.cs#L9)

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
