namespace FSH.WebApi.Domain.Store;

public class Project : AuditableEntity, IAggregateRoot
{
    public string Name { get; private set; } = default!;
    public string? Description { get; private set; }
    public bool IsCompleted { get; private set; } = default!;
    public Guid CustomerId { get; private set; }
    public virtual Customer Customer { get; private set; } = default!;

    public Project(string name, string? description, bool isCompleted, Guid customerId)
    {
        Name = name;
        Description = description;
        IsCompleted = isCompleted;
        CustomerId = customerId;
    }

    public Project Update(string name, string? description, bool? isCompleted, Guid? customerId)
    {
        if (name is not null && Name?.Equals(name) is not true) Name = name;
        if (description is not null && Description?.Equals(description) is not true) Description = description;
        if (isCompleted.HasValue && IsCompleted != isCompleted) IsCompleted = isCompleted.Value;
        if (customerId.HasValue && customerId.Value != Guid.Empty && !CustomerId.Equals(customerId.Value)) CustomerId = customerId.Value;
        return this;
    }
}