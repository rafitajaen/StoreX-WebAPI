namespace FSH.WebApi.Application.Store.Invoices;

public class SearchInvoicesRequest : PaginationFilter, IRequest<PaginationResponse<InvoiceDto>>
{
    public Guid? ProjectId { get; set; }
}

public class InvoicesBySearchRequestSpec : EntitiesByPaginationFilterSpec<Invoice, InvoiceDto>
{
    public InvoicesBySearchRequestSpec(SearchInvoicesRequest request)
        : base(request) =>
        Query.OrderBy(c => c.Name, !request.HasOrderBy());
}

public class InvoicesBySearchRequestWithProjectsSpec : EntitiesByPaginationFilterSpec<Invoice, InvoiceDto>
{
    public InvoicesBySearchRequestWithProjectsSpec(SearchInvoicesRequest request)
        : base(request) =>
        Query
            .Include(p => p.Project)
            .OrderBy(c => c.Name, !request.HasOrderBy())
            .Where(p => p.ProjectId.Equals(request.ProjectId!.Value), request.ProjectId.HasValue);
}

public class SearchInvoicesRequestHandler : IRequestHandler<SearchInvoicesRequest, PaginationResponse<InvoiceDto>>
{
    private readonly IReadRepository<Invoice> _repository;

    public SearchInvoicesRequestHandler(IReadRepository<Invoice> repository) => _repository = repository;

    public async Task<PaginationResponse<InvoiceDto>> Handle(SearchInvoicesRequest request, CancellationToken cancellationToken)
    {
        var spec = new InvoicesBySearchRequestWithProjectsSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}