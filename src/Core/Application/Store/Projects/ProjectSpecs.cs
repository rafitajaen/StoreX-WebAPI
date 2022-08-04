namespace FSH.WebApi.Application.Store.Projects;

public class ProjectSpecs
{
    // SPECS LIST

    // Project By Id
    // Project By Name
    // Project By Customer

    // Project By Search Request
    // Project By Search Request With Customer
}

public class ProjectByIdSpec : Specification<Project, ProjectDto>, ISingleResultSpecification
{
    public ProjectByIdSpec(Guid id) =>
        Query.Where(p => p.Id == id);
}

public class ProjectByNameSpec : Specification<Project>, ISingleResultSpecification
{
    public ProjectByNameSpec(string name) =>
        Query.Where(b => b.Name == name);
}

public class ProjectsByCustomerSpec : Specification<Project>
{
    public ProjectsByCustomerSpec(Guid customerId) =>
        Query.Where(p => p.CustomerId == customerId);
}

public class ProjectsBySearchRequestSpec : EntitiesByPaginationFilterSpec<Project, ProjectDto>
{
    public ProjectsBySearchRequestSpec(SearchProjectsRequest request)
        : base(request) =>
        Query.OrderBy(c => c.Name, !request.HasOrderBy());
}

public class ProjectsBySearchRequestWithCustomersSpec : EntitiesByPaginationFilterSpec<Project, ProjectDto>
{
    public ProjectsBySearchRequestWithCustomersSpec(SearchProjectsRequest request)
        : base(request) =>
        Query
            .Include(p => p.Customer)
            .OrderBy(c => c.Name, !request.HasOrderBy())
            .Where(p => p.CustomerId.Equals(request.CustomerId!.Value), request.CustomerId.HasValue);
}