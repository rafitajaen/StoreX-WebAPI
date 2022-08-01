namespace FSH.WebApi.Application.Store.DeliveryProducts;

public class DeliveryProductDetailsDto : IDto
{
    public Guid DeliveryId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}