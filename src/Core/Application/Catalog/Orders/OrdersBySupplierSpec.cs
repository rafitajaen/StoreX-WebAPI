namespace FSH.WebApi.Application.Catalog.Orders;

public class OrdersBySupplierSpec : Specification<Order>
{
    public OrdersBySupplierSpec(Guid supplierId) =>
        Query.Where(p => p.SupplierId == supplierId);
}