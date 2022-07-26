using FSH.WebApi.Application.Common.Exporters;

namespace FSH.WebApi.Application.Store.StoreProducts;

public class ExportStoreProductsRequest : BaseFilter, IRequest<Stream>
{
    public decimal? MinimumRate { get; set; }
    public decimal? MaximumRate { get; set; }
}

public class ExportStoreProductsRequestHandler : IRequestHandler<ExportStoreProductsRequest, Stream>
{
    private readonly IReadRepository<StoreProduct> _repository;
    private readonly IExcelWriter _excelWriter;

    public ExportStoreProductsRequestHandler(IReadRepository<StoreProduct> repository, IExcelWriter excelWriter)
    {
        _repository = repository;
        _excelWriter = excelWriter;
    }

    public async Task<Stream> Handle(ExportStoreProductsRequest request, CancellationToken cancellationToken)
    {
        var spec = new ExportStoreProductsWithBrandsSpecification(request);

        var list = await _repository.ListAsync(spec, cancellationToken);

        return _excelWriter.WriteToStream(list);
    }
}

public class ExportStoreProductsWithBrandsSpecification : EntitiesByBaseFilterSpec<StoreProduct, StoreProductExportDto>
{
    public ExportStoreProductsWithBrandsSpecification(ExportStoreProductsRequest request)
        : base(request) =>
        Query
            .Where(p => p.BasePrice >= request.MinimumRate!.Value, request.MinimumRate.HasValue)
            .Where(p => p.BasePrice <= request.MaximumRate!.Value, request.MaximumRate.HasValue);
}