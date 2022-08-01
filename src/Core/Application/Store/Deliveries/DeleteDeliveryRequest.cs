using FSH.WebApi.Domain.Common.Events;

namespace FSH.WebApi.Application.Store.Deliveries;

public class DeleteDeliveryRequest : IRequest<Guid>
{
    public Guid Id { get; set; }

    public DeleteDeliveryRequest(Guid id) => Id = id;
}

public class DeleteDeliveryRequestHandler : IRequestHandler<DeleteDeliveryRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Delivery> _repository;

    private readonly IStringLocalizer _t;

    public DeleteDeliveryRequestHandler(IRepositoryWithEvents<Delivery> repository, IStringLocalizer<DeleteDeliveryRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<Guid> Handle(DeleteDeliveryRequest request, CancellationToken cancellationToken)
    {
        var delivery = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = delivery ?? throw new NotFoundException(_t["Delivery {0} Not Found."]);

        // Add Domain Events to be raised after the commit
        delivery.DomainEvents.Add(EntityDeletedEvent.WithEntity(delivery));

        await _repository.DeleteAsync(delivery, cancellationToken);

        return request.Id;
    }
}