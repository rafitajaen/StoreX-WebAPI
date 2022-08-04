namespace FSH.WebApi.Application.Store.Orders;

public class OrderSpecs
{
    // SPECS LIST

    // Order By Id
    // Order By Name
    // Order By Supplier

    // Order By SearchRequest
    // Order By SearchRequest With Supplier

}

public class OrderByIdSpec : Specification<Order, OrderDto>, ISingleResultSpecification
{
    public OrderByIdSpec(Guid id) =>
        Query.Where(p => p.Id == id);
}

public class OrderByNameSpec : Specification<Order>, ISingleResultSpecification
{
    public OrderByNameSpec(string name) =>
        Query.Where(b => b.Name == name);
}

public class OrdersBySupplierSpec : Specification<Order>
{
    public OrdersBySupplierSpec(Guid supplierId) =>
        Query.Where(p => p.SupplierId == supplierId);
}

public class OrdersBySearchRequestSpec : EntitiesByPaginationFilterSpec<Order, OrderDto>
{
    public OrdersBySearchRequestSpec(SearchOrdersRequest request)
        : base(request) =>
        Query.OrderBy(c => c.Name, !request.HasOrderBy());
}

public class OrdersBySearchRequestWithSuppliersSpec : EntitiesByPaginationFilterSpec<Order, OrderDto>
{
    public OrdersBySearchRequestWithSuppliersSpec(SearchOrdersRequest request)
        : base(request) =>
        Query
            .Include(p => p.Supplier)
            .OrderBy(c => c.Name, !request.HasOrderBy())
            .Where(p => p.SupplierId.Equals(request.SupplierId!.Value), request.SupplierId.HasValue);
}