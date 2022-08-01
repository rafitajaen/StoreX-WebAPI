namespace FSH.WebApi.Application.Store.Customers;

public class SearchCustomersRequest : PaginationFilter, IRequest<PaginationResponse<CustomerDto>>
{
}

public class SearchCustomersRequestHandler : IRequestHandler<SearchCustomersRequest, PaginationResponse<CustomerDto>>
{
    private readonly IReadRepository<Customer> _repository;

    public SearchCustomersRequestHandler(IReadRepository<Customer> repository) => _repository = repository;

    public async Task<PaginationResponse<CustomerDto>> Handle(SearchCustomersRequest request, CancellationToken cancellationToken)
    {
        var spec = new CustomersBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}