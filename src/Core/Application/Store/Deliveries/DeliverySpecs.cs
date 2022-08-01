namespace FSH.WebApi.Application.Store.Deliveries;

public class DeliverySpecs
{
    // SPECS LIST

    // Delivery By Id
    // Delivery By Name
    // Delivery By Project
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