namespace FSH.WebApi.Application.Store.Quotations;

public class SearchQuotationsRequest : PaginationFilter, IRequest<PaginationResponse<QuotationDto>>
{
    public Guid? ProjectId { get; set; }
}

public class QuotationsBySearchRequestSpec : EntitiesByPaginationFilterSpec<Quotation, QuotationDto>
{
    public QuotationsBySearchRequestSpec(SearchQuotationsRequest request)
        : base(request) =>
        Query.OrderBy(c => c.Name, !request.HasOrderBy());
}

public class QuotationsBySearchRequestWithProjectsSpec : EntitiesByPaginationFilterSpec<Quotation, QuotationDto>
{
    public QuotationsBySearchRequestWithProjectsSpec(SearchQuotationsRequest request)
        : base(request) =>
        Query
            .Include(p => p.Project)
            .OrderBy(c => c.Name, !request.HasOrderBy())
            .Where(p => p.ProjectId.Equals(request.ProjectId!.Value), request.ProjectId.HasValue);
}

public class SearchQuotationsRequestHandler : IRequestHandler<SearchQuotationsRequest, PaginationResponse<QuotationDto>>
{
    private readonly IReadRepository<Quotation> _repository;

    public SearchQuotationsRequestHandler(IReadRepository<Quotation> repository) => _repository = repository;

    public async Task<PaginationResponse<QuotationDto>> Handle(SearchQuotationsRequest request, CancellationToken cancellationToken)
    {
        var spec = new QuotationsBySearchRequestWithProjectsSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}