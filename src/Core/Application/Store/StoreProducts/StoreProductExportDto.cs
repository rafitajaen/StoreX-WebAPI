namespace FSH.WebApi.Application.Store.StoreProducts;

public class StoreProductExportDto : IDto
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public decimal BasePrice { get; set; } = default!;
    public int StockUnits { get; set; } = default!;
    public string? UnitType { get; set; } = default!;
    public decimal M2 { get; set; } = default!;
}