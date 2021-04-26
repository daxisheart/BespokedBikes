using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BespokedBikes.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Manufacturer { get; set; }
        
        [Required]
        public string Style { get; set; }

        [Display(Name = "Purchase Price")]
        [Required]
        [Range(0, float.MaxValue, ErrorMessage = "Please enter valid Price")]
        public float PurchasePrice { get; set; }

        [Required]
        [Display(Name = "Sales Price")]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter valid Price")]
        public float SalePrice { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int Quantity { get; set; }

        [Required]
        [Range(0,100)]
        [Display(Name = "Comission Percentage")]
        public float ComissionPercentage { get; set; }
    }
}
