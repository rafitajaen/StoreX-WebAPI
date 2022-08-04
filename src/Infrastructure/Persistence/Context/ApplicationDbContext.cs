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
    public DbSet<Delivery> Deliveries => Set<Delivery>();
    public DbSet<DeliveryProduct> DeliveryProducts => Set<DeliveryProduct>();

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

    }
}