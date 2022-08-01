namespace FSH.WebApi.Application.Store.DeliveryProducts;

public class DeliveryProductSpecs
{
    // DeliveryProduct SPECS LIST

    // DeliveryProduct By Id
    // DeliveryProduct By Ids
    // DeliveryProduct By Delivery
    // DeliveryProduct By Delivery With Product
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