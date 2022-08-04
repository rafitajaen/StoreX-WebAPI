namespace FSH.WebApi.Application.Store.Projects;

public class SearchProjectsRequest : PaginationFilter, IRequest<PaginationResponse<ProjectDto>>
{
    public Guid? CustomerId { get; set; }
}

public class SearchProjectsRequestHandler : IRequestHandler<SearchProjectsRequest, PaginationResponse<ProjectDto>>
{
    private readonly IReadRepository<Project> _repository;

    public SearchProjectsRequestHandler(IReadRepository<Project> repository) => _repository = repository;

    public async Task<PaginationResponse<ProjectDto>> Handle(SearchProjectsRequest request, CancellationToken cancellationToken)
    {
        var spec = new ProjectsBySearchRequestWithCustomersSpec(request);
        return await _repository.PaginatedListAsync(spec, request.PageNumber, request.PageSize, cancellationToken);
    }
}