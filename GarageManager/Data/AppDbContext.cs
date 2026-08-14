using Microsoft.EntityFrameworkCore;
using GarageManager.Models;

namespace GarageManager.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<ServiceOrder> ServiceOrders { get; set; }
        public DbSet<ServiceOrderItem> ServiceOrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasMany(c => c.ServiceOrders)
                .WithOne(o => o.Customer)
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Car>()
                .HasMany(c => c.ServiceOrders)
                .WithOne(o => o.Car)
                .HasForeignKey(o => o.CarId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ServiceOrder>()
                .HasMany(o => o.Items)
                .WithOne(i => i.ServiceOrder)
                .HasForeignKey(i => i.ServiceOrderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ServiceOrderItem>()
                .Property(i => i.Quantity)
                .HasPrecision(18, 2);

            modelBuilder.Entity<ServiceOrderItem>()
                .Property(i => i.UnitPrice)
                .HasPrecision(18, 2);
        }
    }
}
