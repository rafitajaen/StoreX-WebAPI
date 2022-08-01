namespace FSH.WebApi.Application.Store.Projects;

public class SearchProjectsRequest : PaginationFilter, IRequest<PaginationResponse<ProjectDto>>
{
    public Guid? CustomerId { get; set; }
}

public class ProjectsBySearchRequestSpec : EntitiesByPaginationFilterSpec<Project, ProjectDto>
{
    public ProjectsBySearchRequestSpec(SearchProjectsRequest request)
        : base(request) =>
        Query.OrderBy(c => c.Name, !request.HasOrderBy());
}

public class ProjectsBySearchRequestWithCustomersSpec : EntitiesByPaginationFilterSpec<Project, ProjectDto>
{
    public ProjectsBySearchRequestWithCustomersSpec(SearchProjectsRequest request)
        : base(request) =>
        Query
            .Include(p => p.Customer)
            .OrderBy(c => c.Name, !request.HasOrderBy())
            .Where(p => p.CustomerId.Equals(request.CustomerId!.Value), request.CustomerId.HasValue);
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