namespace FSH.WebApi.Application.Store.Invoices;

public class UpdateInvoiceRequest : IRequest<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public Guid ProjectId { get; set; }
}

public class UpdateInvoiceRequestValidator : CustomValidator<UpdateInvoiceRequest>
{
    public UpdateInvoiceRequestValidator(IRepository<Invoice> repository, IStringLocalizer<UpdateInvoiceRequestValidator> T) =>
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (invoice, name, ct) =>
                    await repository.GetBySpecAsync(new InvoiceByNameSpec(name), ct)
                        is not Invoice existingInvoice || existingInvoice.Id == invoice.Id)
                .WithMessage((_, name) => T["Invoice {0} already Exists.", name]);
}

public class UpdateInvoiceRequestHandler : IRequestHandler<UpdateInvoiceRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Invoice> _repository;
    private readonly IStringLocalizer _t;

    public UpdateInvoiceRequestHandler(IRepositoryWithEvents<Invoice> repository, IStringLocalizer<UpdateInvoiceRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<Guid> Handle(UpdateInvoiceRequest request, CancellationToken cancellationToken)
    {
        var invoice = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = invoice
        ?? throw new NotFoundException(_t["Invoice {0} Not Found.", request.Id]);

        invoice.Update(request.Name, request.Description, request.ProjectId);

        await _repository.UpdateAsync(invoice, cancellationToken);

        return request.Id;
    }
}