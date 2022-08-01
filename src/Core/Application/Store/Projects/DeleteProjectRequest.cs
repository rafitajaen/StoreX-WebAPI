using FSH.WebApi.Domain.Common.Events;

namespace FSH.WebApi.Application.Store.Projects;

public class DeleteProjectRequest : IRequest<Guid>
{
    public Guid Id { get; set; }

    public DeleteProjectRequest(Guid id) => Id = id;
}

public class DeleteProjectRequestHandler : IRequestHandler<DeleteProjectRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Project> _repository;

    private readonly IStringLocalizer _t;

    public DeleteProjectRequestHandler(IRepositoryWithEvents<Project> repository, IStringLocalizer<DeleteProjectRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<Guid> Handle(DeleteProjectRequest request, CancellationToken cancellationToken)
    {
        var project = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = project ?? throw new NotFoundException(_t["Project {0} Not Found."]);

        // Add Domain Events to be raised after the commit
        project.DomainEvents.Add(EntityDeletedEvent.WithEntity(project));

        await _repository.DeleteAsync(project, cancellationToken);

        return request.Id;
    }
}