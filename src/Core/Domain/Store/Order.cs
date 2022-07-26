namespace FSH.WebApi.Domain.Store;

public class Order : AuditableEntity, IAggregateRoot
{
    public string Name { get; private set; } = default!;
    public string? Description { get; private set; }
    public bool IsCompleted { get; private set; } = default!;
    public bool IsSyncWithStock { get; private set; } = default!;
    public Guid SupplierId { get; private set; }
    public virtual Supplier Supplier { get; private set; } = default!;
    public virtual ICollection<OrderProduct> OrderProducts { get; private set; } = default!;

    public Order(Guid id, string name, string? description, bool isCompleted, bool isSyncWithStock, Guid supplierId)
    {
        Id = id;
        Name = name;
        Description = description;
        IsCompleted = isCompleted;
        IsSyncWithStock = isSyncWithStock;
        SupplierId = supplierId;
    }

    public Order(string name, string? description, bool isCompleted, bool isSyncWithStock, Guid supplierId)
    {
        Name = name;
        Description = description;
        IsCompleted = isCompleted;
        IsSyncWithStock = isSyncWithStock;
        SupplierId = supplierId;
    }

    public Order Update(string name, string? description, bool? isCompleted, bool? isSyncWithStock, Guid? supplierId)
    {
        if (name is not null && Name?.Equals(name) is not true) Name = name;
        if (description is not null && Description?.Equals(description) is not true) Description = description;
        if (isCompleted.HasValue && IsCompleted != isCompleted) IsCompleted = isCompleted.Value;
        if (isSyncWithStock.HasValue && IsSyncWithStock != isSyncWithStock) IsSyncWithStock = isSyncWithStock.Value;
        if (supplierId.HasValue && supplierId.Value != Guid.Empty && !SupplierId.Equals(supplierId.Value)) SupplierId = supplierId.Value;
        return this;
    }
}
