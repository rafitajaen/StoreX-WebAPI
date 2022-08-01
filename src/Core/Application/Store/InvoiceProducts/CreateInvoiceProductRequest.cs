namespace FSH.WebApi.Application.Store.InvoiceProducts;

public class CreateInvoiceProductRequest : IRequest<Guid>
{
    public Guid InvoiceId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}

public class CreateInvoiceProductRequestValidator : CustomValidator<CreateInvoiceProductRequest>
{
    // Reference for this solution: https://stackoverflow.com/questions/20529085/fluentvalidation-rule-for-multiple-properties
    public CreateInvoiceProductRequestValidator(IReadRepository<InvoiceProduct> repository, IStringLocalizer<CreateInvoiceProductRequestValidator> T) =>
        RuleFor(op => new { op.InvoiceId, op.ProductId })
            .MustAsync(async (x, ct) => await repository.GetBySpecAsync(new InvoiceProductByIdsSpec(x.InvoiceId, x.ProductId), ct) is null)
                .WithMessage((_, x) => T["InvoiceProduct {0} already Exists.", x]);
}

public class CreateInvoiceProductRequestHandler : IRequestHandler<CreateInvoiceProductRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<InvoiceProduct> _repository;

    public CreateInvoiceProductRequestHandler(IRepositoryWithEvents<InvoiceProduct> repository) => _repository = repository;

    public async Task<Guid> Handle(CreateInvoiceProductRequest request, CancellationToken cancellationToken)
    {
        var invoiceProduct = new InvoiceProduct(request.InvoiceId, request.ProductId, request.Quantity);

        await _repository.AddAsync(invoiceProduct, cancellationToken);

        return invoiceProduct.Id;
    }
}