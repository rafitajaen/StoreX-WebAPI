using FSH.WebApi.Application.Store.Quotations;

namespace FSH.WebApi.Host.Controllers.Store;

public class QuotationsController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.Quotations)]
    [OpenApiOperation("Search orders using available filters.", "")]
    public Task<PaginationResponse<QuotationDto>> SearchAsync(SearchQuotationsRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.Quotations)]
    [OpenApiOperation("Get order details.", "")]
    public Task<QuotationDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GetQuotationRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.Quotations)]
    [OpenApiOperation("Create a new order.", "")]
    public Task<Guid> CreateAsync(CreateQuotationRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.Quotations)]
    [OpenApiOperation("Update a order.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateQuotationRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.Quotations)]
    [OpenApiOperation("Delete a order.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteQuotationRequest(id));
    }
}