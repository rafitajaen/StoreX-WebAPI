namespace FSH.WebApi.Application.Store.QuotationProducts;

public class CreateQuotationProductRequest : IRequest<Guid>
{
    public Guid QuotationId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}

public class CreateQuotationProductRequestValidator : CustomValidator<CreateQuotationProductRequest>
{
    // Reference for this solution: https://stackoverflow.com/questions/20529085/fluentvalidation-rule-for-multiple-prqperties
    public CreateQuotationProductRequestValidator(IReadRepository<QuotationProduct> repository, IStringLocalizer<CreateQuotationProductRequestValidator> T) =>
        RuleFor(qp => new { qp.QuotationId, qp.ProductId })
            .MustAsync(async (x, ct) => await repository.GetBySpecAsync(new QuotationProductByIdsSpec(x.QuotationId, x.ProductId), ct) is null)
                .WithMessage((_, x) => T["QuotationProduct {0} already Exists.", x]);
}

public class CreateQuotationProductRequestHandler : IRequestHandler<CreateQuotationProductRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<QuotationProduct> _repository;

    public CreateQuotationProductRequestHandler(IRepositoryWithEvents<QuotationProduct> repository) => _repository = repository;

    public async Task<Guid> Handle(CreateQuotationProductRequest request, CancellationToken cancellationToken)
    {
        var quotationProduct = new QuotationProduct(request.QuotationId, request.ProductId, request.Quantity);

        await _repository.AddAsync(quotationProduct, cancellationToken);

        return quotationProduct.Id;
    }
}