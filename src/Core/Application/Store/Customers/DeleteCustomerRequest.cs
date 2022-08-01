using FSH.WebApi.Application.Store.Projects;

namespace FSH.WebApi.Application.Store.Customers;

public class DeleteCustomerRequest : IRequest<Guid>
{
    public Guid Id { get; set; }

    public DeleteCustomerRequest(Guid id) => Id = id;
}

public class DeleteCustomerRequestHandler : IRequestHandler<DeleteCustomerRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Customer> _customerRepo;
    private readonly IReadRepository<Project> _projectRepo;
    private readonly IStringLocalizer _t;

    public DeleteCustomerRequestHandler(IRepositoryWithEvents<Customer> customerRepo, IReadRepository<Project> projectRepo, IStringLocalizer<DeleteCustomerRequestHandler> localizer) =>
        (_customerRepo, _projectRepo, _t) = (customerRepo, projectRepo, localizer);

    public async Task<Guid> Handle(DeleteCustomerRequest request, CancellationToken cancellationToken)
    {
        if (await _projectRepo.AnyAsync(new ProjectsByCustomerSpec(request.Id), cancellationToken))
        {
            throw new ConflictException(_t["Customer cannot be deleted as it's being used."]);
        }

        var customer = await _customerRepo.GetByIdAsync(request.Id, cancellationToken);

        _ = customer ?? throw new NotFoundException(_t["Customer {0} Not Found."]);

        await _customerRepo.DeleteAsync(customer, cancellationToken);

        return request.Id;
    }
}