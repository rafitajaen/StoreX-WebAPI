namespace FSH.WebApi.Application.Store.QuotationProducts;

public class UpdateQuotationProductRequest : IRequest<Guid>
{
    public Guid QuotationId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}

public class UpdateQuotationProductRequestValidator : CustomValidator<UpdateQuotationProductRequest>
{
    public UpdateQuotationProductRequestValidator(IRepository<QuotationProduct> repository, IStringLocalizer<UpdateQuotationProductRequestValidator> T) =>
        RuleFor(ip => ip.Quantity)
            .MustAsync(async (quotationProduct, id, ct) =>
                    await repository.GetBySpecAsync(new QuotationProductByIdsSpec(quotationProduct.QuotationId, quotationProduct.ProductId), ct)
                        is not QuotationProduct existingQuotationProduct || (existingQuotationProduct.QuotationId == quotationProduct.QuotationId && existingQuotationProduct.ProductId == quotationProduct.ProductId))
                .WithMessage((_, name) => T["QuotationProduct {0} already Exists.", name]);
}

public class UpdateQuotationProductRequestHandler : IRequestHandler<UpdateQuotationProductRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<QuotationProduct> _repository;
    private readonly IStringLocalizer _t;

    public UpdateQuotationProductRequestHandler(IRepositoryWithEvents<QuotationProduct> repository, IStringLocalizer<UpdateQuotationProductRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<Guid> Handle(UpdateQuotationProductRequest request, CancellationToken cancellationToken)
    {
        var quotationProduct = await _repository.GetBySpecAsync(new QuotationProductByIdsSpec(request.QuotationId, request.ProductId), cancellationToken);

        _ = quotationProduct ?? throw new NotFoundException(_t["QuotationProduct {0} Not Found.", request.QuotationId, request.ProductId]);

        quotationProduct.Update(request.QuotationId, request.ProductId, request.Quantity);

        await _repository.UpdateAsync(quotationProduct, cancellationToken);

        return request.QuotationId;
    }
}