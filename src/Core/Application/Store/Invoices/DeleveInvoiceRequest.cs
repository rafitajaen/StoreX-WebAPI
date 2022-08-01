using FSH.WebApi.Domain.Common.Events;

namespace FSH.WebApi.Application.Store.Invoices;

public class DeleteInvoiceRequest : IRequest<Guid>
{
    public Guid Id { get; set; }

    public DeleteInvoiceRequest(Guid id) => Id = id;
}

public class DeleteInvoiceRequestHandler : IRequestHandler<DeleteInvoiceRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Invoice> _repository;

    private readonly IStringLocalizer _t;

    public DeleteInvoiceRequestHandler(IRepositoryWithEvents<Invoice> repository, IStringLocalizer<DeleteInvoiceRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<Guid> Handle(DeleteInvoiceRequest request, CancellationToken cancellationToken)
    {
        var invoice = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = invoice ?? throw new NotFoundException(_t["Invoice {0} Not Found."]);

        // Add Domain Events to be raised after the commit
        invoice.DomainEvents.Add(EntityDeletedEvent.WithEntity(invoice));

        await _repository.DeleteAsync(invoice, cancellationToken);

        return request.Id;
    }
}