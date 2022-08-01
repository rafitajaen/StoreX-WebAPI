namespace FSH.WebApi.Application.Store.Suppliers;

public class CreateSupplierRequest : IRequest<Guid>
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? WebsiteUrl { get; set; }
    public string? ImagePath { get; set; }
}

public class CreateSupplierRequestValidator : CustomValidator<CreateSupplierRequest>
{
    public CreateSupplierRequestValidator(IReadRepository<Supplier> repository, IStringLocalizer<CreateSupplierRequestValidator> T) =>
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (name, ct) => await repository.GetBySpecAsync(new SupplierByNameSpec(name), ct) is null)
                .WithMessage((_, name) => T["Supplier {0} already Exists.", name]);
}

public class CreateSupplierRequestHandler : IRequestHandler<CreateSupplierRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Supplier> _repository;

    public CreateSupplierRequestHandler(IRepositoryWithEvents<Supplier> repository) => _repository = repository;

    public async Task<Guid> Handle(CreateSupplierRequest request, CancellationToken cancellationToken)
    {
        var supplier = new Supplier(request.Name, request.Description, request.Phone, request.Email, request.WebsiteUrl, request.ImagePath);

        await _repository.AddAsync(supplier, cancellationToken);

        return supplier.Id;
    }
}