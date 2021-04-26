using BespokedBikes.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BespokedBikes.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {

            using (var context = new BespokedBikesContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<BespokedBikesContext>>()))
            {
                context.Database.EnsureCreated();

                // Look for any movies.
                if (context.Product.Any() || context.Customer.Any() || context.Discount.Any() || context.Salesperson.Any() || context.Sale.Any())
                {
                    return;   // DB has been seeded
                }

                List<Product> products = new List<Product>();
                List<Customer> customers = new List<Customer>();
                List<Discount> discounts = new List<Discount>();
                List<Salesperson> salespersons = new List<Salesperson>();
                List<Sale> sales = new List<Sale>();

                for (int i = 0; i < 10; i++)
                {
                    products.Add(new Product()
                    {
                        ProductId = i,
                        Name = "Book" + i.ToString(),
                        Style = "New" + i.ToString(),
                        Manufacturer = "BookMakers" + i.ToString(),
                        PurchasePrice = 20 + i,
                        Quantity = 5 + i,
                        SalePrice = 25 + i,
                        ComissionPercentage = 10 + i,
                    });
                }

                for (int i = 0; i < 10; i++)
                {
                    customers.Add(new Customer()
                    {
                        CustomerId = i,
                        Address = "House" + i.ToString(),
                        FirstName = "Jack" + i.ToString(),
                        LastName = "NoLastNAme" + i.ToString(),
                        PhoneNumber = 2000000000 + i,
                        StartDate = DateTime.Parse("2000-2-12").AddMonths(i),
                        Sales = new List<Sale>()
                    });
                }

                for (int i = 0; i < 3; i++)
                {
                    discounts.Add(new Discount()
                    {
                        DiscountId = i,
                        DiscountPercentage = 10 + i*5,
                        BeginDate = DateTime.Parse("2000-2-12").AddMonths(i),
                        EndDate = DateTime.Parse("2000-2-12").AddMonths(i + 3),
                        Product = products[i],
                        ProductId = products[i].ProductId
                    });
                }

                for (int i = 0; i < 4; i++)
                {
                    salespersons.Add(new Salesperson()
                    {
                        SalespersonId = i,
                        Address = "Book" + i.ToString(),
                        FirstName = "Normal" + i.ToString(),
                        LastName = "LastNameDude" + i.ToString(),
                        PhoneNumber = 200000000 + i,
                        StartDate = DateTime.Parse("2000-2-12").AddMonths(i),
                        Manager = "Man" + i.ToString(),
                        Sales = new List<Sale>()
                    });
                }

                for (int i = 0; i < 12; i++)
                {
                    Sale sale = new Sale()
                    {
                        SaleId = i,
                        Salesperson = salespersons[i % salespersons.Count],
                        SalespersonId = salespersons[i % salespersons.Count].SalespersonId,
                        CustomerId = customers[i % customers.Count].CustomerId,
                        Customer = customers[i % customers.Count],
                        Product = products[i % products.Count],
                        ProductId = products[i % products.Count].ProductId,
                        SalesPrice = i * 50,
                        SalesDate = DateTime.Parse("2000-2-12").AddMonths(i)
                    };
                    sales.Add(sale);
                    salespersons[i % salespersons.Count].Sales.Add(sale);
                    customers[i % customers.Count].Sales.Add(sale);
                    customers[i % customers.Count].Sales.Add(sale);
                }

                context.Product.AddRange(products.ToArray());
                context.Customer.AddRange(customers.ToArray());
                context.Discount.AddRange(discounts.ToArray());
                context.Salesperson.AddRange(salespersons.ToArray());
                context.Sale.AddRange(sales.ToArray());

                context.SaveChanges();
            }
        }
    }
}
