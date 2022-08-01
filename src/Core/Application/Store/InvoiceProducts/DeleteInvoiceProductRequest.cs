using FSH.WebApi.Domain.Common.Events;

namespace FSH.WebApi.Application.Store.InvoiceProducts;

public class DeleteInvoiceProductRequest : IRequest<Guid>
{
    public Guid InvoiceId { get; set; }
    public Guid ProductId { get; set; }

    public DeleteInvoiceProductRequest(Guid invoiceId, Guid productId)
    {
        InvoiceId = invoiceId;
        ProductId = productId;
    }
}

public class DeleteInvoiceProductRequestHandler : IRequestHandler<DeleteInvoiceProductRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<InvoiceProduct> _repository;
    private readonly IStringLocalizer _t;

    public DeleteInvoiceProductRequestHandler(IRepositoryWithEvents<InvoiceProduct> repository, IStringLocalizer<DeleteInvoiceProductRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<Guid> Handle(DeleteInvoiceProductRequest request, CancellationToken cancellationToken)
    {
        var invoiceProduct = await _repository.GetBySpecAsync(new InvoiceProductByIdsSpec(request.InvoiceId, request.ProductId), cancellationToken);

        _ = invoiceProduct ?? throw new NotFoundException(_t["InvoiceProduct {0} Not Found.", request.InvoiceId, request.ProductId]);

        // Add Domain Events to be raised after the commit
        invoiceProduct.DomainEvents.Add(EntityDeletedEvent.WithEntity(invoiceProduct));

        await _repository.DeleteAsync(invoiceProduct, cancellationToken);

        return request.InvoiceId;
    }
}