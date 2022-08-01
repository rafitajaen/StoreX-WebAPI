namespace FSH.WebApi.Application.Store.Deliveries;

public class CreateDeliveryRequest : IRequest<Guid>
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public bool IsCompleted { get; set; } = default!;
    public bool IsSyncWithStock { get; set; } = default!;
    public Guid ProjectId { get; set; }
}

public class CreateDeliveryRequestValidator : CustomValidator<CreateDeliveryRequest>
{
    public CreateDeliveryRequestValidator(IReadRepository<Delivery> repository, IStringLocalizer<CreateDeliveryRequestValidator> T) =>
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (name, ct) => await repository.GetBySpecAsync(new DeliveryByNameSpec(name), ct) is null)
                .WithMessage((_, name) => T["Delivery {0} already Exists.", name]);
}

public class CreateDeliveryRequestHandler : IRequestHandler<CreateDeliveryRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Delivery> _repository;

    public CreateDeliveryRequestHandler(IRepositoryWithEvents<Delivery> repository) => _repository = repository;

    public async Task<Guid> Handle(CreateDeliveryRequest request, CancellationToken cancellationToken)
    {
        var delivery = new Delivery(request.Name, request.Description, request.IsCompleted, request.IsSyncWithStock, request.ProjectId);

        await _repository.AddAsync(delivery, cancellationToken);

        return delivery.Id;
    }
}