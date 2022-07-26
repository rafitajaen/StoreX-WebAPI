using FSH.WebApi.Application.Store.StoreProducts;

namespace FSH.WebApi.Host.Controllers.Store;

public class StoreProductsController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.StoreProducts)]
    [OpenApiOperation("Search products using available filters.", "")]
    public Task<PaginationResponse<StoreProductDto>> SearchAsync(SearchStoreProductsRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.StoreProducts)]
    [OpenApiOperation("Get product details.", "")]
    public Task<StoreProductDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GetStoreProductRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.StoreProducts)]
    [OpenApiOperation("Create a new product.", "")]
    public Task<Guid> CreateAsync(CreateStoreProductRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.StoreProducts)]
    [OpenApiOperation("Update a product.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateStoreProductRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.StoreProducts)]
    [OpenApiOperation("Delete a product.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteStoreProductRequest(id));
    }

    [HttpPost("export")]
    [MustHavePermission(FSHAction.Export, FSHResource.StoreProducts)]
    [OpenApiOperation("Export a products.", "")]
    public async Task<FileResult> ExportAsync(ExportStoreProductsRequest filter)
    {
        var result = await Mediator.Send(filter);
        return File(result, "application/octet-stream", "StoreProductExports");
    }
}