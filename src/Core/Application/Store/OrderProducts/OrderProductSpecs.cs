namespace FSH.WebApi.Application.Store.OrderProducts;

public class OrderProductSpecs
{
    // OrderProduct SPECS LIST

    // OrderProduct By Id
    // OrderProduct By Ids
    // OrderProduct By Order
    // OrderProduct By Order With Product
    // OrderProduct By Search Request
}

public class OrderProductByIdSpec : Specification<OrderProduct, OrderProductDetailsDto>, ISingleResultSpecification
{
    public OrderProductByIdSpec(Guid id) =>
        Query.Where(p => p.Id == id);
}

public class OrderProductByIdsSpec : Specification<OrderProduct>, ISingleResultSpecification
{
    public OrderProductByIdsSpec(Guid orderId, Guid productId) =>
        Query.Where(op => op.OrderId == orderId && op.ProductId == productId);
}

public class OrderProductsByOrderSpec : Specification<OrderProduct>
{
    public OrderProductsByOrderSpec(Guid orderId) =>
        Query.Where(p => p.OrderId == orderId).Include(op => op.Product);
}

public class OrderProductByOrderWithProductSpec : Specification<OrderProduct, OrderProductDetailsDto>
{
    public OrderProductByOrderWithProductSpec(Guid orderId) =>
        Query
            .Where(op => op.OrderId == orderId)
            .Include(op => op.Product);
}

public class OrderProductsBySearchRequestSpec : EntitiesByPaginationFilterSpec<OrderProduct, OrderProductDetailsDto>
{
    public OrderProductsBySearchRequestSpec(SearchOrderProductsRequest request)
        : base(request) =>
        Query
            .Include(op => op.Product)
            .OrderBy(op => op.Product.Name, !request.HasOrderBy())
            .Where(op => op.OrderId.Equals(request.OrderId!.Value), request.OrderId.HasValue);
}