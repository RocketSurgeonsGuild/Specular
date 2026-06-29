using System.ComponentModel;
using System.Security.Cryptography;
using System.Text;

namespace Indago;

/// <summary>
///   Support class for Indago, this is used to hash the argument expression for the caller.
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public static class IndagoSupport
{
    /// <summary>
    ///     Method used to ensure the argument expression is hashed correctly each time.
    /// </summary>
    /// <param name="argumentExpression"></param>
    /// <returns></returns>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static string GetArgumentExpressionHash(string argumentExpression)
    {
#if !NETSTANDARD2_0
        ArgumentNullException.ThrowIfNull(argumentExpression);
#endif
        var expression = string.Concat(
            argumentExpression
               .Split(['\n', '\r', ' ', '\t'], StringSplitOptions.RemoveEmptyEntries)
               .Select(z => z.Trim())
        );
#if NETSTANDARD2_0
        using var hasher = MD5.Create();
        return Convert.ToBase64String(hasher.ComputeHash(Encoding.UTF8.GetBytes(expression)));
#else
        return Convert.ToBase64String(MD5.HashData(Encoding.UTF8.GetBytes(expression)));
#endif
    }
}
