using FSH.WebApi.Application.Catalog.OrderProducts;

namespace FSH.WebApi.Host.Controllers.Catalog;

public class OrderProductsController : VersionedApiController
{
    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.Brands)]
    [OpenApiOperation("Get orderProduct details.", "")]
    public Task<OrderProductDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GetOrderProductRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.Brands)]
    [OpenApiOperation("Create a new orderProduct.", "")]
    public Task<Guid> CreateAsync(CreateOrderProductRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.Brands)]
    [OpenApiOperation("Update a orderProduct.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateOrderProductRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.Brands)]
    [OpenApiOperation("Delete a orderProduct.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteOrderProductRequest(id));
    }
}