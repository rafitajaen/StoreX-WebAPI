using FSH.WebApi.Application.Store.Orders;

namespace FSH.WebApi.Application.Store.Suppliers;

public class DeleteSupplierRequest : IRequest<Guid>
{
    public Guid Id { get; set; }

    public DeleteSupplierRequest(Guid id) => Id = id;
}

public class DeleteSupplierRequestHandler : IRequestHandler<DeleteSupplierRequest, Guid>
{
    // Add Domain Events automatically by using IRepositoryWithEvents
    private readonly IRepositoryWithEvents<Supplier> _supplierRepo;
    private readonly IReadRepository<Order> _orderRepo;
    private readonly IStringLocalizer _t;

    public DeleteSupplierRequestHandler(IRepositoryWithEvents<Supplier> supplierRepo, IReadRepository<Order> orderRepo, IStringLocalizer<DeleteSupplierRequestHandler> localizer) =>
        (_supplierRepo, _orderRepo, _t) = (supplierRepo, orderRepo, localizer);

    public async Task<Guid> Handle(DeleteSupplierRequest request, CancellationToken cancellationToken)
    {
        if (await _orderRepo.AnyAsync(new OrdersBySupplierSpec(request.Id), cancellationToken))
        {
            throw new ConflictException(_t["Supplier cannot be deleted as it's being used."]);
        }

        var supplier = await _supplierRepo.GetByIdAsync(request.Id, cancellationToken);

        _ = supplier ?? throw new NotFoundException(_t["Supplier {0} Not Found."]);

        await _supplierRepo.DeleteAsync(supplier, cancellationToken);

        return request.Id;
    }
}