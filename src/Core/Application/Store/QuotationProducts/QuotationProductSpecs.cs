namespace FSH.WebApi.Application.Store.QuotationProducts;

public class QuotationProductSpecs
{
    // QuotationProduct SPECS LIST

    // QuotationProduct By Id
    // QuotationProduct By Ids
    // QuotationProduct By Quotation
    // QuotationProduct By Quotation With Product
}

public class QuotationProductByIdSpec : Specification<QuotationProduct, QuotationProductDetailsDto>, ISingleResultSpecification
{
    public QuotationProductByIdSpec(Guid id) =>
        Query.Where(p => p.Id == id);
}

public class QuotationProductByIdsSpec : Specification<QuotationProduct>, ISingleResultSpecification
{
    public QuotationProductByIdsSpec(Guid QuotationId, Guid productId) =>
        Query.Where(qp => qp.QuotationId == QuotationId && qp.ProductId == productId);
}

public class QuotationProductsByQuotationSpec : Specification<QuotationProduct>
{
    public QuotationProductsByQuotationSpec(Guid QuotationId) =>
        Query.Where(p => p.QuotationId == QuotationId).Include(qp => qp.Product);
}

public class QuotationProductByQuotationWithProductSpec : Specification<QuotationProduct, QuotationProductDetailsDto>
{
    public QuotationProductByQuotationWithProductSpec(Guid QuotationId) =>
        Query
            .Where(qp => qp.QuotationId == QuotationId)
            .Include(qp => qp.Product);
}