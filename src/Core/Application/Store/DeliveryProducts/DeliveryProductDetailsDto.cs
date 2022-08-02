using FSH.WebApi.Application.Store.StoreProducts;

namespace FSH.WebApi.Application.Store.DeliveryProducts;

public class DeliveryProductDetailsDto : IDto
{
    public Guid DeliveryId { get; set; }
    public string DeliveryName { get; set; } = default!;
    public StoreProductDto Product { get; set; } = default!;
    public int Quantity { get; set; }
}