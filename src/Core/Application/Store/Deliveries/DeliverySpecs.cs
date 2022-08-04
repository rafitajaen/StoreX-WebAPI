namespace FSH.WebApi.Application.Store.Deliveries;

public class DeliverySpecs
{
    // SPECS LIST

    // Delivery By Id
    // Delivery By Name
    // Delivery By Project

    // Delivery By Search Request
    // Delivery By Search Request With Project
}

public class DeliveryByIdSpec : Specification<Delivery, DeliveryDto>, ISingleResultSpecification
{
    public DeliveryByIdSpec(Guid id) =>
        Query.Where(p => p.Id == id);
}

public class DeliveryByNameSpec : Specification<Delivery>, ISingleResultSpecification
{
    public DeliveryByNameSpec(string name) =>
        Query.Where(b => b.Name == name);
}

public class DeliverysByProjectSpec : Specification<Delivery>
{
    public DeliverysByProjectSpec(Guid projectId) =>
        Query.Where(p => p.ProjectId == projectId);
}

public class DeliveriesBySearchRequestSpec : EntitiesByPaginationFilterSpec<Delivery, DeliveryDto>
{
    public DeliveriesBySearchRequestSpec(SearchDeliveriesRequest request)
        : base(request) =>
        Query.OrderBy(c => c.Name, !request.HasOrderBy());
}

public class DeliveriesBySearchRequestWithProjectsSpec : EntitiesByPaginationFilterSpec<Delivery, DeliveryDto>
{
    public DeliveriesBySearchRequestWithProjectsSpec(SearchDeliveriesRequest request)
        : base(request) =>
        Query
            .Include(p => p.Project)
            .OrderBy(c => c.Name, !request.HasOrderBy())
            .Where(p => p.ProjectId.Equals(request.ProjectId!.Value), request.ProjectId.HasValue);
}