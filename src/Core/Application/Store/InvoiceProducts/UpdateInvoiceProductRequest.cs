namespace FSH.WebApi.Application.Store.InvoiceProducts;

public class UpdateInvoiceProductRequest : IRequest<Guid>
{
    public Guid InvoiceId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}

public class UpdateInvoiceProductRequestValidator : CustomValidator<UpdateInvoiceProductRequest>
{
    public UpdateInvoiceProductRequestValidator(IRepository<InvoiceProduct> repository, IStringLocalizer<UpdateInvoiceProductRequestValidator> T) =>
        RuleFor(ip => ip.Quantity)
            .MustAsync(async (invoiceProduct, id, ct) =>
                    await repository.GetBySpecAsync(new InvoiceProductByIdsSpec(invoiceProduct.InvoiceId, invoiceProduct.ProductId), ct)
                        is not InvoiceProduct existingInvoiceProduct || (existingInvoiceProduct.InvoiceId == invoiceProduct.InvoiceId && existingInvoiceProduct.ProductId == invoiceProduct.ProductId))
                .WithMessage((_, name) => T["InvoiceProduct {0} already Exists.", name]);
}

public class UpdateInvoiceProductRequestHandler : IRequestHandler<UpdateInvoiceProductRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<InvoiceProduct> _repository;
    private readonly IStringLocalizer _t;

    public UpdateInvoiceProductRequestHandler(IRepositoryWithEvents<InvoiceProduct> repository, IStringLocalizer<UpdateInvoiceProductRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<Guid> Handle(UpdateInvoiceProductRequest request, CancellationToken cancellationToken)
    {
        var invoiceProduct = await _repository.GetBySpecAsync(new InvoiceProductByIdsSpec(request.InvoiceId, request.ProductId), cancellationToken);

        _ = invoiceProduct ?? throw new NotFoundException(_t["InvoiceProduct {0} Not Found.", request.InvoiceId, request.ProductId]);

        invoiceProduct.Update(request.InvoiceId, request.ProductId, request.Quantity);

        await _repository.UpdateAsync(invoiceProduct, cancellationToken);

        return request.InvoiceId;
    }
}