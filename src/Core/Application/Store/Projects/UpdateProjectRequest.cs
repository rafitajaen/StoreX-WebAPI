namespace FSH.WebApi.Application.Store.Projects;

public class UpdateProjectRequest : IRequest<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public bool IsCompleted { get; set; } = default!;
    public Guid CustomerId { get; set; }
}

public class UpdateProjectRequestValidator : CustomValidator<UpdateProjectRequest>
{
    public UpdateProjectRequestValidator(IRepository<Project> repository, IStringLocalizer<UpdateProjectRequestValidator> T) =>
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (project, name, ct) =>
                    await repository.GetBySpecAsync(new ProjectByNameSpec(name), ct)
                        is not Project existingProject || existingProject.Id == project.Id)
                .WithMessage((_, name) => T["Project {0} already Exists.", name]);
}

public class UpdateProjectRequestHandler : IRequestHandler<UpdateProjectRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Project> _repository;
    private readonly IStringLocalizer _t;

    public UpdateProjectRequestHandler(IRepositoryWithEvents<Project> repository, IStringLocalizer<UpdateProjectRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<Guid> Handle(UpdateProjectRequest request, CancellationToken cancellationToken)
    {
        var project = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = project
        ?? throw new NotFoundException(_t["Project {0} Not Found.", request.Id]);

        project.Update(request.Name, request.Description, request.IsCompleted, request.CustomerId);

        await _repository.UpdateAsync(project, cancellationToken);

        return request.Id;
    }
}