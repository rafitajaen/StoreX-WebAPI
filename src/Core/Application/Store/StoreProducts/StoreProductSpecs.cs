namespace FSH.WebApi.Application.Store.StoreProducts;
public class StoreProductSpecs
{
    // Product SPEC LIST

    // Product By Id
    // Product By Name
}

public class StoreProductByIdSpec : Specification<StoreProduct>, ISingleResultSpecification
{
    public StoreProductByIdSpec(Guid id) =>
        Query.Where(p => p.Id == id);
}

public class StoreProductByNameSpec : Specification<StoreProduct>, ISingleResultSpecification
{
    public StoreProductByNameSpec(string name) =>
        Query.Where(p => p.Name == name);
}

public class StoreProductsBySearchRequestSpec : EntitiesByPaginationFilterSpec<StoreProduct, StoreProductDto>
{
    public StoreProductsBySearchRequestSpec(SearchStoreProductsRequest request)
        : base(request) =>
        Query.OrderBy(c => c.Name, !request.HasOrderBy());
}