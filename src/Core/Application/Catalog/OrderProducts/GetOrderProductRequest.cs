namespace FSH.WebApi.Application.Catalog.OrderProducts;

public class GetOrderProductRequest : IRequest<OrderProductDto>
{
    public Guid Id { get; set; }

    public GetOrderProductRequest(Guid id) => Id = id;
}

public class OrderProductByIdSpec : Specification<OrderProduct, OrderProductDto>, ISingleResultSpecification
{
    public OrderProductByIdSpec(Guid id) =>
        Query.Where(p => p.Id == id);
}

public class GetOrderProductRequestHandler : IRequestHandler<GetOrderProductRequest, OrderProductDto>
{
    private readonly IRepository<OrderProduct> _repository;
    private readonly IStringLocalizer _t;

    public GetOrderProductRequestHandler(IRepository<OrderProduct> repository, IStringLocalizer<GetOrderProductRequestHandler> localizer) => (_repository, _t) = (repository, localizer);

    public async Task<OrderProductDto> Handle(GetOrderProductRequest request, CancellationToken cancellationToken) =>
        await _repository.GetBySpecAsync(
            (ISpecification<OrderProduct, OrderProductDto>)new OrderProductByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(_t["OrderProduct {0} Not Found.", request.Id]);
}