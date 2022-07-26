using System.Reflection;
using FSH.WebApi.Application.Common.Interfaces;
using FSH.WebApi.Domain.Store;
using FSH.WebApi.Infrastructure.Persistence.Context;
using FSH.WebApi.Infrastructure.Persistence.Initialization;
using Microsoft.Extensions.Logging;

namespace FSH.WebApi.Infrastructure.Store;

public class StoreSeeder : ICustomSeeder
{
    private readonly ISerializerService _serializerService;
    private readonly ApplicationDbContext _db;
    private readonly ILogger<StoreSeeder> _logger;

    public StoreSeeder(ISerializerService serializerService, ILogger<StoreSeeder> logger, ApplicationDbContext db)
    {
        _serializerService = serializerService;
        _logger = logger;
        _db = db;
    }

    public async Task InitializeAsync(CancellationToken cancellationToken)
    {
        string? path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        // Seed SUPPLIERS
        if (!_db.Suppliers.Any())
        {
            _logger.LogInformation("Started to Seed {Suppliers}.", nameof(_db.Suppliers));

            var suppliers = new List<Supplier>
            {
                SeederHelper.Supplier1,
                SeederHelper.Supplier2,
                SeederHelper.Supplier3,
                SeederHelper.Supplier4
            };

            // Here you can use your own logic to populate the database.

            if (suppliers != null)
            {
                foreach (var supplier in suppliers)
                {
                    _logger.LogInformation("Seeding {Supplier} => Name: '{SupplierName}' - Email: '{SupplierEmail}'", nameof(Supplier), supplier.Name, supplier.Email);
                    await _db.Suppliers.AddAsync(supplier, cancellationToken);
                }
            }

            await _db.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Seeded {Suppliers} Complete.", nameof(_db.Suppliers));
        }

        // Seed PRODUCTS
        if (!_db.StoreProducts.Any())
        {
            _logger.LogInformation("Started to Seed {Products}.", nameof(_db.Products));

            var products = new List<StoreProduct>
            {
                SeederHelper.Product1,
                SeederHelper.Product2,
                SeederHelper.Product3,
                SeederHelper.Product4,
                SeederHelper.Product5,
                SeederHelper.Product6
            };

            // Here you can use your own logic to populate the database.

            if (products != null)
            {
                foreach (var product in products)
                {
                    _logger.LogInformation("Seeding {StoreProduct} => Name: '{ProductName}' - Description: '{ProductDesc}'", nameof(StoreProduct), product.Name, product.Description);
                    await _db.StoreProducts.AddAsync(product, cancellationToken);
                }
            }

            await _db.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Seeded {Products} Complete.", nameof(_db.Products));
        }

        var googleSupplier = _db.Suppliers.Where(s => s.Name.Contains("Google")).FirstOrDefault();
        var amazonSupplier = _db.Suppliers.Where(s => s.Name.Contains("Amazon")).FirstOrDefault();
        var facebookSupplier = _db.Suppliers.Where(s => s.Name.Contains("Facebook")).FirstOrDefault();

        // Seed ORDERS
        if (!_db.Orders.Any())
        {
            _logger.LogInformation("Started to Seed {Orders}.", nameof(_db.Orders));

            var orders = new List<Order>
            {
                new Order("Order 1A", "Order 1A from Google", true, false, googleSupplier!.Id ),
                new Order("Order 2A", "Order 2A from Amazon", true, false, amazonSupplier!.Id ),
                new Order("Order 3A", "Order 3A from Google", true, false, googleSupplier!.Id ),
                new Order("Order 4A", "Order 4A from Facebook", true, false, facebookSupplier!.Id ),
            };

            // Here you can use your own logic to populate the database.

            if (orders != null)
            {
                foreach (var order in orders)
                {
                    _logger.LogInformation("Seeding {Order} => Name: '{OrderName}' - Description: '{OrderDesc}' - SupplierId: '{SupplierId}'", nameof(Order), order.Name, order.Description, order.SupplierId);
                    await _db.Orders.AddAsync(order, cancellationToken);
                }
            }

            await _db.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Seeded {Orders} Complete.", nameof(_db.Orders));
        }

        var silicatoProduct = _db.StoreProducts.Where(sp => sp.Name.Contains("SILICATO")).FirstOrDefault();
        var canalProduct = _db.StoreProducts.Where(sp => sp.Name.Contains("CANAL")).FirstOrDefault();
        var casonetoProduct = _db.StoreProducts.Where(sp => sp.Name.Contains("CASONETO")).FirstOrDefault();
        var brocaProduct = _db.StoreProducts.Where(sp => sp.Name.Contains("BROCA")).FirstOrDefault();
        var cintaProduct = _db.StoreProducts.Where(sp => sp.Name.Contains("CINTA")).FirstOrDefault();
        var foseadoProduct = _db.StoreProducts.Where(sp => sp.Name.Contains("FOSEADO")).FirstOrDefault();

        var order1A = _db.Orders.Where(sp => sp.Name.Contains("Order 1A")).FirstOrDefault();
        var order2A = _db.Orders.Where(sp => sp.Name.Contains("Order 2A")).FirstOrDefault();
        var order3A = _db.Orders.Where(sp => sp.Name.Contains("Order 3A")).FirstOrDefault();
        var order4A = _db.Orders.Where(sp => sp.Name.Contains("Order 4A")).FirstOrDefault();

        // Seed ORDERPRODUCTS
        if (!_db.OrderProducts.Any())
        {
            _logger.LogInformation("Started to Seed {OrderProducts}.", nameof(_db.OrderProducts));

            var orderProducts = new List<OrderProduct>
            {
                // Order 1A - Product CASONETO
                new OrderProduct(order1A!.Id, casonetoProduct!.Id, 2),

                // Order 1A - Product BROCA HILTI
                new OrderProduct(order1A!.Id, brocaProduct!.Id, 4),

                // Order 1A - Product FOSEADO
                new OrderProduct(order1A!.Id, foseadoProduct!.Id, 41),

                // Order 2A - Product CANAL
                new OrderProduct(order2A!.Id, canalProduct!.Id, 19),

                // Order 2A - Product CINTA
                new OrderProduct(order2A!.Id, cintaProduct!.Id, 111),

                // Order 3A - Product SILICATO
                new OrderProduct(order3A!.Id, silicatoProduct!.Id, 29)
            };

            // Here you can use your own logic to populate the database.

            if (orderProducts != null)
            {
                foreach (var op in orderProducts)
                {
                    _logger.LogInformation("Seeding {OrderProduct} => OrderId: '{OrderId}' - ProductId: '{ProductId}' - Quantity: '{Quantity}'", nameof(OrderProduct), op.OrderId, op.ProductId, op.Quantity);
                    await _db.OrderProducts.AddAsync(op, cancellationToken);
                }
            }

            await _db.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Seeded {OrderProduct} Complete.", nameof(_db.OrderProducts));
        }
    }
}