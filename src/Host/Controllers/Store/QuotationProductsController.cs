using FSH.WebApi.Application.Store.QuotationProducts;

namespace FSH.WebApi.Host.Controllers.Store;

public class QuotationProductsController : VersionedApiController
{
    [HttpPost("search")]
    [MustHavePermission(FSHAction.Search, FSHResource.QuotationProducts)]
    [OpenApiOperation("Search quotationProducts using available filters.", "")]
    public Task<PaginationResponse<QuotationProductDetailsDto>> SearchAsync(SearchQuotationProductsRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpGet("{quotationId:guid}/{productId:guid}")]
    [MustHavePermission(FSHAction.View, FSHResource.QuotationProducts)]
    [OpenApiOperation("Get quotationProduct details by Quotation Id", "")]
    public Task<QuotationProductDetailsDto> GetAsync(Guid quotationId, Guid productId)
    {
        return Mediator.Send(new GetQuotationProductRequest(quotationId, productId));
    }

    [HttpPost]
    [MustHavePermission(FSHAction.Create, FSHResource.QuotationProducts)]
    [OpenApiOperation("Create a new quotationProduct.", "")]
    public Task<Guid> CreateAsync(CreateQuotationProductRequest request)
    {
        return Mediator.Send(request);
    }

    [HttpPut("{quotationId:guid}/{productId:guid}")]
    [MustHavePermission(FSHAction.Update, FSHResource.QuotationProducts)]
    [OpenApiOperation("Update a quotationProduct.", "")]
    public async Task<ActionResult<Guid>> UpdateAsync(UpdateQuotationProductRequest request, Guid quotationId, Guid productId)
    {
        return (quotationId != request.QuotationId && productId != request.ProductId)
            ? BadRequest()
            : Ok(await Mediator.Send(request));
    }

    [HttpDelete("{quotationId:guid}/{productId:guid}")]
    [MustHavePermission(FSHAction.Delete, FSHResource.QuotationProducts)]
    [OpenApiOperation("Delete a quotationProduct.", "")]
    public Task<Guid> DeleteAsync(Guid quotationId, Guid productId)
    {
        return Mediator.Send(new DeleteQuotationProductRequest(quotationId, productId));
    }
}