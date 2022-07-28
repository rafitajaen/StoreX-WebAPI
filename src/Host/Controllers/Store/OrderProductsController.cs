using FSH.WebApi.Application.Store.OrderProducts;

namespace FSH.WebApi.Host.Controllers.Store;

public class OrderProductsController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.OrderProducts)]
    [OpenApiOperation("Search orderProducts using available filters.", "")]
    public Task<PaginationResponse<OrderProductDetailsDto>> SearchAsync(SearchOrderProductsRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.OrderProducts)]
    [OpenApiOperation("Get orderProduct details by Order Id", "")]
    public Task<OrderProductDetailsDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GetOrderProductRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.OrderProducts)]
    [OpenApiOperation("Create a new orderProduct.", "")]
    public Task<Guid> CreateAsync(CreateOrderProductRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{orderId:guid}/{productId:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.OrderProducts)]
    [OpenApiOperation("Update a orderProduct.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateOrderProductRequest request, Guid orderId, Guid productId)
    {
        return (orderId != request.OrderId && productId != request.ProductId)
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{orderId:guid}/{productId:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.OrderProducts)]
    [OpenApiOperation("Delete a orderProduct.", "")]
    public Task<Guid> DeleteAsync(Guid orderId, Guid productId)
    {
        return Mediator.Send(new DeleteOrderProductRequest(orderId, productId));
    }
}