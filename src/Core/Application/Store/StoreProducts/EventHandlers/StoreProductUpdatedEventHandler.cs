using FSH.WebApi.Domain.Common.Events;

namespace FSH.WebApi.Application.Store.StoreProducts.EventHandlers;

public class StoreProductUpdatedEventHandler : EventNotificationHandler<EntityUpdatedEvent<StoreProduct>>
{
    private readonly ILogger<StoreProductUpdatedEventHandler> _logger;

    public StoreProductUpdatedEventHandler(ILogger<StoreProductUpdatedEventHandler> logger) => _logger = logger;

    public override Task Handle(EntityUpdatedEvent<StoreProduct> @event, CancellationToken cancellationToken)
    {
        _logger.LogInformation("{event} Triggered", @event.GetType().Name);
        return Task.CompletedTask;
    }
}