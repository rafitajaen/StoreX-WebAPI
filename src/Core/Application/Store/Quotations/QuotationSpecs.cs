namespace FSH.WebApi.Application.Store.Quotations;

public class QuotationSpecs
{
    // SPECS LIST

    // Quotation By Id
    // Quotation By Name
    // Quotation By Project
}

public class QuotationByIdSpec : Specification<Quotation, QuotationDto>, ISingleResultSpecification
{
    public QuotationByIdSpec(Guid id) =>
        Query.Where(p => p.Id == id);
}

public class QuotationByNameSpec : Specification<Quotation>, ISingleResultSpecification
{
    public QuotationByNameSpec(string name) =>
        Query.Where(b => b.Name == name);
}

public class QuotationsByProjectSpec : Specification<Quotation>
{
    public QuotationsByProjectSpec(Guid projectId) =>
        Query.Where(p => p.ProjectId == projectId);
}