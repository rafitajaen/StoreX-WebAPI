namespace FSH.WebApi.Application.Store.Deliveries;

public class SearchDeliveriesRequest : PaginationFilter, IRequest<PaginationResponse<DeliveryDto>>
{
    public Guid? ProjectId { get; set; }
}

public class DeliveriesBySearchRequestSpec : EntitiesByPaginationFilterSpec<Delivery, DeliveryDto>
{
    public DeliveriesBySearchRequestSpec(SearchDeliveriesRequest request)
        : base(request) =>
        Query.OrderBy(c => c.Name, !request.HasOrderBy());
}

public class DeliveriesBySearchRequestWithProjectsSpec : EntitiesByPaginationFilterSpec<Delivery, DeliveryDto>
{
    public DeliveriesBySearchRequestWithProjectsSpec(SearchDeliveriesRequest request)
        : base(request) =>
        Query
            .Include(p => p.Project)
            .OrderBy(c => c.Name, !request.HasOrderBy())
            .Where(p => p.ProjectId.Equals(request.ProjectId!.Value), request.ProjectId.HasValue);
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