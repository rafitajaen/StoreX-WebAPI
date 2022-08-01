namespace FSH.WebApi.Application.Store.Quotations;

public class QuotationDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public bool IsAccepted { get; set; } = default!;
    public Guid ProjectId { get; set; }
    public string ProjectName { get; set; } = default!;
}
