using FSH.WebApi.Application.Store.StoreProducts;

namespace FSH.WebApi.Application.Store.OrderProducts;

public class OrderProductDetailsDto : IDto
{
    public Guid OrderId { get; set; }
    public StoreProductDto Product { get; set; } = default!;
    public int Quantity { get; set; }
}
