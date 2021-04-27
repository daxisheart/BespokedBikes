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
                //context.Database.EnsureCreated();
                bool hasProduct = context.Product.Any();
                bool hasCustomer = context.Customer.Any();
                bool hasDiscount = context.Discount.Any();
                bool hasSalespersom = context.Salesperson.Any();
                bool hasSale = context.Sale.Any();
                if ( hasProduct || hasCustomer || hasDiscount || hasSalespersom || hasSale)
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
                        //ProductId = i,
                        Name = "Bike" + i.ToString(),
                        Style = "New" + i.ToString(),
                        Manufacturer = "BikeMakers" + i.ToString(),
                        PurchasePrice = 20 + i,
                        Quantity = 5 + i,
                        SalePrice = 25 + i,
                        ComissionPercentage = 10 + i,
                    });
                }

                context.Product.AddRange(products.ToArray());
                context.SaveChanges();

                for (int i = 0; i < 10; i++)
                {
                    customers.Add(new Customer()
                    {
                        //CustomerId = i,
                        Address = "House" + i.ToString(),
                        FirstName = "Jack" + i.ToString(),
                        LastName = "NoLastNAme" + i.ToString(),
                        PhoneNumber = 2000000000 + i,
                        StartDate = DateTime.Parse("2000-2-12").AddMonths(i),
                        Sales = new List<Sale>()
                    });
                }

                context.Customer.AddRange(customers.ToArray());
                context.SaveChanges();

                for (int i = 0; i < 4; i++)
                {
                    List<int> prodID = context.Product.Select(x => x.ProductId).ToList();
                    discounts.Add(new Discount()
                    {
                        //DiscountId = i,
                        DiscountPercentage = 10 + i*5,
                        BeginDate = DateTime.Parse("2000-2-12").AddMonths(i),
                        EndDate = DateTime.Parse("2000-2-12").AddMonths(i + 3),
                        //Product = products[i],
                        ProductId = prodID[i % prodID.Count]
                    });;
                }

                context.Discount.AddRange(discounts.ToArray());
                context.SaveChanges();


                for (int i = 0; i < 4; i++)
                {
                    salespersons.Add(new Salesperson()
                    {
                        //SalespersonId = i,
                        Address = "Book" + i.ToString(),
                        FirstName = "Normal" + i.ToString(),
                        LastName = "LastNameDude" + i.ToString(),
                        PhoneNumber = 200000000 + i,
                        StartDate = DateTime.Parse("2000-2-12").AddMonths(i),
                        Manager = "Man" + i.ToString(),
                        Sales = new List<Sale>()
                    });
                }

                context.Salesperson.AddRange(salespersons.ToArray());
                context.SaveChanges();


                for (int i = 0; i < 12; i++)
                {
                    List<int> prodID = context.Product.Select(x => x.ProductId).ToList();
                    List<int> custID = context.Customer.Select(x => x.CustomerId).ToList();
                    List<int> salespID = context.Salesperson.Select(x => x.SalespersonId).ToList();

                    Sale sale = new Sale()
                    {
                        //SaleId = i,
                        //Salesperson = salespersons[i % salespersons.Count],
                        SalespersonId = salespID[i % salespID.Count],
                        CustomerId = custID[i % custID.Count],
                        //Customer = customers[i % customers.Count],
                        //Product = products[i % products.Count],
                        ProductId = prodID[i % prodID.Count],
                        SalesPrice = i * 50,
                        SalesDate = DateTime.Parse("2000-2-12").AddMonths(i)
                    };
                    sales.Add(sale);


                    //salespersons[i % salespersons.Count].Sales.Add(sale);
                    //customers[i % customers.Count].Sales.Add(sale);
                }


                context.Sale.AddRange(sales.ToArray());
                context.SaveChanges();


                discounts = context.Discount.ToList();
                foreach (Discount d in discounts)
                {
                    d.Product = context.Product.Where(x => x.ProductId == d.ProductId).First();
                }
                context.Discount.UpdateRange(discounts);
                context.SaveChanges();

                sales = context.Sale.ToList();
                foreach (Sale s in sales)
                {
                    s.Product = context.Product.Where(x => x.ProductId == s.ProductId).First();
                    s.Salesperson = context.Salesperson.Where(x => x.SalespersonId == s.SalespersonId).First();
                    s.Customer = context.Customer.Where(x => x.CustomerId == s.CustomerId).First();
                }
                context.Sale.UpdateRange(sales);
                context.SaveChanges();

            }
        }
    }
}
