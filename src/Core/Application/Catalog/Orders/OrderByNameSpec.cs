namespace FSH.WebApi.Application.Catalog.Orders;

public class OrderByNameSpec : Specification<Order>, ISingleResultSpecification
{
    public OrderByNameSpec(string name) =>
        Query.Where(b => b.Name == name);
}