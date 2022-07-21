namespace FSH.WebApi.Application.Catalog.Products;

public class ProductsBySearchRequestWithBrandsSpec : EntitiesByPaginationFilterSpec<Product, ProductDto>
{
    public ProductsBySearchRequestWithBrandsSpec(SearchProductsRequest request)
        : base(request) =>
        Query
            .Include(p => p.Brand)
            .OrderBy(c => c.Name, !request.HasOrderBy())
            .Where(p => p.BrandId.Equals(request.BrandId!.Value), request.BrandId.HasValue)
            .Where(p => p.BasePrice >= request.MinimumRate!.Value, request.MinimumRate.HasValue)
            .Where(p => p.BasePrice <= request.MaximumRate!.Value, request.MaximumRate.HasValue);
}