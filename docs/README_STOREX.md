## From Entity to EndPoint

- Step 1: Create an Entity in [_src/Core/Catalog_]
- Step 2: Create DTO (Response object) in [_src/Application/Catalog/{EntityName}_]
- Step 3: Create Requests (Create, Get, Delete...) in [_src/Application/Catalog/{EntityName}_]
- Step 4: Create Controller (Add Endpoints to API) in [_src/Host/Controllers/Catalog_]
- Step 5: (Optional) If requires, Register new Permmisions in [_src/Core/Shared/Authorization/FSHPermissions.cs]
- Step 6: Add EntityTypeConfiguration in [_src/Infrastructure/Persistance/Configuration/Catalog.cs_]
- Step 7: Add DbSets for the new Entity to DbContext in [_src/Infrastructure/Persistance/Context/ApplicationDbContext.cs_]
- Step 8: Add Migration:
  > dotnet ef migrations add InitializeStoreDb --project .././Migrators/Migrators.MSSQL/ --context ApplicationDbContext -o Migrations/Application

### Entity

**folder : _src/Core/Catalog_**

An Entity must extends `AuditableEntity`, an abstract class to audit/log changes. At the same time, it extends `BaseEntity` that assign `Id`s to the records in the DB.

Note:

- Repositories will only work with aggregate roots, not their children. Use `IAggregateRoot` to aggregate root entities.

```
public class Brand : AuditableEntity, IAggregateRoot
{
    public string Name { get; private set; }
    public string? Description { get; private set; }

    public Brand(string name, string? description)
    {
        Name = name;
        Description = description;
    }

    public Brand Update(string? name, string? description)
    {
        if (name is not null && Name?.Equals(name) is not true) Name = name;
        if (description is not null && Description?.Equals(description) is not true) Description = description;
        return this;
    }
}
```

### DTO

**folder : _src/Application/Catalog_**

This Model will be the Response to a specific request.

Entity <-> Request <- Handler -> Response <-> DTO

```
public class BrandDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
}
```

### Get Request

```
public class GetBrandRequest : IRequest<BrandDto>
{
    public Guid Id { get; set; }

    public GetBrandRequest(Guid id) => Id = id;
}

public class BrandByIdSpec : Specification<Brand, BrandDto>, ISingleResultSpecification
{
    public BrandByIdSpec(Guid id) =>
        Query.Where(p => p.Id == id);
}

public class GetBrandRequestHandler : IRequestHandler<GetBrandRequest, BrandDto>
{
    private readonly IRepository<Brand> _repository;
    private readonly IStringLocalizer _t;

    public GetBrandRequestHandler(IRepository<Brand> repository, IStringLocalizer<GetBrandRequestHandler> localizer) => (_repository, _t) = (repository, localizer);

    public async Task<BrandDto> Handle(GetBrandRequest request, CancellationToken cancellationToken) =>
        await _repository.GetBySpecAsync(
            (ISpecification<Brand, BrandDto>)new BrandByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(_t["Brand {0} Not Found.", request.Id]);
}
```

### Source Links

[Working with Forks](https://docs.github.com/en/pull-requests/collaborating-with-pull-requests/working-with-forks/about-forks)
