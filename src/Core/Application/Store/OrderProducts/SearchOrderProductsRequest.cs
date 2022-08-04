namespace FSH.WebApi.Application.Store.OrderProducts;
public class SearchOrderProductsRequest : PaginationFilter, IRequest<PaginationResponse<OrderProductDetailsDto>>
{
    public Guid? OrderId { get; set; }
}

public class SearchOrderProductsRequestHandler : IRequestHandler<SearchOrderProductsRequest, PaginationResponse<OrderProductDetailsDto>>
{
    private readonly IReadRepository<OrderProduct> _repository;

    public SearchOrderProductsRequestHandler(IReadRepository<OrderProduct> repository) => _repository = repository;

    public async Task<PaginationResponse<OrderProductDetailsDto>> Handle(SearchOrderProductsRequest request, CancellationToken cancellationToken)
    {
        var spec = new OrderProductsBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}