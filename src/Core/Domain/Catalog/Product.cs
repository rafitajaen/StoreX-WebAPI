namespace FSH.WebApi.Domain.Catalog;

public class Product : AuditableEntity, IAggregateRoot
{
    public string Name { get; private set; } = default!;
    public string? Description { get; private set; }
    public decimal BasePrice { get; private set; }
    public int StockUnits { get; private set; }
    public string? UnitType { get; private set; }
    public decimal M2 { get; private set; }
    public Guid BrandId { get; private set; }
    public virtual Brand Brand { get; private set; } = default!;
    public string? ImagePath { get; private set; }
    public virtual ICollection<OrderProduct> OrderProducts { get; private set; } = default!;

    public Product()
    {
        // Only needed for working with dapper (See GetProductViaDapperRequest)
        // If you're not using dapper it's better to remove this constructor.
    }

    public Product(string name, string? description, decimal basePrice, int stockUnits, string? unitType, decimal m2, Guid brandId, string? imagePath)
    {
        Name = name;
        Description = description;
        BasePrice = basePrice;
        StockUnits = stockUnits;
        UnitType = unitType;
        M2 = m2;
        BrandId = brandId;
    }

    public Product Update(string name, string? description, decimal? basePrice, int? stockUnits, string? unitType, decimal? m2, Guid? brandId, string? imagePath)
    {
        if (name is not null && Name?.Equals(name) is not true) Name = name;
        if (description is not null && Description?.Equals(description) is not true) Description = description;
        if (basePrice.HasValue && BasePrice != basePrice) BasePrice = basePrice.Value;
        if (stockUnits.HasValue && StockUnits != stockUnits) StockUnits = stockUnits.Value;
        if (unitType is not null && UnitType?.Equals(unitType) is not true) UnitType = unitType;
        if (m2.HasValue && M2 != m2) M2 = m2.Value;
        if (brandId.HasValue && brandId.Value != Guid.Empty && !BrandId.Equals(brandId.Value)) BrandId = brandId.Value;
        if (imagePath is not null && ImagePath?.Equals(imagePath) is not true) ImagePath = imagePath;
        return this;
    }

    public Product ClearImagePath()
    {
        ImagePath = string.Empty;
        return this;
    }
}