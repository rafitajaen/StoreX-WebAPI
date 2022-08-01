namespace FSH.WebApi.Application.Store.Invoices;

public class InvoiceSpecs
{
    // SPECS LIST

    // Invoice By Id
    // Invoice By Name
    // Invoice By Project
}

public class InvoiceByIdSpec : Specification<Invoice, InvoiceDto>, ISingleResultSpecification
{
    public InvoiceByIdSpec(Guid id) =>
        Query.Where(p => p.Id == id);
}

public class InvoiceByNameSpec : Specification<Invoice>, ISingleResultSpecification
{
    public InvoiceByNameSpec(string name) =>
        Query.Where(b => b.Name == name);
}

public class InvoicesByProjectSpec : Specification<Invoice>
{
    public InvoicesByProjectSpec(Guid projectId) =>
        Query.Where(p => p.ProjectId == projectId);
}