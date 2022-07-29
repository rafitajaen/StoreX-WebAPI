namespace FSH.WebApi.Application.Store.StoreProducts;

public class SearchStoreProductsRequest : PaginationFilter, IRequest<PaginationResponse<StoreProductDto>>
{
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
}

public class SearchStoreProductsRequestHandler : IRequestHandler<SearchStoreProductsRequest, PaginationResponse<StoreProductDto>>
{
    private readonly IReadRepository<StoreProduct> _repository;

    public SearchStoreProductsRequestHandler(IReadRepository<StoreProduct> repository) => _repository = repository;

    public async Task<PaginationResponse<StoreProductDto>> Handle(SearchStoreProductsRequest request, CancellationToken cancellationToken)
    {
        var spec = new StoreProductsBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken: cancellationToken);
    }
}