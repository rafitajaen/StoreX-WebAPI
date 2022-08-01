using FSH.WebApi.Domain.Common.Events;

namespace FSH.WebApi.Application.Store.Quotations;

public class DeleteQuotationRequest : IRequest<Guid>
{
    public Guid Id { get; set; }

    public DeleteQuotationRequest(Guid id) => Id = id;
}

public class DeleteQuotationRequestHandler : IRequestHandler<DeleteQuotationRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Quotation> _repository;

    private readonly IStringLocalizer _t;

    public DeleteQuotationRequestHandler(IRepositoryWithEvents<Quotation> repository, IStringLocalizer<DeleteQuotationRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<Guid> Handle(DeleteQuotationRequest request, CancellationToken cancellationToken)
    {
        var quotation = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = quotation ?? throw new NotFoundException(_t["Quotation {0} Not Found."]);

        // Add Domain Events to be raised after the commit
        quotation.DomainEvents.Add(EntityDeletedEvent.WithEntity(quotation));

        await _repository.DeleteAsync(quotation, cancellationToken);

        return request.Id;
    }
}