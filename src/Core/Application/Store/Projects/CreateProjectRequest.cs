namespace FSH.WebApi.Application.Store.Projects;

public class CreateProjectRequest : IRequest<Guid>
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public bool IsCompleted { get; set; } = default!;
    public Guid CustomerId { get; set; }
}

public class CreateProjectRequestValidator : CustomValidator<CreateProjectRequest>
{
    public CreateProjectRequestValidator(IReadRepository<Project> repository, IStringLocalizer<CreateProjectRequestValidator> T) =>
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (name, ct) => await repository.GetBySpecAsync(new ProjectByNameSpec(name), ct) is null)
                .WithMessage((_, name) => T["Project {0} already Exists.", name]);
}

public class CreateProjectRequestHandler : IRequestHandler<CreateProjectRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Project> _repository;

    public CreateProjectRequestHandler(IRepositoryWithEvents<Project> repository) => _repository = repository;

    public async Task<Guid> Handle(CreateProjectRequest request, CancellationToken cancellationToken)
    {
        var project = new Project(request.Name, request.Description, request.IsCompleted, request.CustomerId);

        await _repository.AddAsync(project, cancellationToken);

        return project.Id;
    }
}