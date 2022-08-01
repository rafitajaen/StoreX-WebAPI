namespace FSH.WebApi.Application.Store.Invoices;

public class InvoiceDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public Guid ProjectId { get; set; }
    public string ProjectName { get; set; } = default!;
}