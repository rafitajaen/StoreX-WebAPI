namespace FSH.WebApi.Application.Catalog.Orders;

public class CreateOrderRequest : IRequest<Guid>
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public bool IsCompleted { get; set; } = default!;
    public bool IsSyncWithStock { get; set; } = default!;
    public Guid SupplierId { get; set; }
}

public class CreateOrderRequestValidator : CustomValidator<CreateOrderRequest>
{
    public CreateOrderRequestValidator(IReadRepository<Order> repository, IStringLocalizer<CreateOrderRequestValidator> T) =>
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (name, ct) => await repository.GetBySpecAsync(new OrderByNameSpec(name), ct) is null)
                .WithMessage((_, name) => T["Order {0} already Exists.", name]);
}

public class CreateOrderRequestHandler : IRequestHandler<CreateOrderRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Order> _repository;

    public CreateOrderRequestHandler(IRepositoryWithEvents<Order> repository) => _repository = repository;

    public async Task<Guid> Handle(CreateOrderRequest request, CancellationToken cancellationToken)
    {
        var order = new Order(request.Name, request.Description, request.IsCompleted, request.IsSyncWithStock, request.SupplierId);

        await _repository.AddAsync(order, cancellationToken);

        return order.Id;
    }
}