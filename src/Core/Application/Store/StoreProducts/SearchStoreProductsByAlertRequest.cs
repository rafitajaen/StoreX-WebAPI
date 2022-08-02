namespace FSH.WebApi.Application.Store.StoreProducts;

public class SearchStoreProductsByAlertRequest : PaginationFilter, IRequest<PaginationResponse<StoreProductDto>>
{

}

public class SearchStoreProductsByAlertSpec : EntitiesByPaginationFilterSpec<StoreProduct, StoreProductDto>
{
    public SearchStoreProductsByAlertSpec(SearchStoreProductsByAlertRequest request)
        : base(request) =>
        Query
            .Where(p => p.StockAlert >= p.StockUnits);
}

public class SearchStoreProductsByAlertRequestHandler : IRequestHandler<SearchStoreProductsByAlertRequest, PaginationResponse<StoreProductDto>>
{
    private readonly IReadRepository<StoreProduct> _repository;

    public SearchStoreProductsByAlertRequestHandler(IReadRepository<StoreProduct> repository) => _repository = repository;

    public async Task<PaginationResponse<StoreProductDto>> Handle(SearchStoreProductsByAlertRequest request, CancellationToken cancellationToken)
    {
        var spec = new SearchStoreProductsByAlertSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken: cancellationToken);
    }
}