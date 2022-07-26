namespace FSH.WebApi.Application.Store.Orders;

public class OrderSpecs
{
    // SPECS LIST

    // Order By Id
    // Order By Name
    // Order By Supplier
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