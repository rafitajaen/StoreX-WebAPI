namespace FSH.WebApi.Application.Store.Quotations;

public class GetQuotationRequest : IRequest<QuotationDto>
{
    public Guid Id { get; set; }

    public GetQuotationRequest(Guid id) => Id = id;
}

public class GetQuotationRequestHandler : IRequestHandler<GetQuotationRequest, QuotationDto>
{
    private readonly IRepository<Quotation> _repository;
    private readonly IStringLocalizer _t;

    public GetQuotationRequestHandler(IRepository<Quotation> repository, IStringLocalizer<GetQuotationRequestHandler> localizer) => (_repository, _t) = (repository, localizer);

    public async Task<QuotationDto> Handle(GetQuotationRequest request, CancellationToken cancellationToken) =>
        await _repository.GetBySpecAsync(
            (ISpecification<Quotation, QuotationDto>)new QuotationByIdSpec(request.Id), cancellationToken)
        ?? throw new NotFoundException(_t["Quotation {0} Not Found.", request.Id]);
}