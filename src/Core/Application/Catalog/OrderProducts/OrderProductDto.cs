namespace FSH.WebApi.Application.Catalog.OrderProducts;

public class OrderProductDto : IDto
{
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}