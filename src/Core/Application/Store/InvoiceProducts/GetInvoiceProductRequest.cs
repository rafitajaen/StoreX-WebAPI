namespace FSH.WebApi.Application.Store.InvoiceProducts;

public class GetInvoiceProductRequest : IRequest<InvoiceProductDetailsDto>
{
    public Guid InvoiceId { get; set; }
    public Guid ProductId { get; set; }

    public GetInvoiceProductRequest(Guid invoiceId, Guid productId)
    {
        InvoiceId = invoiceId;
        ProductId = productId;
    }
}

public class GetProductRequestHandler : IRequestHandler<GetInvoiceProductRequest, InvoiceProductDetailsDto>
{
    private readonly IRepository<InvoiceProduct> _repository;
    private readonly IStringLocalizer _t;

    public GetProductRequestHandler(IRepository<InvoiceProduct> repository, IStringLocalizer<GetProductRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<InvoiceProductDetailsDto> Handle(GetInvoiceProductRequest request, CancellationToken cancellationToken) =>
        await _repository.GetBySpecAsync(
            (ISpecification<InvoiceProduct, InvoiceProductDetailsDto>)new InvoiceProductByIdsSpec(request.InvoiceId, request.ProductId), cancellationToken)
        ?? throw new NotFoundException(_t["InvoiceProduct {0} Not Found.", request.InvoiceId, request.ProductId]);
}