namespace FSH.WebApi.Domain.Store;

public class Project : AuditableEntity, IAggregateRoot
{
    public string Name { get; private set; } = default!;
    public string? Description { get; private set; }
    public bool IsCompleted { get; private set; } = default!;
    public Guid ClientId { get; private set; }
    public virtual Client Client { get; private set; } = default!;

    public Project(string name, string? description, bool isCompleted, Guid clientId)
    {
        Name = name;
        Description = description;
        IsCompleted = isCompleted;
        ClientId = clientId;
    }

    public Project Update(string name, string? description, bool? isCompleted, Guid? clientId)
    {
        if (name is not null && Name?.Equals(name) is not true) Name = name;
        if (description is not null && Description?.Equals(description) is not true) Description = description;
        if (isCompleted.HasValue && IsCompleted != isCompleted) IsCompleted = isCompleted.Value;
        if (clientId.HasValue && clientId.Value != Guid.Empty && !ClientId.Equals(clientId.Value)) ClientId = clientId.Value;
        return this;
    }
}