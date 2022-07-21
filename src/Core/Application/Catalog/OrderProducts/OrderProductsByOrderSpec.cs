namespace FSH.WebApi.Application.Catalog.OrderProducts;

public class OrderProductsByOrderSpec : Specification<OrderProduct>
{
    public OrderProductsByOrderSpec(Guid orderId) =>
        Query.Where(p => p.OrderId == orderId);
}