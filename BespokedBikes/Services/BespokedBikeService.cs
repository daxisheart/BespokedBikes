using BespokedBikes.Data;
using BespokedBikes.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BespokedBikes.Services
{
    public class BespokedBikeService
    {

        private readonly BespokedBikesContext _context;

        public BespokedBikeService(BespokedBikesContext ctx)
        {
            _context = ctx;
        }

        public Customer GetCustomer(Customer Customer)
        {
            return null;
        }

        public float GetSalesCommission(Sale sale)
        {

            float value = sale.SalesPrice - (1 - (sale.Product.ComissionPercentage / 100));

            return 0;
        }

        public Sale CreateSale(Sale sale)
        {
            sale.SalesPrice = GetSalesPriceAfterDiscount(sale);

            return sale;

        }

        public float GetSalesPriceAfterDiscount(Sale sale)
        {
            var disc = _context.Discount.Where(x => x.BeginDate >= sale.SalesDate && x.EndDate <= sale.SalesDate).ToList();
            if (disc.Count == 0) {
                return sale.SalesPrice;
            }

            if (sale.Product == null)
            {
                return 100;
            }

            float discountedPrice = sale.Product.SalePrice;



            foreach (Discount dis in disc)
            {
                discountedPrice = discountedPrice * (1 - dis.DiscountPercentage/100);
            }
            return discountedPrice;
        }
        
        public string GetFullName(Customer customer)
        {
            if(customer == null)
            {
                return "Full name.";
            }

            return customer.FirstName + " " + customer.LastName;
        }

        public string GetFullName(Salesperson salesperson)
        {
            if (salesperson == null)
            {
                return "Full name.";
            }
            return salesperson.FirstName + " " + salesperson.LastName;
        }

        public List<Report> CreateReport(int quarter, int year)
        {
            DateTime startDate, endDate;
            switch (quarter)
            {
                case 1:
                    startDate = new DateTime(year, 1, 1);
                    endDate = new DateTime(year, 3, 31);
                    break;

                case 2:
                    startDate = new DateTime(year, 4, 1);
                    endDate = new DateTime(year, 6, 30);
                    break;

                case 3:
                    startDate = new DateTime(year, 7, 1);
                    endDate = new DateTime(year, 9, 30);
                    break;

                default:
                    startDate = new DateTime(year, 10, 1);
                    endDate = new DateTime(year, 12, 31);
                    break;
            }

            var discounts = _context.Discount.Where(x => x.BeginDate >= startDate && x.EndDate <= endDate).ToList();
            var sales = _context.Sale.Where(x => x.SalesDate >= startDate && x.SalesDate <= endDate).ToList();
            var salespersons = _context.Salesperson.Select(x => x).ToList();

            List<Report> report = new List<Report>();
            foreach(Salesperson salesperson in salespersons)
            {
                int numSold;
                float comission;
                if (salesperson.Sales == null) {
                    numSold = 0;
                    comission = 0;
                } else
                {
                    numSold = salesperson.Sales.Where(x => x.SalesDate >= startDate && x.SalesDate <= endDate).Count();
                    comission = salesperson.Sales.Sum(x => x.SalesPrice);
                }
                Report r = new Report()
                {
                    Quarter = quarter,
                    Year = year,
                    SalesCommission = comission,
                    NumProductsSold = numSold,
                    SalespersonName = GetFullName(salesperson)
                };
                report.Add(r);
            }


            return report;
        }
    }
}
