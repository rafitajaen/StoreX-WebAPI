using FSH.WebApi.Application.Store.StoreProducts;

namespace FSH.WebApi.Application.Store.InvoiceProducts;

public class InvoiceProductDetailsDto : IDto
{
    public Guid InvoiceId { get; set; }
    public string InvoiceName { get; set; } = default!;
    public StoreProductDto Product { get; set; } = default!;
    public int Quantity { get; set; }
}