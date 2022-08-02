namespace FSH.WebApi.Application.Dashboard;

public class StatsDto
{
    public int ProductCount { get; set; }
    public int BrandCount { get; set; }
    public int UserCount { get; set; }
    public int RoleCount { get; set; }
    public List<ChartSeries> DataEnterBarChart { get; set; } = new();
    public Dictionary<string, double>? ProductByBrandTypePieChart { get; set; }

    // StoreX - StatsDto
    public int SuppliersCount { get; set; }
    public int OrdersCount { get; set; }
    public int StoreProductsCount { get; set; }
    public int CustomersCount { get; set; }
    public int ProjectsCount { get; set; }
    public int DeliveriesCount { get; set; }
}

public class ChartSeries
{
    public string? Name { get; set; }
    public double[]? Data { get; set; }
}