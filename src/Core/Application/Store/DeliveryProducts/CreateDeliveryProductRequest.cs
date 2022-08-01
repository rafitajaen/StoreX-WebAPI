namespace FSH.WebApi.Application.Store.DeliveryProducts;

public class CreateDeliveryProductRequest : IRequest<Guid>
{
    public Guid DeliveryId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}

public class CreateDeliveryProductRequestValidator : CustomValidator<CreateDeliveryProductRequest>
{
    // Reference for this solution: https://stackoverflow.com/questions/20529085/fluentvalidation-rule-for-multiple-properties
    public CreateDeliveryProductRequestValidator(IReadRepository<DeliveryProduct> repository, IStringLocalizer<CreateDeliveryProductRequestValidator> T) =>
        RuleFor(op => new { op.DeliveryId, op.ProductId })
            .MustAsync(async (x, ct) => await repository.GetBySpecAsync(new DeliveryProductByIdsSpec(x.DeliveryId, x.ProductId), ct) is null)
                .WithMessage((_, x) => T["DeliveryProduct {0} already Exists.", x]);
}

public class CreateDeliveryProductRequestHandler : IRequestHandler<CreateDeliveryProductRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<DeliveryProduct> _repository;

    public CreateDeliveryProductRequestHandler(IRepositoryWithEvents<DeliveryProduct> repository) => _repository = repository;

    public async Task<Guid> Handle(CreateDeliveryProductRequest request, CancellationToken cancellationToken)
    {
        var deliveryProduct = new DeliveryProduct(request.DeliveryId, request.ProductId, request.Quantity);

        await _repository.AddAsync(deliveryProduct, cancellationToken);

        return deliveryProduct.Id;
    }
}