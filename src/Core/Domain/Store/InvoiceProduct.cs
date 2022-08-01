namespace FSH.WebApi.Domain.Store;

public class InvoiceProduct : AuditableEntity, IAggregateRoot
{
    public Guid InvoiceId { get; private set; }
    public Invoice Invoice { get; private set; } = default!;
    public Guid ProductId { get; private set; }
    public StoreProduct Product { get; private set; } = default!;
    public int Quantity { get; private set; }

    public InvoiceProduct(Guid invoiceId, Guid productId, int quantity)
    {
        InvoiceId = invoiceId;
        ProductId = productId;
        Quantity = quantity;
    }

    public InvoiceProduct Update(Guid? invoiceId, Guid? productId, int? quantity)
    {
        if (invoiceId.HasValue && invoiceId.Value != Guid.Empty && !InvoiceId.Equals(invoiceId.Value)) InvoiceId = invoiceId.Value;
        if (productId.HasValue && productId.Value != Guid.Empty && !ProductId.Equals(productId.Value)) ProductId = productId.Value;
        if (quantity.HasValue && Quantity != quantity) Quantity = quantity.Value;
        return this;
    }
}