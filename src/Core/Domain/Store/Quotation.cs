namespace FSH.WebApi.Domain.Store;
public class Quotation : AuditableEntity, IAggregateRoot
{
    public string Name { get; private set; } = default!;
    public string? Description { get; private set; }
    public bool IsAccepted { get; private set; } = default!;
    public Guid ProjectId { get; private set; }
    public virtual Project Project { get; private set; } = default!;
    public virtual ICollection<QuotationProduct> QuotationProducts { get; private set; } = default!;

    public Quotation(string name, string? description, bool isAccepted, Guid projectId)
    {
        Name = name;
        Description = description;
        IsAccepted = isAccepted;
        ProjectId = projectId;
    }

    public Quotation Update(string name, string? description, bool? isAccepted, Guid? projectId)
    {
        if (name is not null && Name?.Equals(name) is not true) Name = name;
        if (description is not null && Description?.Equals(description) is not true) Description = description;
        if (isAccepted.HasValue && IsAccepted != isAccepted) IsAccepted = isAccepted.Value;
        if (projectId.HasValue && projectId.Value != Guid.Empty && !ProjectId.Equals(projectId.Value)) ProjectId = projectId.Value;
        return this;
    }
}