using FSH.WebApi.Domain.Common.Events;

namespace FSH.WebApi.Application.Store.StoreProducts;

public class CreateStoreProductRequest : IRequest<Guid>
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public decimal BasePrice { get; set; }
    public int StockUnits { get; set; }
    public string? UnitType { get; set; }
    public decimal M2 { get; set; }
    public FileUploadRequest? Image { get; set; }
}

public class CreateStoreProductRequestHandler : IRequestHandler<CreateStoreProductRequest, Guid>
{
    private readonly IRepository<StoreProduct> _repository;
    private readonly IFileStorageService _file;

    public CreateStoreProductRequestHandler(IRepository<StoreProduct> repository, IFileStorageService file) =>
        (_repository, _file) = (repository, file);

    public async Task<Guid> Handle(CreateStoreProductRequest request, CancellationToken cancellationToken)
    {
        string productImagePath = await _file.UploadAsync<StoreProduct>(request.Image, FileType.Image, cancellationToken);

        var product = new StoreProduct(request.Name, request.Description, request.BasePrice, request.StockUnits, request.UnitType, request.M2, productImagePath);

        // Add Domain Events to be raised after the commit
        product.DomainEvents.Add(EntityCreatedEvent.WithEntity(product));

        await _repository.AddAsync(product, cancellationToken);

        return product.Id;
    }
}