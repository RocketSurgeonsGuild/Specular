namespace Indago.Analyzers.Descriptors;

[SuppressMessage("Design", "CA1008:Enums should have zero value", Justification = "Values are persisted in the ctpjson cache; renumbering would break cache compatibility.")]
public enum NamespaceFilter
{
    Exact = 1,
    In = 2,
    NotIn = 3,
}
