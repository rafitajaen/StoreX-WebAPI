using FSH.WebApi.Domain.Common.Events;

namespace FSH.WebApi.Application.Store.StoreProducts;

public class UpdateStoreProductRequest : IRequest<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public decimal BasePrice { get; set; }
    public int StockUnits { get; set; }
    public int StockAlert { get; set; }
    public string? UnitType { get; set; }
    public decimal M2 { get; set; }
    public bool DeleteCurrentImage { get; set; } = false;
    public FileUploadRequest? Image { get; set; }
}

public class UpdateStoreProductRequestHandler : IRequestHandler<UpdateStoreProductRequest, Guid>
{
    private readonly IRepository<StoreProduct> _repository;
    private readonly IStringLocalizer _t;
    private readonly IFileStorageService _file;

    public UpdateStoreProductRequestHandler(IRepository<StoreProduct> repository, IStringLocalizer<UpdateStoreProductRequestHandler> localizer, IFileStorageService file) =>
        (_repository, _t, _file) = (repository, localizer, file);

    public async Task<Guid> Handle(UpdateStoreProductRequest request, CancellationToken cancellationToken)
    {
        var product = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = product ?? throw new NotFoundException(_t["StoreProduct {0} Not Found.", request.Id]);

        // Remove old image if flag is set
        if (request.DeleteCurrentImage)
        {
            string? currentStoreProductImagePath = product.ImagePath;
            if (!string.IsNullOrEmpty(currentStoreProductImagePath))
            {
                string root = Directory.GetCurrentDirectory();
                _file.Remove(Path.Combine(root, currentStoreProductImagePath));
            }

            product = product.ClearImagePath();
        }

        string? productImagePath = request.Image is not null
            ? await _file.UploadAsync<StoreProduct>(request.Image, FileType.Image, cancellationToken)
            : null;

        var updatedStoreProduct = product.Update(request.Name, request.Description, request.BasePrice, request.StockUnits, request.StockAlert, request.UnitType, request.M2, productImagePath);

        // Add Domain Events to be raised after the commit
        product.DomainEvents.Add(EntityUpdatedEvent.WithEntity(product));

        await _repository.UpdateAsync(updatedStoreProduct, cancellationToken);

        return request.Id;
    }
}