namespace FSH.WebApi.Domain.Store;

public class DeliveryProduct : AuditableEntity, IAggregateRoot
{
    public Guid DeliveryId { get; private set; }
    public Delivery Delivery { get; private set; } = default!;
    public Guid ProductId { get; private set; }
    public StoreProduct Product { get; private set; } = default!;
    public int Quantity { get; private set; }

    public DeliveryProduct(Guid deliveryId, Guid productId, int quantity)
    {
        DeliveryId = deliveryId;
        ProductId = productId;
        Quantity = quantity;
    }

    public DeliveryProduct Update(Guid? deliveryId, Guid? productId, int? quantity)
    {
        if (deliveryId.HasValue && deliveryId.Value != Guid.Empty && !DeliveryId.Equals(deliveryId.Value)) DeliveryId = deliveryId.Value;
        if (productId.HasValue && productId.Value != Guid.Empty && !ProductId.Equals(productId.Value)) ProductId = productId.Value;
        if (quantity.HasValue && Quantity != quantity) Quantity = quantity.Value;
        return this;
    }
}