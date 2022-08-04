using System.Reflection;
using FSH.WebApi.Application.Common.Interfaces;
using FSH.WebApi.Domain.Store;
using FSH.WebApi.Infrastructure.Persistence.Context;
using Microsoft.Extensions.Logging;

namespace FSH.WebApi.Infrastructure.Persistence.Initialization.Store;

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

        //////////////////////////////
        // Seed SUPPLIERS
        //////////////////////////////

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

        var supplier1 = _db.Suppliers.Where(s => s.Name.Contains("PORTLANND")).FirstOrDefault();
        var supplier2 = _db.Suppliers.Where(s => s.Name.Contains("CHEMICALS")).FirstOrDefault();
        var supplier3 = _db.Suppliers.Where(s => s.Name.Contains("ELKON")).FirstOrDefault();
        var supplier4 = _db.Suppliers.Where(s => s.Name.Contains("SONNEN")).FirstOrDefault();

        //////////////////////////////
        // Seed PRODUCTS
        //////////////////////////////

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

        var product1 = _db.StoreProducts.Where(sp => sp.Name.Contains("GROUND MOUNTING")).FirstOrDefault();
        var product2 = _db.StoreProducts.Where(sp => sp.Name.Contains("CEMENT COATING")).FirstOrDefault();
        var product3 = _db.StoreProducts.Where(sp => sp.Name.Contains("VIBRATING TABLE")).FirstOrDefault();
        var product4 = _db.StoreProducts.Where(sp => sp.Name.Contains("ROBINIA")).FirstOrDefault();
        var product5 = _db.StoreProducts.Where(sp => sp.Name.Contains("GP-RPL")).FirstOrDefault();
        var product6 = _db.StoreProducts.Where(sp => sp.Name.Contains("POCKETEVO")).FirstOrDefault();

        //////////////////////////////
        // Seed ORDERS
        //////////////////////////////

        if (!_db.Orders.Any())
        {
            _logger.LogInformation("Started to Seed {Orders}.", nameof(_db.Orders));

            var orders = new List<Order>
            {
                new Order("Order 1A - New Vibration Table", "First order of vibration tables", true, false, supplier1!.Id ),
                new Order("Order 2A - Wood", "Robinia squared timber for a customer request", true, false, supplier2!.Id ),
                new Order("Order 3A - Cement Stuff", "Cement coating and a Vibration table because we ran out of stock", true, false, supplier1!.Id ),
                new Order("Order 4A - Electric Stuff", "Items needed in a customer's project", true, false, supplier3!.Id ),
                new Order("Order 5A - Mounting system", "Re-stockage of mounting systems", true, false, supplier4!.Id ),
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

        var order1A = _db.Orders.Where(sp => sp.Name.Contains("Order 1A")).FirstOrDefault();
        var order2A = _db.Orders.Where(sp => sp.Name.Contains("Order 2A")).FirstOrDefault();
        var order3A = _db.Orders.Where(sp => sp.Name.Contains("Order 3A")).FirstOrDefault();
        var order4A = _db.Orders.Where(sp => sp.Name.Contains("Order 4A")).FirstOrDefault();
        var order5A = _db.Orders.Where(sp => sp.Name.Contains("Order 5A")).FirstOrDefault();

        //////////////////////////////
        // Seed ORDERPRODUCTS
        //////////////////////////////

        if (!_db.OrderProducts.Any())
        {
            _logger.LogInformation("Started to Seed {OrderProducts}.", nameof(_db.OrderProducts));

            var orderProducts = new List<OrderProduct>
            {
                new OrderProduct(order1A!.Id, product3!.Id, 5),
                new OrderProduct(order1A!.Id, product2!.Id, 18),

                new OrderProduct(order2A!.Id, product4!.Id, 20),

                new OrderProduct(order3A!.Id, product2!.Id, 40),
                new OrderProduct(order3A!.Id, product3!.Id, 40),

                new OrderProduct(order4A!.Id, product5!.Id, 95),
                new OrderProduct(order4A!.Id, product6!.Id, 15),

                new OrderProduct(order5A!.Id, product1!.Id, 3),
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

        //////////////////////////////
        // Seed CUSTOMERS
        //////////////////////////////

        if (!_db.Customers.Any())
        {
            _logger.LogInformation("Started to Seed {Customers}.", nameof(_db.Customers));

            var customers = new List<Customer>
            {
                SeederHelper.Customer1,
                SeederHelper.Customer2,
            };

            // Here you can use your own logic to populate the database.

            if (customers != null)
            {
                foreach (var op in customers)
                {
                    _logger.LogInformation("Seeding {Customer} => Name: '{Name}' - Description: '{Description}'", nameof(Customer), op.Name, op.Description);
                    await _db.Customers.AddAsync(op, cancellationToken);
                }
            }

            await _db.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Seeded {Customers} Complete.", nameof(_db.Customers));
        }

        var customer1 = _db.Customers.Where(p => p.Name.Contains("INOXYDA")).FirstOrDefault();
        var customer2 = _db.Customers.Where(p => p.Name.Contains("INNOVARIA")).FirstOrDefault();

        //////////////////////////////
        // Seed Projects
        //////////////////////////////

        if (!_db.Projects.Any())
        {
            _logger.LogInformation("Started to Seed {Projects}.", nameof(_db.Projects));

            var projects = new List<Project>
            {
                new Project("P1 - Oregon City Hall", "Construction of 5 new offices", false, customer1!.Id),
                new Project("P2 - O'Donnells Pub", "Initial electric installation", false, customer2!.Id),
            };

            // Here you can use your own logic to populate the database.

            if (projects != null)
            {
                foreach (var op in projects)
                {
                    _logger.LogInformation("Seeding {Project} => CustomerId: '{CustomerId}' - Name: '{Name}' - Description: '{Description}'", nameof(Project), op.CustomerId, op.Name, op.Description);
                    await _db.Projects.AddAsync(op, cancellationToken);
                }
            }

            await _db.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Seeded {Projects} Complete.", nameof(_db.Projects));
        }

        var project1 = _db.Projects.Where(p => p.Name.Contains("P1")).FirstOrDefault();
        var project2 = _db.Projects.Where(p => p.Name.Contains("P2")).FirstOrDefault();

        //////////////////////////////
        // Seed Deliveries
        //////////////////////////////

        if (!_db.Deliveries.Any())
        {
            _logger.LogInformation("Started to Seed {Deliveries}.", nameof(_db.Deliveries));

            var deliveries = new List<Delivery>
            {
                new Delivery("D1 - Cement and Wood", "First Materials to start the work", false, false, project1!.Id),
                new Delivery("D2 - Transmitters", "Transmitters and complements", false, false, project2!.Id),
                new Delivery("D3 - Electric installation", "Complete the offices", false, false, project1!.Id),
            };

            // Here you can use your own logic to populate the database.

            if (deliveries != null)
            {
                foreach (var op in deliveries)
                {
                    _logger.LogInformation("Seeding {Delivery} => ProjectId: '{ProjectId}' - Name: '{Name}' - Description: '{Description}'", nameof(Delivery), op.ProjectId, op.Name, op.Description);
                    await _db.Deliveries.AddAsync(op, cancellationToken);
                }
            }

            await _db.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Seeded {Deliveries} Complete.", nameof(_db.Deliveries));
        }

        var delivery1 = _db.Deliveries.Where(p => p.Name.Contains("D1")).FirstOrDefault();
        var delivery2 = _db.Deliveries.Where(p => p.Name.Contains("D2")).FirstOrDefault();
        var delivery3 = _db.Deliveries.Where(p => p.Name.Contains("D3")).FirstOrDefault();

        //////////////////////////////
        // Seed DELIVERYPRODUCTS
        //////////////////////////////

        if (!_db.DeliveryProducts.Any())
        {
            _logger.LogInformation("Started to Seed {DeliveryProducts}.", nameof(_db.DeliveryProducts));

            var deliveryProducts = new List<DeliveryProduct>
            {
                new DeliveryProduct(delivery1!.Id, product2!.Id, 4),
                new DeliveryProduct(delivery1!.Id, product3!.Id, 1),
                new DeliveryProduct(delivery1!.Id, product4!.Id, 10),

                new DeliveryProduct(delivery2!.Id, product5!.Id, 32),
                new DeliveryProduct(delivery2!.Id, product6!.Id, 2),

                new DeliveryProduct(delivery3!.Id, product1!.Id, 1),
                new DeliveryProduct(delivery3!.Id, product5!.Id, 18),
                new DeliveryProduct(delivery3!.Id, product6!.Id, 5),

            };

            // Here you can use your own logic to populate the database.

            if (deliveryProducts != null)
            {
                foreach (var op in deliveryProducts)
                {
                    _logger.LogInformation("Seeding {DeliveryProduct} => DeliveryId: '{DeliveryId}' - ProductId: '{ProductId}' - Quantity: '{Quantity}'", nameof(DeliveryProduct), op.DeliveryId, op.ProductId, op.Quantity);
                    await _db.DeliveryProducts.AddAsync(op, cancellationToken);
                }
            }

            await _db.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Seeded {DeliveryProducts} Complete.", nameof(_db.DeliveryProducts));
        }
    }
}