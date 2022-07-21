namespace FSH.WebApi.Application.Catalog.Orders;

public class SearchOrdersRequest : PaginationFilter, IRequest<PaginationResponse<OrderDto>>
{
}

public class OrdersBySearchRequestSpec : EntitiesByPaginationFilterSpec<Order, OrderDto>
{
    public OrdersBySearchRequestSpec(SearchOrdersRequest request)
        : base(request) =>
        Query.OrderBy(c => c.Name, !request.HasOrderBy());
}

public class SearchOrdersRequestHandler : IRequestHandler<SearchOrdersRequest, PaginationResponse<OrderDto>>
{
    private readonly IReadRepository<Order> _repository;

    public SearchOrdersRequestHandler(IReadRepository<Order> repository) => _repository = repository;

    public async Task<PaginationResponse<OrderDto>> Handle(SearchOrdersRequest request, CancellationToken cancellationToken)
    {
        var spec = new OrdersBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}