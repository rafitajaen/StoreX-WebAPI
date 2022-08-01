namespace FSH.WebApi.Application.Store.DeliveryProducts;

public class UpdateDeliveryProductRequest : IRequest<Guid>
{
    public Guid DeliveryId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}

public class UpdateDeliveryProductRequestValidator : CustomValidator<UpdateDeliveryProductRequest>
{
    public UpdateDeliveryProductRequestValidator(IRepository<DeliveryProduct> repository, IStringLocalizer<UpdateDeliveryProductRequestValidator> T) =>
        RuleFor(op => op.Quantity)
            .MustAsync(async (deliveryProduct, id, ct) =>
                    await repository.GetBySpecAsync(new DeliveryProductByIdsSpec(deliveryProduct.DeliveryId, deliveryProduct.ProductId), ct)
                        is not DeliveryProduct existingDeliveryProduct || (existingDeliveryProduct.DeliveryId == deliveryProduct.DeliveryId && existingDeliveryProduct.ProductId == deliveryProduct.ProductId))
                .WithMessage((_, name) => T["DeliveryProduct {0} already Exists.", name]);
}

public class UpdateDeliveryProductRequestHandler : IRequestHandler<UpdateDeliveryProductRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<DeliveryProduct> _repository;
    private readonly IStringLocalizer _t;

    public UpdateDeliveryProductRequestHandler(IRepositoryWithEvents<DeliveryProduct> repository, IStringLocalizer<UpdateDeliveryProductRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<Guid> Handle(UpdateDeliveryProductRequest request, CancellationToken cancellationToken)
    {
        var deliveryProduct = await _repository.GetBySpecAsync(new DeliveryProductByIdsSpec(request.DeliveryId, request.ProductId), cancellationToken);

        _ = deliveryProduct ?? throw new NotFoundException(_t["DeliveryProduct {0} Not Found.", request.DeliveryId, request.ProductId]);

        deliveryProduct.Update(request.DeliveryId, request.ProductId, request.Quantity);

        await _repository.UpdateAsync(deliveryProduct, cancellationToken);

        return request.DeliveryId;
    }
}