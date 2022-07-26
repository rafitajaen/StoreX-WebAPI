using FSH.WebApi.Application.Store.OrderProducts;

namespace FSH.WebApi.Host.Controllers.Store;

public class OrderProductsController : VersionedApiController
{
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

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.OrderProducts)]
    [OpenApiOperation("Update a orderProduct.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateOrderProductRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.OrderProducts)]
    [OpenApiOperation("Delete a orderProduct.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteOrderProductRequest(id));
    }
}