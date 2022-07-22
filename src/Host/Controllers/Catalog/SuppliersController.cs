using FSH.WebApi.Application.Catalog.Suppliers;

namespace FSH.WebApi.Host.Controllers.Catalog;

public class SuppliersController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.Suppliers)]
    [OpenApiOperation("Search suppliers using available filters.", "")]
    public Task<PaginationResponse<SupplierDto>> SearchAsync(SearchSuppliersRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.Suppliers)]
    [OpenApiOperation("Get supplier details.", "")]
    public Task<SupplierDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GetSupplierRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.Suppliers)]
    [OpenApiOperation("Create a new supplier.", "")]
    public Task<Guid> CreateAsync(CreateSupplierRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.Suppliers)]
    [OpenApiOperation("Update a supplier.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateSupplierRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.Suppliers)]
    [OpenApiOperation("Delete a supplier.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteSupplierRequest(id));
    }
}