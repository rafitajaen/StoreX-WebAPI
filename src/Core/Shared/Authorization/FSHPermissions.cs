using System.Collections.ObjectModel;

namespace FSH.WebApi.Shared.Authorization;

public static class FSHAction
{
    public const string View = nameof(View);
    public const string Search = nameof(Search);
    public const string Create = nameof(Create);
    public const string Update = nameof(Update);
    public const string Delete = nameof(Delete);
    public const string Export = nameof(Export);
    public const string Generate = nameof(Generate);
    public const string Clean = nameof(Clean);
    public const string UpgradeSubscription = nameof(UpgradeSubscription);
}

public static class FSHResource
{
    public const string Tenants = nameof(Tenants);
    public const string Dashboard = nameof(Dashboard);
    public const string Hangfire = nameof(Hangfire);
    public const string Users = nameof(Users);
    public const string UserRoles = nameof(UserRoles);
    public const string Roles = nameof(Roles);
    public const string RoleClaims = nameof(RoleClaims);
    public const string Products = nameof(Products);
    public const string Brands = nameof(Brands);

    // STOREX new RESOURCES
    public const string Suppliers = nameof(Suppliers);
    public const string Orders = nameof(Orders);
    public const string OrderProducts = nameof(OrderProducts);
}

public static class FSHPermissions
{
    private static readonly FSHPermission[] _all = new FSHPermission[]
    {
        new("View Dashboard", FSHAction.View, FSHResource.Dashboard),
        new("View Hangfire", FSHAction.View, FSHResource.Hangfire),
        new("View Users", FSHAction.View, FSHResource.Users),
        new("Search Users", FSHAction.Search, FSHResource.Users),
        new("Create Users", FSHAction.Create, FSHResource.Users),
        new("Update Users", FSHAction.Update, FSHResource.Users),
        new("Delete Users", FSHAction.Delete, FSHResource.Users),
        new("Export Users", FSHAction.Export, FSHResource.Users),
        new("View UserRoles", FSHAction.View, FSHResource.UserRoles),
        new("Update UserRoles", FSHAction.Update, FSHResource.UserRoles),
        new("View Roles", FSHAction.View, FSHResource.Roles),
        new("Create Roles", FSHAction.Create, FSHResource.Roles),
        new("Update Roles", FSHAction.Update, FSHResource.Roles),
        new("Delete Roles", FSHAction.Delete, FSHResource.Roles),
        new("View RoleClaims", FSHAction.View, FSHResource.RoleClaims),
        new("Update RoleClaims", FSHAction.Update, FSHResource.RoleClaims),
        new("View Products", FSHAction.View, FSHResource.Products, IsBasic: true),
        new("Search Products", FSHAction.Search, FSHResource.Products, IsBasic: true),
        new("Create Products", FSHAction.Create, FSHResource.Products),
        new("Update Products", FSHAction.Update, FSHResource.Products),
        new("Delete Products", FSHAction.Delete, FSHResource.Products),
        new("Export Products", FSHAction.Export, FSHResource.Products),
        new("View Brands", FSHAction.View, FSHResource.Brands, IsBasic: true),
        new("Search Brands", FSHAction.Search, FSHResource.Brands, IsBasic: true),
        new("Create Brands", FSHAction.Create, FSHResource.Brands),
        new("Update Brands", FSHAction.Update, FSHResource.Brands),
        new("Delete Brands", FSHAction.Delete, FSHResource.Brands),
        new("Generate Brands", FSHAction.Generate, FSHResource.Brands),
        new("Clean Brands", FSHAction.Clean, FSHResource.Brands),
        new("View Tenants", FSHAction.View, FSHResource.Tenants, IsRoot: true),
        new("Create Tenants", FSHAction.Create, FSHResource.Tenants, IsRoot: true),
        new("Update Tenants", FSHAction.Update, FSHResource.Tenants, IsRoot: true),
        new("Upgrade Tenant Subscription", FSHAction.UpgradeSubscription, FSHResource.Tenants, IsRoot: true),

        // STOREX new PERMISSIONS
        new("View   Suppliers", FSHAction.View,   FSHResource.Suppliers, IsBasic: true),
        new("Search Suppliers", FSHAction.Search, FSHResource.Suppliers, IsBasic: true),
        new("Create Suppliers", FSHAction.Create, FSHResource.Suppliers),
        new("Update Suppliers", FSHAction.Update, FSHResource.Suppliers),
        new("Delete Suppliers", FSHAction.Delete, FSHResource.Suppliers),
        new("Export Suppliers", FSHAction.Export, FSHResource.Suppliers),

        new("View   Orders", FSHAction.View,   FSHResource.Orders, IsBasic: true),
        new("Search Orders", FSHAction.Search, FSHResource.Orders, IsBasic: true),
        new("Create Orders", FSHAction.Create, FSHResource.Orders),
        new("Update Orders", FSHAction.Update, FSHResource.Orders),
        new("Delete Orders", FSHAction.Delete, FSHResource.Orders),
        new("Export Orders", FSHAction.Export, FSHResource.Orders),

        new("View   Order Products", FSHAction.View,   FSHResource.OrderProducts, IsBasic: true),
        new("Search Order Products", FSHAction.Search, FSHResource.OrderProducts, IsBasic: true),
        new("Create Order Products", FSHAction.Create, FSHResource.OrderProducts),
        new("Update Order Products", FSHAction.Update, FSHResource.OrderProducts),
        new("Delete Order Products", FSHAction.Delete, FSHResource.OrderProducts),
        new("Export Order Products", FSHAction.Export, FSHResource.OrderProducts),

    };

    public static IReadOnlyList<FSHPermission> All { get; } = new ReadOnlyCollection<FSHPermission>(_all);
    public static IReadOnlyList<FSHPermission> Root { get; } = new ReadOnlyCollection<FSHPermission>(_all.Where(p => p.IsRoot).ToArray());
    public static IReadOnlyList<FSHPermission> Admin { get; } = new ReadOnlyCollection<FSHPermission>(_all.Where(p => !p.IsRoot).ToArray());
    public static IReadOnlyList<FSHPermission> Basic { get; } = new ReadOnlyCollection<FSHPermission>(_all.Where(p => p.IsBasic).ToArray());
}

public record FSHPermission(string Description, string Action, string Resource, bool IsBasic = false, bool IsRoot = false)
{
    public string Name => NameFor(Action, Resource);
    public static string NameFor(string action, string resource) => $"Permissions.{resource}.{action}";
}
