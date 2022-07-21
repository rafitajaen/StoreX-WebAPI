namespace FSH.WebApi.Application.Catalog.OrderProducts;

public class OrderProductByIdsSpec : Specification<OrderProduct>, ISingleResultSpecification
{
    public OrderProductByIdsSpec(Guid orderId, Guid productId) =>
        Query.Where(op => op.OrderId == orderId && op.ProductId == productId);
}