namespace FSH.WebApi.Application.Catalog.Suppliers;

public class SearchSuppliersRequest : PaginationFilter, IRequest<PaginationResponse<SupplierDto>>
{
}

public class SuppliersBySearchRequestSpec : EntitiesByPaginationFilterSpec<Supplier, SupplierDto>
{
    public SuppliersBySearchRequestSpec(SearchSuppliersRequest request)
        : base(request) =>
        Query.OrderBy(c => c.Name, !request.HasOrderBy());
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