namespace FSH.WebApi.Application.Store.Customers;

public class UpdateCustomerRequest : IRequest<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? WebsiteUrl { get; set; }
    public string? ImagePath { get; set; }
}

public class UpdateCustomerRequestValidator : CustomValidator<UpdateCustomerRequest>
{
    public UpdateCustomerRequestValidator(IRepository<Customer> repository, IStringLocalizer<UpdateCustomerRequestValidator> T) =>
        RuleFor(p => p.Name)
            .NotEmpty()
            .MaximumLength(75)
            .MustAsync(async (customer, name, ct) =>
                    await repository.GetBySpecAsync(new CustomerByNameSpec(name), ct)
                        is not Customer existingCustomer || existingCustomer.Id == customer.Id)
                .WithMessage((_, name) => T["Customer {0} already Exists.", name]);
}

public class UpdateCustomerRequestHandler : IRequestHandler<UpdateCustomerRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Customer> _repository;
    private readonly IStringLocalizer _t;

    public UpdateCustomerRequestHandler(IRepositoryWithEvents<Customer> repository, IStringLocalizer<UpdateCustomerRequestHandler> localizer) =>
        (_repository, _t) = (repository, localizer);

    public async Task<Guid> Handle(UpdateCustomerRequest request, CancellationToken cancellationToken)
    {
        var customer = await _repository.GetByIdAsync(request.Id, cancellationToken);

        _ = customer
        ?? throw new NotFoundException(_t["Customer {0} Not Found.", request.Id]);

        customer.Update(request.Name, request.Description, request.Phone, request.Email, request.WebsiteUrl, request.ImagePath);

        await _repository.UpdateAsync(customer, cancellationToken);

        return request.Id;
    }
}