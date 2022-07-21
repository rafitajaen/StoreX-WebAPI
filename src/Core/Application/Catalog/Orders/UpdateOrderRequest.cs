namespace FSH.WebApi.Application.Catalog.Orders;

public class UpdateOrderRequest : IRequest<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public bool IsCompleted { get; set; } = default!;
    public bool IsSyncWithStock { get; set; } = default!;
    public Guid SupplierId { get; set; }
}

public class UpdateOrderRequestValidator : CustomValidator<UpdateOrderRequest>
{
    public UpdateOrderRequestValidator(IRepository<Order> repository, IStringLocalizer<UpdateOrderRequestValidator> T) =>
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (order, name, ct) =>
                    await repository.GetBySpecAsync(new OrderByNameSpec(name), ct)
                        is not Order existingOrder || existingOrder.Id == order.Id)
                .WithMessage((_, name) => T["Order {0} already Exists.", name]);
}

public class UpdateOrderRequestHandler : IRequestHandler<UpdateOrderRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Order> _repository;
    private readonly IStringLocalizer _t;

    public UpdateOrderRequestHandler(IRepositoryWithEvents<Order> repository, IStringLocalizer<UpdateOrderRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<Guid> Handle(UpdateOrderRequest request, CancellationToken cancellationToken)
    {
        var order = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = order
        ?? throw new NotFoundException(_t["Order {0} Not Found.", request.Id]);

        order.Update(request.Name, request.Description, request.IsCompleted, request.IsSyncWithStock, request.SupplierId);

        await _repository.UpdateAsync(order, cancellationToken);

        return request.Id;
    }
}