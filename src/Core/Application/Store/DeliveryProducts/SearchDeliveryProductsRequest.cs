namespace FSH.WebApi.Application.Store.DeliveryProducts;

public class SearchDeliveryProductsRequest : PaginationFilter, IRequest<PaginationResponse<DeliveryProductDetailsDto>>
{
    public Guid? DeliveryId { get; set; }
}

public class SearchDeliveryProductsRequestHandler : IRequestHandler<SearchDeliveryProductsRequest, PaginationResponse<DeliveryProductDetailsDto>>
{
    private readonly IReadRepository<DeliveryProduct> _repository;

    public SearchDeliveryProductsRequestHandler(IReadRepository<DeliveryProduct> repository) => _repository = repository;

    public async Task<PaginationResponse<DeliveryProductDetailsDto>> Handle(SearchDeliveryProductsRequest request, CancellationToken cancellationToken)
    {
        var spec = new DeliveryProductsBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}