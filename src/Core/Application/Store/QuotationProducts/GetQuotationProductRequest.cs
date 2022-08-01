namespace FSH.WebApi.Application.Store.QuotationProducts;

public class GetQuotationProductRequest : IRequest<QuotationProductDetailsDto>
{
    public Guid QuotationId { get; set; }
    public Guid ProductId { get; set; }

    public GetQuotationProductRequest(Guid quotationId, Guid productId)
    {
        QuotationId = quotationId;
        ProductId = productId;
    }
}

public class GetProductRequestHandler : IRequestHandler<GetQuotationProductRequest, QuotationProductDetailsDto>
{
    private readonly IRepository<QuotationProduct> _repository;
    private readonly IStringLocalizer _t;

    public GetProductRequestHandler(IRepository<QuotationProduct> repository, IStringLocalizer<GetProductRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<QuotationProductDetailsDto> Handle(GetQuotationProductRequest request, CancellationToken cancellationToken) =>
        await _repository.GetBySpecAsync(
            (ISpecification<QuotationProduct, QuotationProductDetailsDto>)new QuotationProductByIdsSpec(request.QuotationId, request.ProductId), cancellationToken)
        ?? throw new NotFoundException(_t["QuotationProduct {0} Not Found.", request.QuotationId, request.ProductId]);
}