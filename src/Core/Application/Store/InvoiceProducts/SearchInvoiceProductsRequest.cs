namespace FSH.WebApi.Application.Store.InvoiceProducts;

public class SearchInvoiceProductsRequest : PaginationFilter, IRequest<PaginationResponse<InvoiceProductDetailsDto>>
{
    public Guid? InvoiceId { get; set; }
}

public class InvoiceProductsBySearchRequestSpec : EntitiesByPaginationFilterSpec<InvoiceProduct, InvoiceProductDetailsDto>
{
    public InvoiceProductsBySearchRequestSpec(SearchInvoiceProductsRequest request)
        : base(request) =>
        Query
            .Include(ip => ip.Product)
            .OrderBy(ip => ip.Product.Name, !request.HasOrderBy())
            .Where(ip => ip.InvoiceId.Equals(request.InvoiceId!.Value), request.InvoiceId.HasValue);
}

public class SearchInvoiceProductsRequestHandler : IRequestHandler<SearchInvoiceProductsRequest, PaginationResponse<InvoiceProductDetailsDto>>
{
    private readonly IReadRepository<InvoiceProduct> _repository;

    public SearchInvoiceProductsRequestHandler(IReadRepository<InvoiceProduct> repository) => _repository = repository;

    public async Task<PaginationResponse<InvoiceProductDetailsDto>> Handle(SearchInvoiceProductsRequest request, CancellationToken cancellationToken)
    {
        var spec = new InvoiceProductsBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}