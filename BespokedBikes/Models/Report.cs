using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BespokedBikes.Models
{
    public class Report
    {

        public Report()
        {
        }
        [Key]
        public int ReportId { get; set; }

        public int SalespersonId { get; set; }

        [Display(Name = "Salesperson")]
        public string SalespersonName { get; set; }
        
        [Range(1,4)]
        public int Quarter { get; set; }

        [Range(0, 10000)]
        public int Year { get; set; }

        [Display(Name = "Num of Products Sold")]
        public int NumProductsSold { get; set; }

        [Display(Name = "Sales Comission Amount")]
        [Range(0, 100, ErrorMessage = "Please enter valid percentage")]
        public double SalesCommission { get; set; }

    }
}
