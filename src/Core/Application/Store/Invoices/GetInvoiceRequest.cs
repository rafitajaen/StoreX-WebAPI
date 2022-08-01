namespace FSH.WebApi.Application.Store.Invoices;

public class GetInvoiceRequest : IRequest<InvoiceDto>
{
    public Guid Id { get; set; }

    public GetInvoiceRequest(Guid id) => Id = id;
}

public class GetInvoiceRequestHandler : IRequestHandler<GetInvoiceRequest, InvoiceDto>
{
    private readonly IRepository<Invoice> _repository;
    private readonly IStringLocalizer _t;

    public GetInvoiceRequestHandler(IRepository<Invoice> repository, IStringLocalizer<GetInvoiceRequestHandler> localizer) => (_repository, _t) = (repository, localizer);

    public async Task<InvoiceDto> Handle(GetInvoiceRequest request, CancellationToken cancellationToken) =>
        await _repository.GetBySpecAsync(
            (ISpecification<Invoice, InvoiceDto>)new InvoiceByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(_t["Invoice {0} Not Found.", request.Id]);
}