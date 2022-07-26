namespace FSH.WebApi.Application.Store.Suppliers;

public class GetSupplierRequest : IRequest<SupplierDto>
{
    public Guid Id { get; set; }

    public GetSupplierRequest(Guid id) => Id = id;
}

public class GetSupplierRequestHandler : IRequestHandler<GetSupplierRequest, SupplierDto>
{
    private readonly IRepository<Supplier> _repository;
    private readonly IStringLocalizer _t;

    public GetSupplierRequestHandler(IRepository<Supplier> repository, IStringLocalizer<GetSupplierRequestHandler> localizer) => (_repository, _t) = (repository, localizer);

    public async Task<SupplierDto> Handle(GetSupplierRequest request, CancellationToken cancellationToken) =>
        await _repository.GetBySpecAsync(
            (ISpecification<Supplier, SupplierDto>)new SupplierByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(_t["Supplier {0} Not Found.", request.Id]);
}