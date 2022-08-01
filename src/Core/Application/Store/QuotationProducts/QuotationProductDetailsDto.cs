namespace FSH.WebApi.Application.Store.QuotationProducts;

public class QuotationProductDetailsDto : IDto
{
    public Guid QuotationId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}