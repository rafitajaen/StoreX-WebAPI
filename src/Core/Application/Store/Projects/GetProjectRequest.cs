namespace FSH.WebApi.Application.Store.Projects;

public class GetProjectRequest : IRequest<ProjectDto>
{
    public Guid Id { get; set; }

    public GetProjectRequest(Guid id) => Id = id;
}

public class GetProjectRequestHandler : IRequestHandler<GetProjectRequest, ProjectDto>
{
    private readonly IRepository<Project> _repository;
    private readonly IStringLocalizer _t;

    public GetProjectRequestHandler(IRepository<Project> repository, IStringLocalizer<GetProjectRequestHandler> localizer) => (_repository, _t) = (repository, localizer);

    public async Task<ProjectDto> Handle(GetProjectRequest request, CancellationToken cancellationToken) =>
        await _repository.GetBySpecAsync(
            (ISpecification<Project, ProjectDto>)new ProjectByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(_t["Project {0} Not Found.", request.Id]);
}