namespace FSH.WebApi.Domain.Store;

public class Invoice : AuditableEntity, IAggregateRoot
{
    public string Name { get; private set; } = default!;
    public string? Description { get; private set; }
    public Guid ProjectId { get; private set; }
    public virtual Project Project { get; private set; } = default!;
    public virtual ICollection<InvoiceProduct> InvoiceProducts { get; private set; } = default!;

    public Invoice(string name, string? description, Guid projectId)
    {
        Name = name;
        Description = description;
        ProjectId = projectId;
    }

    public Invoice Update(string name, string? description, Guid? projectId)
    {
        if (name is not null && Name?.Equals(name) is not true) Name = name;
        if (description is not null && Description?.Equals(description) is not true) Description = description;
        if (projectId.HasValue && projectId.Value != Guid.Empty && !ProjectId.Equals(projectId.Value)) ProjectId = projectId.Value;
        return this;
    }
}