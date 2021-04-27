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

            modelBuilder.Entity<Sale>(entity =>
            {
                entity.ToTable("Sale");

                entity.Property(e => e.SaleId).ValueGeneratedNever();

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Sales)
                    .HasForeignKey(d => d.CustomerId)
                ;

                entity.HasOne(d => d.Salesperson)
                    .WithMany(p => p.Sales)
                    .HasForeignKey(d => d.SalespersonId)
                    ;
            });
            
        }
    }

}