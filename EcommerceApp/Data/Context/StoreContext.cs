using EcommerceApp.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApp.Data.Context
{
    public class StoreContext : IdentityDbContext<ApplicationUser>
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options) { }     
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Return> Returns { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Shipment> Shipments { get; set; }
        public DbSet<ShipmentCarrier> ShipmentCarriers { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
      
            builder.Entity<Product>(entity =>
            {
                entity.HasOne(p => p.Brand)
                .WithMany(b => b.Products)
                .HasForeignKey(p => p.BrandId)
                .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.SetNull);
            });

            builder.Entity<Inventory>(entity =>
            {
                entity.HasOne(i => i.Product)
                .WithMany(p => p.Inventories)
                .HasForeignKey(i => i.ProductId)
                .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(i => i.Warehouse)
                .WithMany(w => w.Inventories)
                .HasForeignKey(i => i.WarehouseId)
                .OnDelete(DeleteBehavior.SetNull);
            });

            builder.Entity<OrderDetail>(entity =>
            {
                entity.HasOne(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OrderId)
                .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(od => od.Product)
                .WithMany(p => p.OrderDetails)
                .HasForeignKey(od => od.ProductId)
                .OnDelete(DeleteBehavior.SetNull);
            });

            builder.Entity<Review>(entity =>
            {
                entity.HasOne(rv => rv.Product)
                .WithMany(p => p.Reviews)
                .HasForeignKey(rv => rv.ProductId)
                .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(rv => rv.ApplicationUser)
                .WithMany(u => u.Reviews)
                .HasForeignKey(rv => rv.UserId)
                .OnDelete(DeleteBehavior.SetNull);
            });

            builder.Entity<Order>(entity =>
            {
                entity.HasOne(o => o.OrderUser)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.SetNull);
            });

            builder.Entity<Return>(entity =>
            {
                entity.HasOne(rt => rt.Order)
                .WithMany(o => o.Returns)
                .HasForeignKey(rt => rt.OrderId)
                .OnDelete(DeleteBehavior.SetNull);
            });

            builder.Entity<Payment>(entity =>
            {
                entity.HasOne(py => py.Order)
                .WithMany(o => o.Payments)
                .HasForeignKey(py => py.OrderId)
                .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(py => py.Discount)
                .WithMany(d => d.Payments)
                .HasForeignKey(py => py.DiscountId)
                .OnDelete(DeleteBehavior.SetNull);
            });

            builder.Entity<Shipment>(entity =>
            {
                entity.HasOne(s => s.Order)
                .WithMany(o => o.Shipments)
                .HasForeignKey(s => s.OrderId)
                .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(s => s.ShipmentCarrier)
                .WithMany(c => c.Shipments)
                .HasForeignKey(s => s.CarrierId)
                .OnDelete(DeleteBehavior.SetNull);
            });
        }
    }
}
