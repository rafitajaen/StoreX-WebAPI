using Finbuckle.MultiTenant;
using FSH.WebApi.Application.Common.Events;
using FSH.WebApi.Application.Common.Interfaces;
using FSH.WebApi.Domain.Catalog;
using FSH.WebApi.Infrastructure.Persistence.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

// StoreX Using
using FSH.WebApi.Domain.Store;

namespace FSH.WebApi.Infrastructure.Persistence.Context;

public class ApplicationDbContext : BaseDbContext
{
    public ApplicationDbContext(ITenantInfo currentTenant, DbContextOptions options, ICurrentUser currentUser, ISerializerService serializer, IOptions<DatabaseSettings> dbSettings, IEventPublisher events)
        : base(currentTenant, options, currentUser, serializer, dbSettings, events)
    {
    }

    public DbSet<Product> Products => Set<Product>();
    public DbSet<Brand> Brands => Set<Brand>();

    // StoreX DbSets
    public DbSet<Supplier> Suppliers => Set<Supplier>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<StoreProduct> StoreProducts => Set<StoreProduct>();
    public DbSet<OrderProduct> OrderProducts => Set<OrderProduct>();
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<Quotation> Quotations => Set<Quotation>();
    public DbSet<QuotationProduct> QuotationProducts => Set<QuotationProduct>();
    public DbSet<Delivery> Deliveries => Set<Delivery>();
    public DbSet<DeliveryProduct> DeliveryProducts => Set<DeliveryProduct>();
    public DbSet<Invoice> Invoices => Set<Invoice>();
    public DbSet<InvoiceProduct> InvoiceProducts => Set<InvoiceProduct>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // StoreX - OrderProducts - Many to Many Relationship

        modelBuilder.Entity<OrderProduct>()
            .HasKey(p => new { p.OrderId, p.ProductId });

        modelBuilder.Entity<OrderProduct>()
            .HasOne(p => p.Order)
            .WithMany(pc => pc.OrderProducts)
            .HasForeignKey(p => p.OrderId);

        modelBuilder.Entity<OrderProduct>()
            .HasOne(p => p.Product)
            .WithMany(pc => pc.OrderProducts)
            .HasForeignKey(p => p.ProductId);

        modelBuilder.HasDefaultSchema(SchemaNames.Store);

        // StoreX - QuotationProducts - Many to Many Relationship

        modelBuilder.Entity<QuotationProduct>()
            .HasKey(p => new { p.QuotationId, p.ProductId });

        modelBuilder.Entity<QuotationProduct>()
            .HasOne(p => p.Quotation)
            .WithMany(pc => pc.QuotationProducts)
            .HasForeignKey(p => p.QuotationId);

        modelBuilder.Entity<QuotationProduct>()
            .HasOne(p => p.Product)
            .WithMany(pc => pc.QuotationProducts)
            .HasForeignKey(p => p.ProductId);

        modelBuilder.HasDefaultSchema(SchemaNames.Store);

        // StoreX - DeliveryProducts - Many to Many Relationship

        modelBuilder.Entity<DeliveryProduct>()
            .HasKey(p => new { p.DeliveryId, p.ProductId });

        modelBuilder.Entity<DeliveryProduct>()
            .HasOne(p => p.Delivery)
            .WithMany(pc => pc.DeliveryProducts)
            .HasForeignKey(p => p.DeliveryId);

        modelBuilder.Entity<DeliveryProduct>()
            .HasOne(p => p.Product)
            .WithMany(pc => pc.DeliveryProducts)
            .HasForeignKey(p => p.ProductId);

        modelBuilder.HasDefaultSchema(SchemaNames.Store);

        // StoreX - InvoiceProducts - Many to Many Relationship

        modelBuilder.Entity<InvoiceProduct>()
            .HasKey(p => new { p.InvoiceId, p.ProductId });

        modelBuilder.Entity<InvoiceProduct>()
            .HasOne(p => p.Invoice)
            .WithMany(pc => pc.InvoiceProducts)
            .HasForeignKey(p => p.InvoiceId);

        modelBuilder.Entity<InvoiceProduct>()
            .HasOne(p => p.Product)
            .WithMany(pc => pc.InvoiceProducts)
            .HasForeignKey(p => p.ProductId);

        modelBuilder.HasDefaultSchema(SchemaNames.Store);

    }
}