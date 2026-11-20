# SpecularSupport Methods

- Package: [Specular](/Specular/v0.4.2/api/specular.md)
- Type: [SpecularSupport](/Specular/v0.4.2/api/specular/specularsupport.md)
- Kind: `Methods`
- Members: `1`

Support class for Specular, this is used to hash the argument expression for the caller.

<a id="getargumentexpressionhash"></a>
<a id="getargumentexpressionhash-string"></a>

## GetArgumentExpressionHash(string)

- Name: `GetArgumentExpressionHash(string)`
- Modifiers: `static`
- Returns: `string`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.main.Sha.cfc4e1e69ff7a6dcb9388579cde1a5879d0e3e7e/src/Specular/SpecularSupport.cs#L22-L33)

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
