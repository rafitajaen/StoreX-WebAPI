using FSH.WebApi.Application.Store.StoreProducts;

namespace FSH.WebApi.Application.Store.QuotationProducts;

public class QuotationProductDetailsDto : IDto
{
    public Guid QuotationId { get; set; }
    public string QuotationName { get; set; } = default!;
    public StoreProductDto Product { get; set; } = default!;
    public int Quantity { get; set; }
}