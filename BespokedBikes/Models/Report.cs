using System;
using System.ComponentModel.DataAnnotations;

namespace BespokedBikes.Models
{
    public class Report
    {

        [Key]
        public int ReportId { get; set; }

        public int SalespersonId { get; set; }

        [Display(Name = "Salesperson")]
        public string SalespersonName { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Display(Name = "Num of Products Sold")]
        public int NumProductsSold { get; set; }

        [Display(Name = "Sales Comission Amount")]
        [Range(0, 100, ErrorMessage = "Please enter valid percentage")]
        public double SalesCommission { get; set; }

    }
}
