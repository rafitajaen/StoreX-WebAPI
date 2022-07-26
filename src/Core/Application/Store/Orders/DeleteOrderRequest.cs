using FSH.WebApi.Domain.Common.Events;

namespace FSH.WebApi.Application.Store.Orders;

public class DeleteOrderRequest : IRequest<Guid>
{
    public Guid Id { get; set; }

    public DeleteOrderRequest(Guid id) => Id = id;
}

public class DeleteOrderRequestHandler : IRequestHandler<DeleteOrderRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Order> _orderRepo;

    private readonly IStringLocalizer _t;

    public DeleteOrderRequestHandler(IRepositoryWithEvents<Order> orderRepo, IStringLocalizer<DeleteOrderRequestHandler> localizer) =>
        (_orderRepo, _t) = (orderRepo, localizer);

    public async Task<Guid> Handle(DeleteOrderRequest request, CancellationToken cancellationToken)
    {
        var order = await _orderRepo.GetByIdAsync(request.Id, cancellationToken);

        _ = order ?? throw new NotFoundException(_t["Order {0} Not Found."]);

        // Add Domain Events to be raised after the commit
        order.DomainEvents.Add(EntityDeletedEvent.WithEntity(order));

        await _orderRepo.DeleteAsync(order, cancellationToken);

        return request.Id;
    }
}