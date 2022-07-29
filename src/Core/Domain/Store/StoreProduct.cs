namespace FSH.WebApi.Domain.Store;

public class StoreProduct : AuditableEntity, IAggregateRoot
{
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public decimal BasePrice { get; private set; }
    public int StockUnits { get; private set; }
    public int StockAlert { get; private set; }
    public string? UnitType { get; private set; }
    public decimal M2 { get; private set; }
    public string? ImagePath { get; private set; }
    public virtual ICollection<OrderProduct> OrderProducts { get; private set; } = default!;

    public StoreProduct(string name, string? description, decimal basePrice, int stockUnits, int stockAlert, string? unitType, decimal m2, string? imagePath)
    {
        Name = name;
        Description = description;
        BasePrice = basePrice;
        StockUnits = stockUnits;
        StockAlert = stockAlert;
        UnitType = unitType;
        M2 = m2;
    }

    public StoreProduct Update(string name, string? description, decimal? basePrice, int? stockUnits, int? stockAlert, string? unitType, decimal? m2, string? imagePath)
    {
        if (name is not null && Name?.Equals(name) is not true) Name = name;
        if (description is not null && Description?.Equals(description) is not true) Description = description;
        if (basePrice.HasValue && BasePrice != basePrice) BasePrice = basePrice.Value;
        if (stockUnits.HasValue && StockUnits != stockUnits) StockUnits = stockUnits.Value;
        if (stockAlert.HasValue && StockAlert != stockAlert) StockAlert = stockAlert.Value;
        if (unitType is not null && UnitType?.Equals(unitType) is not true) UnitType = unitType;
        if (m2.HasValue && M2 != m2) M2 = m2.Value;
        if (imagePath is not null && ImagePath?.Equals(imagePath) is not true) ImagePath = imagePath;
        return this;
    }

    public StoreProduct ClearImagePath()
    {
        ImagePath = string.Empty;
        return this;
    }
}