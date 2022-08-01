namespace FSH.WebApi.Application.Store.Deliveries;

public class GetDeliveryRequest : IRequest<DeliveryDto>
{
    public Guid Id { get; set; }

    public GetDeliveryRequest(Guid id) => Id = id;
}

public class GetDeliveryRequestHandler : IRequestHandler<GetDeliveryRequest, DeliveryDto>
{
    private readonly IRepository<Delivery> _repository;
    private readonly IStringLocalizer _t;

    public GetDeliveryRequestHandler(IRepository<Delivery> repository, IStringLocalizer<GetDeliveryRequestHandler> localizer) => (_repository, _t) = (repository, localizer);

    public async Task<DeliveryDto> Handle(GetDeliveryRequest request, CancellationToken cancellationToken) =>
        await _repository.GetBySpecAsync(
            (ISpecification<Delivery, DeliveryDto>)new DeliveryByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(_t["Delivery {0} Not Found.", request.Id]);
}