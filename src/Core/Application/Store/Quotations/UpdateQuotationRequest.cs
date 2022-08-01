namespace FSH.WebApi.Application.Store.Quotations;

public class UpdateQuotationRequest : IRequest<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public bool IsCompleted { get; set; } = default!;
    public Guid ProjectId { get; set; }
}

public class UpdateQuotationRequestValidator : CustomValidator<UpdateQuotationRequest>
{
    public UpdateQuotationRequestValidator(IRepository<Quotation> repository, IStringLocalizer<UpdateQuotationRequestValidator> T) =>
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (quotation, name, ct) =>
                    await repository.GetBySpecAsync(new QuotationByNameSpec(name), ct)
                        is not Quotation existingQuotation || existingQuotation.Id == quotation.Id)
                .WithMessage((_, name) => T["Quotation {0} already Exists.", name]);
}

public class UpdateQuotationRequestHandler : IRequestHandler<UpdateQuotationRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Quotation> _repository;
    private readonly IStringLocalizer _t;

    public UpdateQuotationRequestHandler(IRepositoryWithEvents<Quotation> repository, IStringLocalizer<UpdateQuotationRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<Guid> Handle(UpdateQuotationRequest request, CancellationToken cancellationToken)
    {
        var quotation = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = quotation
        ?? throw new NotFoundException(_t["Quotation {0} Not Found.", request.Id]);

        quotation.Update(request.Name, request.Description, request.IsCompleted, request.ProjectId);

        await _repository.UpdateAsync(quotation, cancellationToken);

        return request.Id;
    }
}