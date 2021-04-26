using Microsoft.EntityFrameworkCore;
using BespokedBikes.Models;
using System.Collections.Generic;

namespace BespokedBikes.Data
{
    public class BespokedBikesContext : DbContext
    {
        public DbSet<Salesperson> Salesperson { get; set; }
        public DbSet<Sale> Sale { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Discount> Discount { get; set; }
        public DbSet<Report> Report { get; set; }

        public BespokedBikesContext(DbContextOptions<BespokedBikesContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.Property(e => e.CustomerId).ValueGeneratedNever();

            });
            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.ProductId).ValueGeneratedNever();

            });
            modelBuilder.Entity<Discount>(entity =>
            {
                entity.ToTable("Discount");

                entity.Property(e => e.DiscountId).ValueGeneratedNever();

            });
            modelBuilder.Entity<Salesperson>(entity =>
            {
                entity.ToTable("Salesperson");

                entity.Property(e => e.SalespersonId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Sale>(entity =>
            {
                entity.ToTable("Sale");

                entity.Property(e => e.SaleId).ValueGeneratedNever();

/*                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Sales)
                    .HasForeignKey(d => d.CustomerId);

                entity.HasOne(d => d.Salesperson)
                    .WithMany(p => p.Sales)
                    .HasForeignKey(d => d.SalespersonId);
*/            });
            
            OnModelCreating(modelBuilder);
        }
    }

}