using FSH.WebApi.Domain.Common.Events;

namespace FSH.WebApi.Application.Store.StoreProducts.EventHandlers;

public class StoreProductCreatedEventHandler : EventNotificationHandler<EntityCreatedEvent<StoreProduct>>
{
    private readonly ILogger<StoreProductCreatedEventHandler> _logger;

    public StoreProductCreatedEventHandler(ILogger<StoreProductCreatedEventHandler> logger) => _logger = logger;

    public override Task Handle(EntityCreatedEvent<StoreProduct> @event, CancellationToken cancellationToken)
    {
        _logger.LogInformation("{event} Triggered", @event.GetType().Name);
        return Task.CompletedTask;
    }
}