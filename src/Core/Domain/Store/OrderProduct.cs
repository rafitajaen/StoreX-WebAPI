namespace FSH.WebApi.Domain.Store;

public class OrderProduct : AuditableEntity, IAggregateRoot
{
    public Guid OrderId { get; private set; }
    public Order Order { get; private set; } = default!;
    public Guid ProductId { get; private set; }
    public StoreProduct Product { get; private set; } = default!;
    public int Quantity { get; private set; }

    public OrderProduct(Guid orderId, Guid productId, int quantity)
    {
        OrderId = orderId;
        ProductId = productId;
        Quantity = quantity;
    }

    public OrderProduct(Guid id, Guid orderId, Guid productId, int quantity)
    {
        Id = id;
        OrderId = orderId;
        ProductId = productId;
        Quantity = quantity;
    }

    public OrderProduct Update(Guid? orderId, Guid? productId, int? quantity)
    {
        if (orderId.HasValue && orderId.Value != Guid.Empty && !OrderId.Equals(orderId.Value)) OrderId = orderId.Value;
        if (productId.HasValue && productId.Value != Guid.Empty && !ProductId.Equals(productId.Value)) ProductId = productId.Value;
        if (quantity.HasValue && Quantity != quantity) Quantity = quantity.Value;
        return this;
    }
}