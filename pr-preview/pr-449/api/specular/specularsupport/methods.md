# SpecularSupport Methods

- Package: [Specular](/Specular/pr-preview/pr-449/api/specular.md)
- Type: [SpecularSupport](/Specular/pr-preview/pr-449/api/specular/specularsupport.md)
- Kind: `Methods`
- Members: `1`

Support class for Specular, this is used to hash the argument expression for the caller.

<a id="getargumentexpressionhash"></a>
<a id="getargumentexpressionhash-string"></a>

## GetArgumentExpressionHash(string)

- Name: `GetArgumentExpressionHash(string)`
- Modifiers: `static`
- Returns: `string`
- Source: [GitHub](https://github.com/RocketSurgeonsGuild/Specular/blob/Branch.pull-449-merge.Sha.a4e7451225f525b22271723fffa01211c276f62e/src/Specular/SpecularSupport.cs#L22-L33)

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
