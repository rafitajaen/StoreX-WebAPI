namespace FSH.WebApi.Application.Store.Customers;

public class CreateCustomerRequest : IRequest<Guid>
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? WebsiteUrl { get; set; }
    public string? ImagePath { get; set; }
}

public class CreateCustomerRequestValidator : CustomValidator<CreateCustomerRequest>
{
    public CreateCustomerRequestValidator(IReadRepository<Customer> repository, IStringLocalizer<CreateCustomerRequestValidator> T) =>
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (name, ct) => await repository.GetBySpecAsync(new CustomerByNameSpec(name), ct) is null)
                .WithMessage((_, name) => T["Customer {0} already Exists.", name]);
}

public class CreateCustomerRequestHandler : IRequestHandler<CreateCustomerRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Customer> _repository;

    public CreateCustomerRequestHandler(IRepositoryWithEvents<Customer> repository) => _repository = repository;

    public async Task<Guid> Handle(CreateCustomerRequest request, CancellationToken cancellationToken)
    {
        var customer = new Customer(request.Name, request.Description, request.Phone, request.Email, request.WebsiteUrl, request.ImagePath);

        await _repository.AddAsync(customer, cancellationToken);

        return customer.Id;
    }
}