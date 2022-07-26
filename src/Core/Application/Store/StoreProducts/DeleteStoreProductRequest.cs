using FSH.WebApi.Domain.Common.Events;

namespace FSH.WebApi.Application.Store.StoreProducts;

public class DeleteStoreProductRequest : IRequest<Guid>
{
    public Guid Id { get; set; }

    public DeleteStoreProductRequest(Guid id) => Id = id;
}

public class DeleteStoreProductRequestHandler : IRequestHandler<DeleteStoreProductRequest, Guid>
{
    private readonly IRepository<StoreProduct> _repository;
    private readonly IStringLocalizer _t;

    public DeleteStoreProductRequestHandler(IRepository<StoreProduct> repository, IStringLocalizer<DeleteStoreProductRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<Guid> Handle(DeleteStoreProductRequest request, CancellationToken cancellationToken)
    {
        var product = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = product ?? throw new NotFoundException(_t["StoreProduct {0} Not Found."]);

        // Add Domain Events to be raised after the commit
        product.DomainEvents.Add(EntityDeletedEvent.WithEntity(product));

        await _repository.DeleteAsync(product, cancellationToken);

        return request.Id;
    }
}