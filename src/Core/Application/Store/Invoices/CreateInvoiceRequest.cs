namespace FSH.WebApi.Application.Store.Invoices;

public class CreateInvoiceRequest : IRequest<Guid>
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public Guid ProjectId { get; set; }
}

public class CreateInvoiceRequestValidator : CustomValidator<CreateInvoiceRequest>
{
    public CreateInvoiceRequestValidator(IReadRepository<Invoice> repository, IStringLocalizer<CreateInvoiceRequestValidator> T) =>
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (name, ct) => await repository.GetBySpecAsync(new InvoiceByNameSpec(name), ct) is null)
                .WithMessage((_, name) => T["Invoice {0} already Exists.", name]);
}

public class CreateInvoiceRequestHandler : IRequestHandler<CreateInvoiceRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Invoice> _repository;

    public CreateInvoiceRequestHandler(IRepositoryWithEvents<Invoice> repository) => _repository = repository;

    public async Task<Guid> Handle(CreateInvoiceRequest request, CancellationToken cancellationToken)
    {
        var invoice = new Invoice(request.Name, request.Description, request.ProjectId);

        await _repository.AddAsync(invoice, cancellationToken);

        return invoice.Id;
    }
}