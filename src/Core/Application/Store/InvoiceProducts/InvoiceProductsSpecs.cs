namespace FSH.WebApi.Application.Store.InvoiceProducts;

public class InvoiceProductSpecs
{
    // InvoiceProduct SPECS LIST

    // InvoiceProduct By Id
    // InvoiceProduct By Ids
    // InvoiceProduct By Invoice
    // InvoiceProduct By Invoice With Product
}

public class InvoiceProductByIdSpec : Specification<InvoiceProduct, InvoiceProductDetailsDto>, ISingleResultSpecification
{
    public InvoiceProductByIdSpec(Guid id) =>
        Query.Where(p => p.Id == id);
}

public class InvoiceProductByIdsSpec : Specification<InvoiceProduct>, ISingleResultSpecification
{
    public InvoiceProductByIdsSpec(Guid InvoiceId, Guid productId) =>
        Query.Where(dp => dp.InvoiceId == InvoiceId && dp.ProductId == productId);
}

public class InvoiceProductsByInvoiceSpec : Specification<InvoiceProduct>
{
    public InvoiceProductsByInvoiceSpec(Guid InvoiceId) =>
        Query.Where(p => p.InvoiceId == InvoiceId).Include(dp => dp.Product);
}

public class InvoiceProductByInvoiceWithProductSpec : Specification<InvoiceProduct, InvoiceProductDetailsDto>
{
    public InvoiceProductByInvoiceWithProductSpec(Guid InvoiceId) =>
        Query
            .Where(dp => dp.InvoiceId == InvoiceId)
            .Include(dp => dp.Product);
}