using FSH.WebApi.Domain.Common.Events;

namespace FSH.WebApi.Application.Store.OrderProducts;

public class DeleteOrderProductRequest : IRequest<Guid>
{
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }

    public DeleteOrderProductRequest(Guid orderId, Guid productId)
    {
        OrderId = orderId;
        ProductId = productId;
    }
}

public class DeleteOrderProductRequestHandler : IRequestHandler<DeleteOrderProductRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<OrderProduct> _repository;
    private readonly IStringLocalizer _t;

    public DeleteOrderProductRequestHandler(IRepositoryWithEvents<OrderProduct> repository, IStringLocalizer<DeleteOrderProductRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<Guid> Handle(DeleteOrderProductRequest request, CancellationToken cancellationToken)
    {
        var orderProduct = await _repository.GetBySpecAsync(new OrderProductByIdsSpec(request.OrderId, request.ProductId), cancellationToken);

        _ = orderProduct ?? throw new NotFoundException(_t["OrderProduct {0} Not Found.", request.OrderId, request.ProductId]);

        // Add Domain Events to be raised after the commit
        orderProduct.DomainEvents.Add(EntityDeletedEvent.WithEntity(orderProduct));

        await _repository.DeleteAsync(orderProduct, cancellationToken);

        return request.OrderId;
    }
}