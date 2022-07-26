namespace FSH.WebApi.Application.Store.Orders;

public class GetOrderRequest : IRequest<OrderDto>
{
    public Guid Id { get; set; }

    public GetOrderRequest(Guid id) => Id = id;
}

public class GetOrderRequestHandler : IRequestHandler<GetOrderRequest, OrderDto>
{
    private readonly IRepository<Order> _repository;
    private readonly IStringLocalizer _t;

    public GetOrderRequestHandler(IRepository<Order> repository, IStringLocalizer<GetOrderRequestHandler> localizer) => (_repository, _t) = (repository, localizer);

    public async Task<OrderDto> Handle(GetOrderRequest request, CancellationToken cancellationToken) =>
        await _repository.GetBySpecAsync(
            (ISpecification<Order, OrderDto>)new OrderByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(_t["Order {0} Not Found.", request.Id]);
}