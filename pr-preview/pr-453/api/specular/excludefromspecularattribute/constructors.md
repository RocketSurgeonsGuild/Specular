# ExcludeFromSpecularAttribute Constructors

- Package: [Specular](/Specular/pr-preview/pr-453/api/specular.md)
- Type: [ExcludeFromSpecularAttribute](/Specular/pr-preview/pr-453/api/specular/excludefromspecularattribute.md)
- Kind: `Constructors`
- Members: `1`

Exclude the given assembly from compiled type provider resolution.

<a id="constructor"></a>

## ExcludeFromSpecularAttribute

- Name: `Constructor`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-453-merge.Sha.507a376b7125f9abab9ee28903b20cf32ac8ece3/src/Specular/Abstractions/ExcludeFromSpecularAttribute.cs#L9)

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
