namespace FSH.WebApi.Application.Catalog.Orders;

public class SearchOrdersRequest : PaginationFilter, IRequest<PaginationResponse<OrderDto>>
{
    public Guid? SupplierId { get; set; }
}

public class OrdersBySearchRequestSpec : EntitiesByPaginationFilterSpec<Order, OrderDto>
{
    public OrdersBySearchRequestSpec(SearchOrdersRequest request)
        : base(request) =>
        Query.OrderBy(c => c.Name, !request.HasOrderBy());
}

public class OrdersBySearchRequestWithSuppliersSpec : EntitiesByPaginationFilterSpec<Order, OrderDto>
{
    public OrdersBySearchRequestWithSuppliersSpec(SearchOrdersRequest request)
        : base(request) =>
        Query
            .Include(p => p.Supplier)
            .OrderBy(c => c.Name, !request.HasOrderBy())
            .Where(p => p.SupplierId.Equals(request.SupplierId!.Value), request.SupplierId.HasValue);
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