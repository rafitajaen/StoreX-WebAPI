namespace FSH.WebApi.Application.Store.Deliveries;

public class UpdateDeliveryRequest : IRequest<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public bool IsCompleted { get; set; } = default!;
    public bool IsSyncWithStock { get; set; } = default!;
    public Guid ProjectId { get; set; }
}

public class UpdateDeliveryRequestValidator : CustomValidator<UpdateDeliveryRequest>
{
    public UpdateDeliveryRequestValidator(IRepository<Delivery> repository, IStringLocalizer<UpdateDeliveryRequestValidator> T) =>
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (order, name, ct) =>
                    await repository.GetBySpecAsync(new DeliveryByNameSpec(name), ct)
                        is not Delivery existingDelivery || existingDelivery.Id == order.Id)
                .WithMessage((_, name) => T["Delivery {0} already Exists.", name]);
}

public class UpdateDeliveryRequestHandler : IRequestHandler<UpdateDeliveryRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Delivery> _repository;
    private readonly IStringLocalizer _t;

    public UpdateDeliveryRequestHandler(IRepositoryWithEvents<Delivery> repository, IStringLocalizer<UpdateDeliveryRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<Guid> Handle(UpdateDeliveryRequest request, CancellationToken cancellationToken)
    {
        var order = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = order
        ?? throw new NotFoundException(_t["Delivery {0} Not Found.", request.Id]);

        order.Update(request.Name, request.Description, request.IsCompleted, request.IsSyncWithStock, request.ProjectId);

        await _repository.UpdateAsync(order, cancellationToken);

        return request.Id;
    }
}