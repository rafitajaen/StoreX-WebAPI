namespace FSH.WebApi.Application.Store.DeliveryProducts;

public class DeliveryProductSpecs
{
    // DeliveryProduct SPECS LIST

    // DeliveryProduct By Id
    // DeliveryProduct By Ids
    // DeliveryProduct By Delivery
    // DeliveryProduct By Delivery With Product
    // DeliveryProduct By Search Request
}

public class DeliveryProductByIdSpec : Specification<DeliveryProduct, DeliveryProductDetailsDto>, ISingleResultSpecification
{
    public DeliveryProductByIdSpec(Guid id) =>
        Query.Where(p => p.Id == id);
}

public class DeliveryProductByIdsSpec : Specification<DeliveryProduct>, ISingleResultSpecification
{
    public DeliveryProductByIdsSpec(Guid DeliveryId, Guid productId) =>
        Query.Where(dp => dp.DeliveryId == DeliveryId && dp.ProductId == productId);
}

public class DeliveryProductsByDeliverySpec : Specification<DeliveryProduct>
{
    public DeliveryProductsByDeliverySpec(Guid DeliveryId) =>
        Query.Where(p => p.DeliveryId == DeliveryId).Include(dp => dp.Product);
}

public class DeliveryProductByDeliveryWithProductSpec : Specification<DeliveryProduct, DeliveryProductDetailsDto>
{
    public DeliveryProductByDeliveryWithProductSpec(Guid DeliveryId) =>
        Query
            .Where(dp => dp.DeliveryId == DeliveryId)
            .Include(dp => dp.Product);
}

public class DeliveryProductsBySearchRequestSpec : EntitiesByPaginationFilterSpec<DeliveryProduct, DeliveryProductDetailsDto>
{
    public DeliveryProductsBySearchRequestSpec(SearchDeliveryProductsRequest request)
        : base(request) =>
        Query
            .Include(dp => dp.Product)
            .OrderBy(dp => dp.Product.Name, !request.HasOrderBy())
            .Where(dp => dp.DeliveryId.Equals(request.DeliveryId!.Value), request.DeliveryId.HasValue);
}