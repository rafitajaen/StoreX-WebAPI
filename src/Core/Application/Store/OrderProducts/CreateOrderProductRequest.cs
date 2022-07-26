namespace FSH.WebApi.Application.Store.OrderProducts;

public class CreateOrderProductRequest : IRequest<Guid>
{
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}

public class CreateOrderProductRequestValidator : CustomValidator<CreateOrderProductRequest>
{
    // Reference for this solution: https://stackoverflow.com/questions/20529085/fluentvalidation-rule-for-multiple-properties
    public CreateOrderProductRequestValidator(IReadRepository<OrderProduct> repository, IStringLocalizer<CreateOrderProductRequestValidator> T) =>
        RuleFor(op => new { op.OrderId, op.ProductId })
            .MustAsync(async (x, ct) => await repository.GetBySpecAsync(new OrderProductByIdsSpec(x.OrderId, x.ProductId), ct) is null)
                .WithMessage((_, x) => T["OrderProduct {0} already Exists.", x]);
}

public class CreateOrderProductRequestHandler : IRequestHandler<CreateOrderProductRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<OrderProduct> _repository;

    public CreateOrderProductRequestHandler(IRepositoryWithEvents<OrderProduct> repository) => _repository = repository;

    public async Task<Guid> Handle(CreateOrderProductRequest request, CancellationToken cancellationToken)
    {
        var brand = new OrderProduct(request.OrderId, request.ProductId, request.Quantity);

        await _repository.AddAsync(brand, cancellationToken);

        return brand.Id;
    }
}