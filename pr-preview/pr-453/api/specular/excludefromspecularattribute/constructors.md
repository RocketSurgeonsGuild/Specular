# ExcludeFromSpecularAttribute Constructors

- Package: [Specular](/Specular/pr-preview/pr-453/api/specular.md)
- Type: [ExcludeFromSpecularAttribute](/Specular/pr-preview/pr-453/api/specular/excludefromspecularattribute.md)
- Kind: `Constructors`
- Members: `1`

Exclude the given assembly from compiled type provider resolution.

<a id="constructor"></a>

## ExcludeFromSpecularAttribute

- Name: `Constructor`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-453-merge.Sha.ca2de83094300ad9fca8d4b18dd49afd62fc3ac1/src/Specular/Abstractions/ExcludeFromSpecularAttribute.cs#L9)

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
