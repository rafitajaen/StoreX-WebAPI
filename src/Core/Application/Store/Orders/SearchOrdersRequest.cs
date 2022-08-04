namespace FSH.WebApi.Application.Store.Orders;

public class SearchOrdersRequest : PaginationFilter, IRequest<PaginationResponse<OrderDto>>
{
    public Guid? SupplierId { get; set; }
}

public class SearchOrdersRequestHandler : IRequestHandler<SearchOrdersRequest, PaginationResponse<OrderDto>>
{
    private readonly IReadRepository<Order> _repository;

    public SearchOrdersRequestHandler(IReadRepository<Order> repository) => _repository = repository;

    public async Task<PaginationResponse<OrderDto>> Handle(SearchOrdersRequest request, CancellationToken cancellationToken)
    {
        var spec = new OrdersBySearchRequestWithSuppliersSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}