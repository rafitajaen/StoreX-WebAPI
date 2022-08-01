namespace FSH.WebApi.Application.Store.InvoiceProducts;

public class InvoiceProductDetailsDto : IDto
{
    public Guid InvoiceId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}