namespace FSH.WebApi.Application.Store.QuotationProducts;

public class SearchQuotationProductsRequest : PaginationFilter, IRequest<PaginationResponse<QuotationProductDetailsDto>>
{
    public Guid? QuotationId { get; set; }
}

public class QuotationProductsBySearchRequestSpec : EntitiesByPaginationFilterSpec<QuotationProduct, QuotationProductDetailsDto>
{
    public QuotationProductsBySearchRequestSpec(SearchQuotationProductsRequest request)
        : base(request) =>
        Query
            .Include(qp => qp.Product)
            .OrderBy(qp => qp.Product.Name, !request.HasOrderBy())
            .Where(qp => qp.QuotationId.Equals(request.QuotationId!.Value), request.QuotationId.HasValue);
}

public class SearchQuotationProductsRequestHandler : IRequestHandler<SearchQuotationProductsRequest, PaginationResponse<QuotationProductDetailsDto>>
{
    private readonly IReadRepository<QuotationProduct> _repository;

    public SearchQuotationProductsRequestHandler(IReadRepository<QuotationProduct> repository) => _repository = repository;

    public async Task<PaginationResponse<QuotationProductDetailsDto>> Handle(SearchQuotationProductsRequest request, CancellationToken cancellationToken)
    {
        var spec = new QuotationProductsBySearchRequestSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}