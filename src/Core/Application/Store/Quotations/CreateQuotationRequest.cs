namespace FSH.WebApi.Application.Store.Quotations;

public class CreateQuotationRequest : IRequest<Guid>
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public bool IsCompleted { get; set; } = default!;
    public Guid ProjectId { get; set; }
}

public class CreateQuotationRequestValidator : CustomValidator<CreateQuotationRequest>
{
    public CreateQuotationRequestValidator(IReadRepository<Quotation> repository, IStringLocalizer<CreateQuotationRequestValidator> T) =>
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (name, ct) => await repository.GetBySpecAsync(new QuotationByNameSpec(name), ct) is null)
                .WithMessage((_, name) => T["Quotation {0} already Exists.", name]);
}

public class CreateQuotationRequestHandler : IRequestHandler<CreateQuotationRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Quotation> _repository;

    public CreateQuotationRequestHandler(IRepositoryWithEvents<Quotation> repository) => _repository = repository;

    public async Task<Guid> Handle(CreateQuotationRequest request, CancellationToken cancellationToken)
    {
        var quotation = new Quotation(request.Name, request.Description, request.IsCompleted, request.ProjectId);

        await _repository.AddAsync(quotation, cancellationToken);

        return quotation.Id;
    }
}