using Finbuckle.MultiTenant.EntityFrameworkCore;
using FSH.WebApi.Domain.Store;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSH.WebApi.Infrastructure.Persistence.Configuration;

public class StoreProductConfig : IEntityTypeConfiguration<StoreProduct>
{
    public void Configure(EntityTypeBuilder<StoreProduct> builder)
    {
        builder
        .ToTable("StoreProducts", SchemaNames.Store)
        .IsMultiTenant();

        builder
            .Property(b => b.Name)
                .HasMaxLength(1024);

        builder
            .Property(p => p.ImagePath)
                .HasMaxLength(2048);
    }
}

public class SupplierConfig : IEntityTypeConfiguration<Supplier>
{
    public void Configure(EntityTypeBuilder<Supplier> builder)
    {
        builder
        .ToTable("Suppliers", SchemaNames.Store)
        .IsMultiTenant();

        builder
            .Property(b => b.Name)
                .HasMaxLength(1024);

        builder
            .Property(p => p.ImagePath)
                .HasMaxLength(2048);
    }
}

public class OrderConfig : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder
        .ToTable("Orders", SchemaNames.Store)
        .IsMultiTenant();

        builder
            .Property(b => b.Name)
                .HasMaxLength(1024);
    }
}

public class OrderProductConfig : IEntityTypeConfiguration<OrderProduct>
{
    public void Configure(EntityTypeBuilder<OrderProduct> builder)
    {
        builder
        .ToTable("OrderProducts", SchemaNames.Store)
        .IsMultiTenant();
    }
}

public class CustomerConfig : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder
        .ToTable("Customers", SchemaNames.Store)
        .IsMultiTenant();

        builder
            .Property(b => b.Name)
                .HasMaxLength(1024);

        builder
            .Property(p => p.ImagePath)
                .HasMaxLength(2048);
    }
}

public class ProjectConfig : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder
        .ToTable("Projects", SchemaNames.Store)
        .IsMultiTenant();

        builder
            .Property(b => b.Name)
                .HasMaxLength(1024);
    }
}

public class DeliveryConfig : IEntityTypeConfiguration<Delivery>
{
    public void Configure(EntityTypeBuilder<Delivery> builder)
    {
        builder
        .ToTable("Deliveries", SchemaNames.Store)
        .IsMultiTenant();

        builder
            .Property(b => b.Name)
                .HasMaxLength(1024);
    }
}

public class DeliveryProductConfig : IEntityTypeConfiguration<DeliveryProduct>
{
    public void Configure(EntityTypeBuilder<DeliveryProduct> builder)
    {
        builder
        .ToTable("DeliveryProducts", SchemaNames.Store)
        .IsMultiTenant();
    }
}
