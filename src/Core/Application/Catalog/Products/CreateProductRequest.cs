using FSH.WebApi.Domain.Common.Events;

namespace FSH.WebApi.Application.Catalog.Products;

public class CreateProductRequest : IRequest<Guid>
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public decimal BasePrice { get; set; }
    public int StockUnits { get; set; }
    public string? UnitType { get; set; }
    public decimal M2 { get; set; }
    public Guid BrandId { get; set; }
    public FileUploadRequest? Image { get; set; }
}

public class CreateProductRequestHandler : IRequestHandler<CreateProductRequest, Guid>
{
    private readonly IRepository<Product> _repository;
    private readonly IFileStorageService _file;

    public CreateProductRequestHandler(IRepository<Product> repository, IFileStorageService file) =>
        (_repository, _file) = (repository, file);

    public async Task<Guid> Handle(CreateProductRequest request, CancellationToken cancellationToken)
    {
        string productImagePath = await _file.UploadAsync<Product>(request.Image, FileType.Image, cancellationToken);

        var product = new Product(request.Name, request.Description, request.BasePrice, request.StockUnits, request.UnitType, request.M2, request.BrandId, productImagePath);

        // Add Domain Events to be raised after the commit
        product.DomainEvents.Add(EntityCreatedEvent.WithEntity(product));

        await _repository.AddAsync(product, cancellationToken);

        return product.Id;
    }
}