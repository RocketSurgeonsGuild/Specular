# SpecularSupport Methods

- Package: [Specular](/Specular/pr-preview/pr-458/api/specular.md)
- Type: [SpecularSupport](/Specular/pr-preview/pr-458/api/specular/specularsupport.md)
- Kind: `Methods`
- Members: `1`

Support class for Specular, this is used to hash the argument expression for the caller.

<a id="getargumentexpressionhash"></a>
<a id="getargumentexpressionhash-string"></a>

## GetArgumentExpressionHash(string)

- Name: `GetArgumentExpressionHash(string)`
- Modifiers: `static`
- Returns: `string`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-458-merge.Sha.f398584acaebb436a7dc6af0a2c4966988fca3fe/src/Specular/SpecularSupport.cs#L22-L33)

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
