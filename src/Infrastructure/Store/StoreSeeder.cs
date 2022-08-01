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

        var googleSupplier = _db.Suppliers.Where(s => s.Name.Contains("Google")).FirstOrDefault();
        var amazonSupplier = _db.Suppliers.Where(s => s.Name.Contains("Amazon")).FirstOrDefault();
        var facebookSupplier = _db.Suppliers.Where(s => s.Name.Contains("Facebook")).FirstOrDefault();

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

        var silicatoProduct = _db.StoreProducts.Where(sp => sp.Name.Contains("SILICATO")).FirstOrDefault();
        var canalProduct = _db.StoreProducts.Where(sp => sp.Name.Contains("CANAL")).FirstOrDefault();
        var casonetoProduct = _db.StoreProducts.Where(sp => sp.Name.Contains("CASONETO")).FirstOrDefault();
        var brocaProduct = _db.StoreProducts.Where(sp => sp.Name.Contains("BROCA")).FirstOrDefault();
        var cintaProduct = _db.StoreProducts.Where(sp => sp.Name.Contains("CINTA")).FirstOrDefault();
        var foseadoProduct = _db.StoreProducts.Where(sp => sp.Name.Contains("FOSEADO")).FirstOrDefault();

        //////////////////////////////
        // Seed ORDERS
        //////////////////////////////

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

        var order1A = _db.Orders.Where(sp => sp.Name.Contains("Order 1A")).FirstOrDefault();
        var order2A = _db.Orders.Where(sp => sp.Name.Contains("Order 2A")).FirstOrDefault();
        var order3A = _db.Orders.Where(sp => sp.Name.Contains("Order 3A")).FirstOrDefault();
        var order4A = _db.Orders.Where(sp => sp.Name.Contains("Order 4A")).FirstOrDefault();

        //////////////////////////////
        // Seed ORDERPRODUCTS
        //////////////////////////////

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

        var toshiba = _db.Customers.Where(p => p.Name.Contains("Toshiba")).FirstOrDefault();
        var pilot = _db.Customers.Where(p => p.Name.Contains("PILOT")).FirstOrDefault();

        //////////////////////////////
        // Seed Projects
        //////////////////////////////

        if (!_db.Projects.Any())
        {
            _logger.LogInformation("Started to Seed {Projects}.", nameof(_db.Projects));

            var projects = new List<Project>
            {
                new Project("Proyecto1", "Descripción Proyecto 1 de PILOT", false, pilot!.Id),
                new Project("Proyecto2", "Descripción Proyecto 2 de Toshiba", false, toshiba!.Id),
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

        var project1 = _db.Projects.Where(p => p.Name.Contains("Proyecto1")).FirstOrDefault();
        var project2 = _db.Projects.Where(p => p.Name.Contains("Proyecto2")).FirstOrDefault();

        //////////////////////////////
        // Seed Quotations
        //////////////////////////////

        if (!_db.Quotations.Any())
        {
            _logger.LogInformation("Started to Seed {Quotations}.", nameof(_db.Quotations));

            var quotations = new List<Quotation>
            {
                new Quotation("Quotation1", "Descripción Quotation 1 del Proyecto2", false, project2!.Id),
                new Quotation("Quotation2", "Descripción Quotation 2 del Proyecto1", false, project1!.Id),
            };

            // Here you can use your own logic to populate the database.

            if (quotations != null)
            {
                foreach (var op in quotations)
                {
                    _logger.LogInformation("Seeding {Quotation} => ProjectId: '{ProjectId}' - Name: '{Name}' - Description: '{Description}'", nameof(Quotation), op.ProjectId, op.Name, op.Description);
                    await _db.Quotations.AddAsync(op, cancellationToken);
                }
            }

            await _db.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Seeded {Quotations} Complete.", nameof(_db.Quotations));
        }

        var quotation1 = _db.Quotations.Where(p => p.Name.Contains("Quotation1")).FirstOrDefault();
        var quotation2 = _db.Quotations.Where(p => p.Name.Contains("Quotation2")).FirstOrDefault();

        //////////////////////////////
        // Seed Deliveries
        //////////////////////////////

        if (!_db.Deliveries.Any())
        {
            _logger.LogInformation("Started to Seed {Deliveries}.", nameof(_db.Deliveries));

            var deliveries = new List<Delivery>
            {
                new Delivery("Delivery1", "Descripción Delivery 1", false, false, project1!.Id),
                new Delivery("Delivery2", "Descripción Delivery 2", false, false, project2!.Id),
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

        var delivery1 = _db.Deliveries.Where(p => p.Name.Contains("Delivery1")).FirstOrDefault();
        var delivery2 = _db.Deliveries.Where(p => p.Name.Contains("Delivery2")).FirstOrDefault();

        //////////////////////////////
        // Seed QUOTATIONPRODUCTS
        //////////////////////////////

        if (!_db.QuotationProducts.Any())
        {
            _logger.LogInformation("Started to Seed {QuotationProducts}.", nameof(_db.QuotationProducts));

            var quotationProducts = new List<QuotationProduct>
            {
                // Quotation 1 - Product CASONETO
                new QuotationProduct(quotation1!.Id, casonetoProduct!.Id, 2),

                // Quotation 1 - Product BROCA HILTI
                new QuotationProduct(quotation1!.Id, brocaProduct!.Id, 4),

                // Quotation 1 - Product FOSEADO
                new QuotationProduct(quotation1!.Id, foseadoProduct!.Id, 41),

                // Quotation 1- Product CANAL
                new QuotationProduct(quotation1!.Id, canalProduct!.Id, 19),

                // Quotation 2 - Product CINTA
                new QuotationProduct(quotation2!.Id, cintaProduct!.Id, 111),

                // Quotation 2 - Product SILICATO
                new QuotationProduct(quotation2!.Id, silicatoProduct!.Id, 29)
            };

            // Here you can use your own logic to populate the database.

            if (quotationProducts != null)
            {
                foreach (var op in quotationProducts)
                {
                    _logger.LogInformation("Seeding {QuotationProduct} => QuotationId: '{QuotationId}' - ProductId: '{ProductId}' - Quantity: '{Quantity}'", nameof(QuotationProduct), op.QuotationId, op.ProductId, op.Quantity);
                    await _db.QuotationProducts.AddAsync(op, cancellationToken);
                }
            }

            await _db.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Seeded {QuotationProducts} Complete.", nameof(_db.QuotationProducts));
        }

        //////////////////////////////
        // Seed DELIVERYPRODUCTS
        //////////////////////////////

        if (!_db.DeliveryProducts.Any())
        {
            _logger.LogInformation("Started to Seed {DeliveryProducts}.", nameof(_db.DeliveryProducts));

            var deliveryProducts = new List<DeliveryProduct>
            {
                // Delivery 1 - Product CASONETO
                new DeliveryProduct(delivery1!.Id, casonetoProduct!.Id, 2),

                // Delivery 2 - Product BROCA HILTI
                new DeliveryProduct(delivery2!.Id, brocaProduct!.Id, 4),

                // Delivery 2 - Product FOSEADO
                new DeliveryProduct(delivery2!.Id, foseadoProduct!.Id, 41),

                // Delivery 2- Product CANAL
                new DeliveryProduct(delivery2!.Id, canalProduct!.Id, 19),

                // Delivery 2 - Product CINTA
                new DeliveryProduct(delivery2!.Id, cintaProduct!.Id, 111),

                // Delivery 2 - Product SILICATO
                new DeliveryProduct(delivery2!.Id, silicatoProduct!.Id, 29)
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