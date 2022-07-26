namespace FSH.WebApi.Application.Store.Suppliers;

public class SupplierSpecs
{
    // Supplier SPECS LIST

    // Supplier By Id
    // Supplier By Name
    // Supplier By Search Request
}
public class SupplierByIdSpec : Specification<Supplier, SupplierDto>, ISingleResultSpecification
{
    public SupplierByIdSpec(Guid id) =>
        Query.Where(p => p.Id == id);
}

public class SupplierByNameSpec : Specification<Supplier>, ISingleResultSpecification
{
    public SupplierByNameSpec(string name) =>
        Query.Where(b => b.Name == name);
}

public class SuppliersBySearchRequestSpec : EntitiesByPaginationFilterSpec<Supplier, SupplierDto>
{
    public SuppliersBySearchRequestSpec(SearchSuppliersRequest request)
        : base(request) =>
        Query.OrderBy(c => c.Name, !request.HasOrderBy());
}