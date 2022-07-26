namespace FSH.WebApi.Application.Store.OrderProducts;

public class GetOrderProductRequest : IRequest<OrderProductDetailsDto>
{
    public Guid Id { get; set; }

    public GetOrderProductRequest(Guid id) => Id = id;
}

public class GetProductRequestHandler : IRequestHandler<GetOrderProductRequest, OrderProductDetailsDto>
{
    private readonly IRepository<OrderProduct> _repository;
    private readonly IStringLocalizer _t;

    public GetProductRequestHandler(IRepository<OrderProduct> repository, IStringLocalizer<GetProductRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<OrderProductDetailsDto> Handle(GetOrderProductRequest request, CancellationToken cancellationToken) =>
        await _repository.GetBySpecAsync(
            (ISpecification<OrderProduct, OrderProductDetailsDto>)new OrderProductByOrderWithProductSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(_t["OrderProduct {0} Not Found.", request.Id]);
}