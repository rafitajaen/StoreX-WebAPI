namespace FSH.WebApi.Application.Store.DeliveryProducts;

public class GetDeliveryProductRequest : IRequest<DeliveryProductDetailsDto>
{
    public Guid DeliveryId { get; set; }
    public Guid ProductId { get; set; }

    public GetDeliveryProductRequest(Guid deliveryId, Guid productId)
    {
        DeliveryId = deliveryId;
        ProductId = productId;
    }
}

public class GetProductRequestHandler : IRequestHandler<GetDeliveryProductRequest, DeliveryProductDetailsDto>
{
    private readonly IRepository<DeliveryProduct> _repository;
    private readonly IStringLocalizer _t;

    public GetProductRequestHandler(IRepository<DeliveryProduct> repository, IStringLocalizer<GetProductRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<DeliveryProductDetailsDto> Handle(GetDeliveryProductRequest request, CancellationToken cancellationToken) =>
        await _repository.GetBySpecAsync(
            (ISpecification<DeliveryProduct, DeliveryProductDetailsDto>)new DeliveryProductByIdsSpec(request.DeliveryId, request.ProductId), cancellationToken)
        ?? throw new NotFoundException(_t["DeliveryProduct {0} Not Found.", request.DeliveryId, request.ProductId]);
}