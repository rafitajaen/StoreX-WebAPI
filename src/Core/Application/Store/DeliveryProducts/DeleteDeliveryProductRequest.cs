using FSH.WebApi.Domain.Common.Events;

namespace FSH.WebApi.Application.Store.DeliveryProducts;

public class DeleteDeliveryProductRequest : IRequest<Guid>
{
    public Guid DeliveryId { get; set; }
    public Guid ProductId { get; set; }

    public DeleteDeliveryProductRequest(Guid deliveryId, Guid productId)
    {
        DeliveryId = deliveryId;
        ProductId = productId;
    }
}

public class DeleteDeliveryProductRequestHandler : IRequestHandler<DeleteDeliveryProductRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<DeliveryProduct> _repository;
    private readonly IStringLocalizer _t;

    public DeleteDeliveryProductRequestHandler(IRepositoryWithEvents<DeliveryProduct> repository, IStringLocalizer<DeleteDeliveryProductRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<Guid> Handle(DeleteDeliveryProductRequest request, CancellationToken cancellationToken)
    {
        var deliveryProduct = await _repository.GetBySpecAsync(new DeliveryProductByIdsSpec(request.DeliveryId, request.ProductId), cancellationToken);

        _ = deliveryProduct ?? throw new NotFoundException(_t["DeliveryProduct {0} Not Found.", request.DeliveryId, request.ProductId]);

        // Add Domain Events to be raised after the commit
        deliveryProduct.DomainEvents.Add(EntityDeletedEvent.WithEntity(deliveryProduct));

        await _repository.DeleteAsync(deliveryProduct, cancellationToken);

        return request.DeliveryId;
    }
}