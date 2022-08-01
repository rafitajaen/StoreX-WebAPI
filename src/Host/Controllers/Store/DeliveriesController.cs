using FSH.WebApi.Application.Store.Deliveries;

namespace FSH.WebApi.Host.Controllers.Store;

public class DeliveriesController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.Deliveries)]
    [OpenApiOperation("Search deliveries using available filters.", "")]
    public Task<PaginationResponse<DeliveryDto>> SearchAsync(SearchDeliveriesRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.Deliveries)]
    [OpenApiOperation("Get delivery details.", "")]
    public Task<DeliveryDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GetDeliveryRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.Deliveries)]
    [OpenApiOperation("Create a new delivery.", "")]
    public Task<Guid> CreateAsync(CreateDeliveryRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.Deliveries)]
    [OpenApiOperation("Update a delivery.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateDeliveryRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.Deliveries)]
    [OpenApiOperation("Delete a delivery.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteDeliveryRequest(id));
    }
}