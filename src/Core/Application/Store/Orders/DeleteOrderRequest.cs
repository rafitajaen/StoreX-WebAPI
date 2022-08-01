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
    private readonly IRepositoryWithEvents<Order> _repository;

    private readonly IStringLocalizer _t;

    public DeleteOrderRequestHandler(IRepositoryWithEvents<Order> repository, IStringLocalizer<DeleteOrderRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<Guid> Handle(DeleteOrderRequest request, CancellationToken cancellationToken)
    {
        var order = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = order ?? throw new NotFoundException(_t["Order {0} Not Found."]);

        // Add Domain Events to be raised after the commit
        order.DomainEvents.Add(EntityDeletedEvent.WithEntity(order));

        await _repository.DeleteAsync(order, cancellationToken);

        return request.Id;
    }
}