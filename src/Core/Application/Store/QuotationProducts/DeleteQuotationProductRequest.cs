using FSH.WebApi.Domain.Common.Events;

namespace FSH.WebApi.Application.Store.QuotationProducts;

public class DeleteQuotationProductRequest : IRequest<Guid>
{
    public Guid QuotationId { get; set; }
    public Guid ProductId { get; set; }

    public DeleteQuotationProductRequest(Guid quotationId, Guid productId)
    {
        QuotationId = quotationId;
        ProductId = productId;
    }
}

public class DeleteQuotationProductRequestHandler : IRequestHandler<DeleteQuotationProductRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<QuotationProduct> _repository;
    private readonly IStringLocalizer _t;

    public DeleteQuotationProductRequestHandler(IRepositoryWithEvents<QuotationProduct> repository, IStringLocalizer<DeleteQuotationProductRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<Guid> Handle(DeleteQuotationProductRequest request, CancellationToken cancellationToken)
    {
        var quotationProduct = await _repository.GetBySpecAsync(new QuotationProductByIdsSpec(request.QuotationId, request.ProductId), cancellationToken);

        _ = quotationProduct ?? throw new NotFoundException(_t["QuotationProduct {0} Not Found.", request.QuotationId, request.ProductId]);

        // Add Domain Events to be raised after the commit
        quotationProduct.DomainEvents.Add(EntityDeletedEvent.WithEntity(quotationProduct));

        await _repository.DeleteAsync(quotationProduct, cancellationToken);

        return request.QuotationId;
    }
}