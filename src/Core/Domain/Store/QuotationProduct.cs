namespace FSH.WebApi.Domain.Store;
public class QuotationProduct : AuditableEntity, IAggregateRoot
{
    public Guid QuotationId { get; private set; }
    public Quotation Quotation { get; private set; } = default!;
    public Guid ProductId { get; private set; }
    public StoreProduct Product { get; private set; } = default!;
    public int Quantity { get; private set; }

    public QuotationProduct(Guid quotationId, Guid productId, int quantity)
    {
        QuotationId = quotationId;
        ProductId = productId;
        Quantity = quantity;
    }

    public QuotationProduct Update(Guid? quotationId, Guid? productId, int? quantity)
    {
        if (quotationId.HasValue && quotationId.Value != Guid.Empty && !QuotationId.Equals(quotationId.Value)) QuotationId = quotationId.Value;
        if (productId.HasValue && productId.Value != Guid.Empty && !ProductId.Equals(productId.Value)) ProductId = productId.Value;
        if (quantity.HasValue && Quantity != quantity) Quantity = quantity.Value;
        return this;
    }
}