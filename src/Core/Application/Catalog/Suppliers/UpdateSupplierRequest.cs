namespace FSH.WebApi.Application.Catalog.Suppliers;

public class UpdateSupplierRequest : IRequest<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? WebsiteUrl { get; set; }
    public string? ImagePath { get; set; }
}

public class UpdateSupplierRequestValidator : CustomValidator<UpdateSupplierRequest>
{
    public UpdateSupplierRequestValidator(IRepository<Supplier> repository, IStringLocalizer<UpdateSupplierRequestValidator> T) =>
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (supplier, name, ct) =>
                    await repository.GetBySpecAsync(new SupplierByNameSpec(name), ct)
                        is not Supplier existingSupplier || existingSupplier.Id == supplier.Id)
                .WithMessage((_, name) => T["Supplier {0} already Exists.", name]);
}

public class UpdateSupplierRequestHandler : IRequestHandler<UpdateSupplierRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Supplier> _repository;
    private readonly IStringLocalizer _t;

    public UpdateSupplierRequestHandler(IRepositoryWithEvents<Supplier> repository, IStringLocalizer<UpdateSupplierRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<Guid> Handle(UpdateSupplierRequest request, CancellationToken cancellationToken)
    {
        var supplier = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = supplier
        ?? throw new NotFoundException(_t["Supplier {0} Not Found.", request.Id]);

        supplier.Update(request.Name, request.Description, request.Phone, request.Email, request.WebsiteUrl, request.ImagePath);

        await _repository.UpdateAsync(supplier, cancellationToken);

        return request.Id;
    }
}