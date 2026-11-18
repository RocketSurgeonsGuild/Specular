# SpecularSupport Methods

- Package: [Specular](/Specular/api/specular.md)
- Type: [SpecularSupport](/Specular/api/specular/specularsupport.md)
- Kind: `Methods`
- Members: `1`

Support class for Specular, this is used to hash the argument expression for the caller.

<a id="getargumentexpressionhash"></a>
<a id="getargumentexpressionhash-string"></a>

## GetArgumentExpressionHash(string)

- Name: `GetArgumentExpressionHash(string)`
- Modifiers: `static`
- Returns: `string`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.main.Sha.d4a702693c72a74c7d2f15946a641a7a58285f09/src/Specular/SpecularSupport.cs#L22-L33)

Method used to ensure the argument expression is hashed correctly each time.

```csharp
public static class SpecularSupport
{
    public static string GetArgumentExpressionHash(
        string argumentExpression)
    {
        // ...
    }
}
```

## Parameters

- `argumentExpression` (`string`)
