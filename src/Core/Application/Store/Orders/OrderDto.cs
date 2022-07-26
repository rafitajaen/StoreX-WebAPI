namespace FSH.WebApi.Application.Store.Orders;

public class OrderDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public bool IsCompleted { get; set; } = default!;
    public bool IsSyncWithStock { get; set; } = default!;
    public Guid SupplierId { get; set; }
    public string SupplierName { get; set; } = default!;
}
