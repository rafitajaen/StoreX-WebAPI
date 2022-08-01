using FSH.WebApi.Application.Store.Invoices;

namespace FSH.WebApi.Host.Controllers.Store;

public class InvoicesController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.Invoices)]
    [OpenApiOperation("Search invoices using available filters.", "")]
    public Task<PaginationResponse<InvoiceDto>> SearchAsync(SearchInvoicesRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{id:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.Invoices)]
    [OpenApiOperation("Get invoice details.", "")]
    public Task<InvoiceDto> GetAsync(Guid id)
    {
        return Mediator.Send(new GetInvoiceRequest(id));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.Invoices)]
    [OpenApiOperation("Create a new invoice.", "")]
    public Task<Guid> CreateAsync(CreateInvoiceRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{id:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.Invoices)]
    [OpenApiOperation("Update a invoice.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateInvoiceRequest request, Guid id)
    {
        return id != request.Id
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{id:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.Invoices)]
    [OpenApiOperation("Delete a invoice.", "")]
    public Task<Guid> DeleteAsync(Guid id)
    {
        return Mediator.Send(new DeleteInvoiceRequest(id));
    }
}