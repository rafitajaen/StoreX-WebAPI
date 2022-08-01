namespace FSH.WebApi.Application.Store.Customers;

public class GetCustomerRequest : IRequest<CustomerDto>
{
    public Guid Id { get; set; }

    public GetCustomerRequest(Guid id) => Id = id;
}

public class GetCustomerRequestHandler : IRequestHandler<GetCustomerRequest, CustomerDto>
{
    private readonly IRepository<Customer> _repository;
    private readonly IStringLocalizer _t;

    public GetCustomerRequestHandler(IRepository<Customer> repository, IStringLocalizer<GetCustomerRequestHandler> localizer) => (_repository, _t) = (repository, localizer);

    public async Task<CustomerDto> Handle(GetCustomerRequest request, CancellationToken cancellationToken) =>
        await _repository.GetBySpecAsync(
            (ISpecification<Customer, CustomerDto>)new CustomerByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(_t["Customer {0} Not Found.", request.Id]);
}