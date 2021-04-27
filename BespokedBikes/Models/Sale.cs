using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BespokedBikes.Models
{
    
    public class Sale
    {
        [Key]
        public int SaleId { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        [ForeignKey("Salesperson")]
        public int SalespersonId { get; set; }

        public Salesperson Salesperson { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Sales Date")]
        public DateTime SalesDate { get; set; }

        [Required]
        [Display(Name = "Sales Price")]
        [Range(0, float.MaxValue, ErrorMessage = "Please enter valid Price")]
        public float SalesPrice { get; set; }
    }
}
