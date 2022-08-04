namespace FSH.WebApi.Application.Store.Deliveries;

public class SearchDeliveriesRequest : PaginationFilter, IRequest<PaginationResponse<DeliveryDto>>
{
    public Guid? ProjectId { get; set; }
}

public class SearchDeliveriesRequestHandler : IRequestHandler<SearchDeliveriesRequest, PaginationResponse<DeliveryDto>>
{
    private readonly IReadRepository<Delivery> _repository;

    public SearchDeliveriesRequestHandler(IReadRepository<Delivery> repository) => _repository = repository;

    public async Task<PaginationResponse<DeliveryDto>> Handle(SearchDeliveriesRequest request, CancellationToken cancellationToken)
    {
        var spec = new DeliveriesBySearchRequestWithProjectsSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}