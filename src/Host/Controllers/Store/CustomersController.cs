using FSH.WebApi.Application.Store.Customers;

namespace FSH.WebApi.Host.Controllers.Store;

public class CustomersController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.Customers)]
    [OpenApiOperation("Search customers using available filters.", "")]
    public Task<PaginationResponse<CustomerDto>> SearchAsync(SearchCustomersRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.Customers)]
    [OpenApiOperation("Get customer details.", "")]
    public Task<CustomerDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GetCustomerRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.Customers)]
    [OpenApiOperation("Create a new customer.", "")]
    public Task<Guid> CreateAsync(CreateCustomerRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.Customers)]
    [OpenApiOperation("Update a customer.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateCustomerRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.Customers)]
    [OpenApiOperation("Delete a customer.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteCustomerRequest(id));
    }
}