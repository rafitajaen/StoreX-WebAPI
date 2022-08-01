using FSH.WebApi.Application.Store.InvoiceProducts;

namespace FSH.WebApi.Host.Controllers.Store;

public class InvoiceProductsController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.InvoiceProducts)]
    [OpenApiOperation("Search invoiceProducts using available filters.", "")]
    public Task<PaginationResponse<InvoiceProductDetailsDto>> SearchAsync(SearchInvoiceProductsRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{invoiceId:guid}/{productId:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.InvoiceProducts)]
    [OpenApiOperation("Get invoiceProduct details by Invoice Id", "")]
    public Task<InvoiceProductDetailsDto> GetAsync(Guid invoiceId, Guid productId)
    {
        return Mediator.Send(new GetInvoiceProductRequest(invoiceId, productId));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.InvoiceProducts)]
    [OpenApiOperation("Create a new invoiceProduct.", "")]
    public Task<Guid> CreateAsync(CreateInvoiceProductRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{invoiceId:guid}/{productId:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.InvoiceProducts)]
    [OpenApiOperation("Update a invoiceProduct.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateInvoiceProductRequest request, Guid invoiceId, Guid productId)
    {
        return (invoiceId != request.InvoiceId && productId != request.ProductId)
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{invoiceId:guid}/{productId:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.InvoiceProducts)]
    [OpenApiOperation("Delete a invoiceProduct.", "")]
    public Task<Guid> DeleteAsync(Guid invoiceId, Guid productId)
    {
        return Mediator.Send(new DeleteInvoiceProductRequest(invoiceId, productId));
    }
}