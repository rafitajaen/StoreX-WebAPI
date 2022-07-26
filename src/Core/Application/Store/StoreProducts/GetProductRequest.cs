namespace FSH.WebApi.Application.Store.StoreProducts;

public class GetStoreProductRequest : IRequest<StoreProductDto>
{
    public Guid Id { get; set; }

    public GetStoreProductRequest(Guid id) => Id = id;
}

public class GetStoreProductRequestHandler : IRequestHandler<GetStoreProductRequest, StoreProductDto>
{
    private readonly IRepository<StoreProduct> _repository;
    private readonly IStringLocalizer _t;

    public GetStoreProductRequestHandler(IRepository<StoreProduct> repository, IStringLocalizer<GetStoreProductRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<StoreProductDto> Handle(GetStoreProductRequest request, CancellationToken cancellationToken) =>
        await _repository.GetBySpecAsync(
            (ISpecification<StoreProduct, StoreProductDto>)new StoreProductByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(_t["StoreProduct {0} Not Found.", request.Id]);
}