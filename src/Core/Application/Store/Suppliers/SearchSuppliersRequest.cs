namespace FSH.WebApi.Application.Store.Suppliers;

public class SearchSuppliersRequest : PaginationFilter, IRequest<PaginationResponse<SupplierDto>>
{
}



public class SearchSuppliersRequestHandler : IRequestHandler<SearchSuppliersRequest, PaginationResponse<SupplierDto>>
{
    private readonly IReadRepository<Supplier> _repository;

    public SearchSuppliersRequestHandler(IReadRepository<Supplier> repository) => _repository = repository;

    public async Task<PaginationResponse<SupplierDto>> Handle(SearchSuppliersRequest request, CancellationToken cancellationToken)
    {
        var spec = new SuppliersBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}