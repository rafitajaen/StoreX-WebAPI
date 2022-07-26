using FSH.WebApi.Domain.Common.Events;

namespace FSH.WebApi.Application.Store.StoreProducts.EventHandlers;

public class StoreProductDeletedEventHandler : EventNotificationHandler<EntityDeletedEvent<StoreProduct>>
{
    private readonly ILogger<StoreProductDeletedEventHandler> _logger;

    public StoreProductDeletedEventHandler(ILogger<StoreProductDeletedEventHandler> logger) => _logger = logger;

    public override Task Handle(EntityDeletedEvent<StoreProduct> @event, CancellationToken cancellationToken)
    {
        _logger.LogInformation("{event} Triggered", @event.GetType().Name);
        return Task.CompletedTask;
    }
}