namespace FSH.WebApi.Application.Store.Deliveries;

public class DeliveryDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public bool IsCompleted { get; set; } = default!;
    public bool IsSyncWithStock { get; set; } = default!;
    public Guid ProjectId { get; set; }
    public string ProjectName { get; set; } = default!;

}
