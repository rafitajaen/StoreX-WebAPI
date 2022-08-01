using FSH.WebApi.Application.Store.DeliveryProducts;

namespace FSH.WebApi.Host.Controllers.Store;

public class DeliveryProductsController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.DeliveryProducts)]
    [OpenApiOperation("Search deliveryProducts using available filters.", "")]
    public Task<PaginationResponse<DeliveryProductDetailsDto>> SearchAsync(SearchDeliveryProductsRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{deliveryId:guid}/{productId:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.DeliveryProducts)]
    [OpenApiOperation("Get deliveryProduct details by Delivery Id", "")]
    public Task<DeliveryProductDetailsDto> GetAsync(Guid deliveryId, Guid productId)
    {
        return Mediator.Send(new GetDeliveryProductRequest(deliveryId, productId));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.DeliveryProducts)]
    [OpenApiOperation("Create a new deliveryProduct.", "")]
    public Task<Guid> CreateAsync(CreateDeliveryProductRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{deliveryId:guid}/{productId:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.DeliveryProducts)]
    [OpenApiOperation("Update a deliveryProduct.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateDeliveryProductRequest request, Guid deliveryId, Guid productId)
    {
        return (deliveryId != request.DeliveryId && productId != request.ProductId)
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{deliveryId:guid}/{productId:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.DeliveryProducts)]
    [OpenApiOperation("Delete a deliveryProduct.", "")]
    public Task<Guid> DeleteAsync(Guid deliveryId, Guid productId)
    {
        return Mediator.Send(new DeleteDeliveryProductRequest(deliveryId, productId));
    }
}