using System.Reflection;
using FSH.WebApi.Application.Common.Interfaces;
using FSH.WebApi.Domain.Catalog;
using FSH.WebApi.Infrastructure.Persistence.Context;
using FSH.WebApi.Infrastructure.Persistence.Initialization;
using Microsoft.Extensions.Logging;

namespace FSH.WebApi.Infrastructure.Catalog;

public class SupplierSeeder : ICustomSeeder
{
    private readonly ISerializerService _serializerService;
    private readonly ApplicationDbContext _db;
    private readonly ILogger<SupplierSeeder> _logger;

    public SupplierSeeder(ISerializerService serializerService, ILogger<SupplierSeeder> logger, ApplicationDbContext db)
    {
        _serializerService = serializerService;
        _logger = logger;
        _db = db;
    }

    public async Task InitializeAsync(CancellationToken cancellationToken)
    {
        string? path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        if (!_db.Suppliers.Any())
        {
            _logger.LogInformation("Started to Seed Suppliers.");

            var suppliers = new List<Supplier> {
                new Supplier("Google LLC.", "Google LLC is an American multinational technology company that focuses on artificial intelligence.","123-456-789","tom@google.com","http://google.es","https://upload.wikimedia.org/wikipedia/commons/thumb/7/77/Google_Images_2015_logo.svg/800px-Google_Images_2015_logo.svg.png" ),
                new Supplier("Amazon Inc.", "American multinational technology company which focuses on e-commerce, cloud computing", "234-567-891","jeff@amazon.com","http://amazon.com", "https://m.media-amazon.com/images/I/31%2BDgxPWXtL.jpg"),
                new Supplier("Facebook", "Online social media and social networking service owned by American company Meta Platforms", "345-678-901", "mark@facebook.com", "http://facebook.com",  "https://www.redeszone.net/app/uploads-redeszone.net/2022/02/comprobar-inicios-sesion-facebook.jpg"),
                new Supplier("Tesla Inc.", "American multinational automotive and clean energy company headquartered in Austin, Texas",  "456-789-012", "elon@tesla.com","http://tesla.com", "https://openexpoeurope.com/wp-content/uploads/2016/12/tesla-logo-red-300x225.png")
            };

            // Here you can use your own logic to populate the database.
            // As an example, I am using a JSON file to populate the database.
            //string supplierData = await File.ReadAllTextAsync(path + "/Catalog/suppliers.json", cancellationToken);
            //var suppliers = _serializerService.Deserialize<List<Supplier>>(supplierData);

            if (suppliers != null)
            {
                foreach (var supplier in suppliers)
                {
                    _logger.LogWarning($"supplierId: {supplier.Id} - supplierName: {supplier.Name}");
                    await _db.Suppliers.AddAsync(supplier, cancellationToken);
                }
            }

            await _db.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Seeded Suppliers.");
        }
    }
}