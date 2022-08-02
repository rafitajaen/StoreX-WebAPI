using FSH.WebApi.Application.Identity.Roles;
using FSH.WebApi.Application.Identity.Users;

namespace FSH.WebApi.Application.Dashboard;

public class GetStatsRequest : IRequest<StatsDto>
{
}

public class GetStatsRequestHandler : IRequestHandler<GetStatsRequest, StatsDto>
{
    private readonly IUserService _userService;
    private readonly IRoleService _roleService;
    private readonly IReadRepository<Brand> _brandRepo;
    private readonly IReadRepository<Product> _productRepo;
    private readonly IStringLocalizer _t;

    // StoreX - Variable Declaration for GetStatsRequest
    private readonly IReadRepository<Supplier> _supplierRepo;
    private readonly IReadRepository<Order> _orderRepo;
    private readonly IReadRepository<StoreProduct> _storeProductRepo;
    private readonly IReadRepository<Customer> _customerRepo;
    private readonly IReadRepository<Project> _projectRepo;
    private readonly IReadRepository<Delivery> _deliveryRepo;

    // StoreX - Constructor for GetStatsRequest
    public GetStatsRequestHandler(
        IUserService userService,
        IRoleService roleService,
        IReadRepository<Brand> brandRepo,
        IReadRepository<Product> productRepo,
        IStringLocalizer<GetStatsRequestHandler> localizer,
        IReadRepository<Supplier> supplierRepo,
        IReadRepository<Order> orderRepo,
        IReadRepository<StoreProduct> storeProductRepo,
        IReadRepository<Customer> customerRepo,
        IReadRepository<Project> projectRepo,
        IReadRepository<Delivery> deliveryRepo)
    {
        _userService = userService;
        _roleService = roleService;
        _brandRepo = brandRepo;
        _productRepo = productRepo;
        _t = localizer;

        _supplierRepo = supplierRepo;
        _orderRepo = orderRepo;
        _storeProductRepo = storeProductRepo;
        _customerRepo = customerRepo;
        _projectRepo = projectRepo;
        _deliveryRepo = deliveryRepo;
    }

    public async Task<StatsDto> Handle(GetStatsRequest request, CancellationToken cancellationToken)
    {
        var stats = new StatsDto
        {
            ProductCount = await _productRepo.CountAsync(cancellationToken),
            BrandCount = await _brandRepo.CountAsync(cancellationToken),
            UserCount = await _userService.GetCountAsync(cancellationToken),
            RoleCount = await _roleService.GetCountAsync(cancellationToken),
            SuppliersCount = await _supplierRepo.CountAsync(cancellationToken),
            OrdersCount = await _orderRepo.CountAsync(cancellationToken),
            StoreProductsCount = await _storeProductRepo.CountAsync(cancellationToken),
            CustomersCount = await _customerRepo.CountAsync(cancellationToken),
            ProjectsCount = await _projectRepo.CountAsync(cancellationToken),
            DeliveriesCount = await _deliveryRepo.CountAsync(cancellationToken)
        };

        int selectedYear = DateTime.UtcNow.Year;
        double[] productsFigure = new double[13];
        double[] brandsFigure = new double[13];

        double[] suppliersFigure = new double[13];
        double[] ordersFigure = new double[13];
        double[] storeProductsFigure = new double[13];
        double[] customersFigure = new double[13];
        double[] projectsFigure = new double[13];
        double[] deliveriesFigure = new double[13];

        for (int i = 1; i <= 12; i++)
        {
            int month = i;
            var filterStartDate = new DateTime(selectedYear, month, 01).ToUniversalTime();
            var filterEndDate = new DateTime(selectedYear, month, DateTime.DaysInMonth(selectedYear, month), 23, 59, 59).ToUniversalTime(); // Monthly Based

            var brandSpec = new AuditableEntitiesByCreatedOnBetweenSpec<Brand>(filterStartDate, filterEndDate);
            var productSpec = new AuditableEntitiesByCreatedOnBetweenSpec<Product>(filterStartDate, filterEndDate);

            var supplierSpec = new AuditableEntitiesByCreatedOnBetweenSpec<Supplier>(filterStartDate, filterEndDate);
            var orderSpec = new AuditableEntitiesByCreatedOnBetweenSpec<Order>(filterStartDate, filterEndDate);
            var storeProductSpec = new AuditableEntitiesByCreatedOnBetweenSpec<StoreProduct>(filterStartDate, filterEndDate);
            var customerSpec = new AuditableEntitiesByCreatedOnBetweenSpec<Customer>(filterStartDate, filterEndDate);
            var projectSpec = new AuditableEntitiesByCreatedOnBetweenSpec<Project>(filterStartDate, filterEndDate);
            var deliverySpec = new AuditableEntitiesByCreatedOnBetweenSpec<Delivery>(filterStartDate, filterEndDate);

            brandsFigure[i - 1] = await _brandRepo.CountAsync(brandSpec, cancellationToken);
            productsFigure[i - 1] = await _productRepo.CountAsync(productSpec, cancellationToken);
        }

        stats.DataEnterBarChart.Add(new ChartSeries { Name = _t["Products"], Data = productsFigure });
        stats.DataEnterBarChart.Add(new ChartSeries { Name = _t["Brands"], Data = brandsFigure });

        stats.DataEnterBarChart.Add(new ChartSeries { Name = _t["Suppliers"], Data = suppliersFigure });
        stats.DataEnterBarChart.Add(new ChartSeries { Name = _t["Orders"], Data = ordersFigure });
        stats.DataEnterBarChart.Add(new ChartSeries { Name = _t["Store Products"], Data = storeProductsFigure });
        stats.DataEnterBarChart.Add(new ChartSeries { Name = _t["Customers"], Data = customersFigure });
        stats.DataEnterBarChart.Add(new ChartSeries { Name = _t["Projects"], Data = projectsFigure });
        stats.DataEnterBarChart.Add(new ChartSeries { Name = _t["Deliveries"], Data = deliveriesFigure });

        return stats;
    }
}
