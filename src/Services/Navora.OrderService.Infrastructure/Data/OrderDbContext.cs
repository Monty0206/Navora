using Microsoft.EntityFrameworkCore;
using Navora.OrderService.Domain.Entities;

namespace Navora.OrderService.Infrastructure.Data;

public class OrderDbContext : DbContext
{
    public OrderDbContext(DbContextOptions<OrderDbContext> options)
        : base(options)
    {
    }

    public DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.OrderNumber).IsUnique();
            entity.HasIndex(e => e.Status);
            entity.HasIndex(e => e.CreatedAt);

            entity.Property(e => e.OrderNumber).IsRequired().HasMaxLength(50);
            entity.Property(e => e.CustomerName).IsRequired().HasMaxLength(200);
            entity.Property(e => e.CustomerPhone).IsRequired().HasMaxLength(20);
            entity.Property(e => e.PickupAddress).IsRequired().HasMaxLength(500);
            entity.Property(e => e.DeliveryAddress).IsRequired().HasMaxLength(500);

            entity.Property(e => e.Status).HasConversion<int>();
        });
    }
}