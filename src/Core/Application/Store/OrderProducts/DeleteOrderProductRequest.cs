using FSH.WebApi.Domain.Common.Events;

namespace FSH.WebApi.Application.Store.OrderProducts;

public class DeleteOrderProductRequest : IRequest<Guid>
{
    public Guid Id { get; set; }

    public DeleteOrderProductRequest(Guid id) => Id = id;
}

public class DeleteOrderProductRequestHandler : IRequestHandler<DeleteOrderProductRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<OrderProduct> _orderProductRepo;
    private readonly IStringLocalizer _t;

    public DeleteOrderProductRequestHandler(IRepositoryWithEvents<OrderProduct> orderProductRepo, IStringLocalizer<DeleteOrderProductRequestHandler> localizer) =>
        (_orderProductRepo, _t) = (orderProductRepo, localizer);

    public async Task<Guid> Handle(DeleteOrderProductRequest request, CancellationToken cancellationToken)
    {
        var orderProduct = await _orderProductRepo.GetByIdAsync(request.Id, cancellationToken);

        _ = orderProduct ?? throw new NotFoundException(_t["OrderProduct {0} Not Found."]);

        // Add Domain Events to be raised after the commit
        orderProduct.DomainEvents.Add(EntityDeletedEvent.WithEntity(orderProduct));

        await _orderProductRepo.DeleteAsync(orderProduct, cancellationToken);

        return request.Id;
    }
}