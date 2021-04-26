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

        public float GetSalesCommission(Sale sale)
        {

            float value = sale.SalesPrice - (1 - (sale.Product.ComissionPercentage / 100));

            return 0;
        }

        public float GetSalesPriceAfterDiscount(Sale sale)
        {
            //get if sale exist within discount start/end dates
            var disc = _context.Discount.Where(x => x.BeginDate <= sale.SalesDate && x.EndDate >= sale.SalesDate);
            if (disc.ToList().Count == 0) {
                return sale.SalesPrice;
            }

            //var discount =  _context.Discount.First(d => sale.SalesDate > d.BeginDate && sale.SalesDate < d.EndDate);
            //float discountedPrice = sale.Product.SalePrice * (1 - discount.DiscountPercentage / 100);
            float discountedPrice = sale.Product.SalePrice;
            return discountedPrice;
        }
        
        public string GetFullName(Customer customer)
        {
            return customer.FirstName + " " + customer.LastName;
        }

        public string GetFullName(Salesperson salesperson)
        {
            return salesperson.FirstName + " " + salesperson.LastName;
        }

        public List<Report> CreateReport(int quarter, int year)
        {
            //

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
            var sales = _context.Sale.Select(x => x.SalesDate <= startDate && x.SalesDate >= endDate).ToList();
            var salespersons = _context.Salesperson.Select(x => x).ToList();

            List<Report> report = new List<Report>();
            foreach(Salesperson salesperson in salespersons)
            {
                report.Add(new Report()
                {
                    Quarter = quarter,
                    Year = year,
                    SalesCommission = salesperson.Sales.Sum(x => x.SalesPrice),
                    NumProductsSold = salesperson.Sales.Select(x => x.SalesDate <= startDate && x.SalesDate >= endDate).ToList().Count,
                    SalespersonName = GetFullName(salesperson)
                });
            }


            return report;
        }
    }
}
