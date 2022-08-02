using FSH.WebApi.Application.Common.Exporters;

namespace FSH.WebApi.Application.Store.StoreProducts;

public class ExportStoreProductsByAlertRequest : BaseFilter, IRequest<Stream>
{

}

public class ExportStoreProductsByAlertRequestHandler : IRequestHandler<ExportStoreProductsByAlertRequest, Stream>
{
    private readonly IReadRepository<StoreProduct> _repository;
    private readonly IExcelWriter _excelWriter;

    public ExportStoreProductsByAlertRequestHandler(IReadRepository<StoreProduct> repository, IExcelWriter excelWriter)
    {
        _repository = repository;
        _excelWriter = excelWriter;
    }

    public async Task<Stream> Handle(ExportStoreProductsByAlertRequest request, CancellationToken cancellationToken)
    {
        var spec = new ExportStoreProductsByAlertSpec(request);

        var list = await _repository.ListAsync(spec, cancellationToken);

        return _excelWriter.WriteToStream(list);
    }
}

public class ExportStoreProductsByAlertSpec : EntitiesByBaseFilterSpec<StoreProduct, StoreProductExportDto>
{
    public ExportStoreProductsByAlertSpec(ExportStoreProductsByAlertRequest request)
        : base(request) =>
        Query
            .Where(p => p.StockAlert >= p.StockUnits);
}