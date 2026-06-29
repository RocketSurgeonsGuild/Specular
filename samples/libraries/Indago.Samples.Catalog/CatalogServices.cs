namespace Indago.Samples.Catalog;

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
public sealed class ProductService : IProductService
{
    /// <inheritdoc />
    public string Describe() => "catalog products";
}

/// <inheritdoc />
public sealed class CategoryService : ICategoryService
{
    /// <inheritdoc />
    public string Describe() => "catalog categories";
}

/// <inheritdoc />
public sealed class InventoryService : IInventoryService
{
    /// <inheritdoc />
    public string Describe() => "catalog inventory";
}

/// <inheritdoc />
public sealed class PricingService : IPricingService
{
    /// <inheritdoc />
    public string Describe() => "catalog pricing";
}
