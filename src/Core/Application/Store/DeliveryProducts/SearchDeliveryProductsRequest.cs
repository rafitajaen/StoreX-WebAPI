namespace FSH.WebApi.Application.Store.DeliveryProducts;

public class SearchDeliveryProductsRequest : PaginationFilter, IRequest<PaginationResponse<DeliveryProductDetailsDto>>
{
    public Guid? DeliveryId { get; set; }
}

public class DeliveryProductsBySearchRequestSpec : EntitiesByPaginationFilterSpec<DeliveryProduct, DeliveryProductDetailsDto>
{
    public DeliveryProductsBySearchRequestSpec(SearchDeliveryProductsRequest request)
        : base(request) =>
        Query
            .Include(dp => dp.Product)
            .OrderBy(dp => dp.Product.Name, !request.HasOrderBy())
            .Where(dp => dp.DeliveryId.Equals(request.DeliveryId!.Value), request.DeliveryId.HasValue);
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