namespace Specular.Samples.Catalog;

/// <summary>Looks up catalog products. Discovered by interface-matching.</summary>
public interface IProductService
{
    string Describe();
}

/// <summary>Looks up catalog categories. Discovered by interface-matching.</summary>
public interface ICategoryService
{
    string Describe();
}

/// <summary>Reports on-hand inventory. Discovered by interface-matching.</summary>
public interface IInventoryService
{
    string Describe();
}

/// <summary>Computes catalog prices. Discovered by interface-matching.</summary>
public interface IPricingService
{
    string Describe();
}

/// <inheritdoc />
internal sealed class ProductService : IProductService
{
    /// <inheritdoc />
    public string Describe() => "catalog products";
}

/// <inheritdoc />
internal sealed class CategoryService : ICategoryService
{
    /// <inheritdoc />
    public string Describe() => "catalog categories";
}

/// <inheritdoc />
internal sealed class InventoryService : IInventoryService
{
    /// <inheritdoc />
    public string Describe() => "catalog inventory";
}

/// <inheritdoc />
internal sealed class PricingService : IPricingService
{
    /// <inheritdoc />
    public string Describe() => "catalog pricing";
}
