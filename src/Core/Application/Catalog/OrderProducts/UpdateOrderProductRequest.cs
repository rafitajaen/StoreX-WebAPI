namespace FSH.WebApi.Application.Catalog.OrderProducts;

public class UpdateOrderProductRequest : IRequest<Guid>
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}

public class UpdateOrderProductRequestValidator : CustomValidator<UpdateOrderProductRequest>
{
    public UpdateOrderProductRequestValidator(IRepository<OrderProduct> repository, IStringLocalizer<UpdateOrderProductRequestValidator> T) =>
        RuleFor(p => p.Id)
            .MustAsync(async (orderProduct, id, ct) =>
                    await repository.GetBySpecAsync(new OrderProductByIdSpec(id), ct)
                        is not OrderProduct existingOrderProduct || existingOrderProduct.Id == orderProduct.Id)
                .WithMessage((_, name) => T["OrderProduct {0} already Exists.", name]);
}

public class UpdateOrderProductRequestHandler : IRequestHandler<UpdateOrderProductRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<OrderProduct> _repository;
    private readonly IStringLocalizer _t;

    public UpdateOrderProductRequestHandler(IRepositoryWithEvents<OrderProduct> repository, IStringLocalizer<UpdateOrderProductRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<Guid> Handle(UpdateOrderProductRequest request, CancellationToken cancellationToken)
    {
        var orderProduct = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = orderProduct
        ?? throw new NotFoundException(_t["OrderProduct {0} Not Found.", request.Id]);

        orderProduct.Update(request.OrderId, request.ProductId, request.Quantity);

        await _repository.UpdateAsync(orderProduct, cancellationToken);

        return request.Id;
    }
}