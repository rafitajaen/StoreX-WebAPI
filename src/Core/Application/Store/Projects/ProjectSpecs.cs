namespace FSH.WebApi.Application.Store.Projects;

public class ProjectSpecs
{
    // SPECS LIST

    // Project By Id
    // Project By Name
    // Project By Customer
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