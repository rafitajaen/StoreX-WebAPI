namespace FSH.WebApi.Application.Store.StoreProducts;

public class StoreProductExportDto : IDto
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; } = default!;
    public decimal BasePrice { get; set; }
    public int StockUnits { get; set; }
    public int StockAlert { get; set; }
    public string? UnitType { get; set; } = default!;
    public decimal M2 { get; set; }
}