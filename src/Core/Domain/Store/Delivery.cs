namespace FSH.WebApi.Domain.Store;
public class Delivery : AuditableEntity, IAggregateRoot
{
    public string Name { get; private set; } = default!;
    public string? Description { get; private set; }
    public bool IsCompleted { get; private set; } = default!;
    public bool IsSyncWithStock { get; private set; } = default!;
    public Guid ProjectId { get; private set; }
    public virtual Project Project { get; private set; } = default!;
    public virtual ICollection<DeliveryProduct> DeliveryProducts { get; private set; } = default!;

    public Delivery(string name, string? description, bool isCompleted, bool isSyncWithStock, Guid projectId)
    {
        Name = name;
        Description = description;
        IsCompleted = isCompleted;
        IsSyncWithStock = isSyncWithStock;
        ProjectId = projectId;

    }

    public Delivery Update(string name, string? description, bool? isCompleted, bool? isSyncWithStock, Guid? projectId)
    {
        if (name is not null && Name?.Equals(name) is not true) Name = name;
        if (description is not null && Description?.Equals(description) is not true) Description = description;
        if (isCompleted.HasValue && IsCompleted != isCompleted) IsCompleted = isCompleted.Value;
        if (isSyncWithStock.HasValue && IsSyncWithStock != isSyncWithStock) IsSyncWithStock = isSyncWithStock.Value;
        if (projectId.HasValue && projectId.Value != Guid.Empty && !ProjectId.Equals(projectId.Value)) ProjectId = projectId.Value;
        return this;
    }
}