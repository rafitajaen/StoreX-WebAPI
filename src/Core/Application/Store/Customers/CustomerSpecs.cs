namespace FSH.WebApi.Application.Store.Customers;

public class CustomerSpecs
{
    // Customer SPECS LIST

    // Customer By Id
    // Customer By Name
    // Customer By Search Request
}

public class CustomerByIdSpec : Specification<Customer, CustomerDto>, ISingleResultSpecification
{
    public CustomerByIdSpec(Guid id) =>
        Query.Where(p => p.Id == id);
}

public class CustomerByNameSpec : Specification<Customer>, ISingleResultSpecification
{
    public CustomerByNameSpec(string name) =>
        Query.Where(b => b.Name == name);
}

public class CustomersBySearchRequestSpec : EntitiesByPaginationFilterSpec<Customer, CustomerDto>
{
    public CustomersBySearchRequestSpec(SearchCustomersRequest request)
        : base(request) =>
        Query.OrderBy(c => c.Name, !request.HasOrderBy());
}