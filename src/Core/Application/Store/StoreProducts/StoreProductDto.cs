namespace FSH.WebApi.Application.Store.StoreProducts;

public class StoreProductDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public decimal BasePrice { get; set; }
    public int StockUnits { get; set; }
    public string? UnitType { get; set; }
    public decimal M2 { get; set; }
    public string? ImagePath { get; set; }
}