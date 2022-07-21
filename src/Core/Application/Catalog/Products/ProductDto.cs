namespace FSH.WebApi.Application.Catalog.Products;

public class ProductDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public decimal BasePrice { get; set; }
    public int StockUnits { get; set; }
    public string? UnitType { get; set; }
    public decimal M2 { get; set; }
    public Guid BrandId { get; set; }
    public string BrandName { get; set; } = default!;
    public string? ImagePath { get; set; }
}