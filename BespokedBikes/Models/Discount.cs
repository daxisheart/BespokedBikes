using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BespokedBikes.Models
{
    public class Discount
    {
        [Key]
        public int DiscountId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Begin Date")]
        public DateTime BeginDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [Required]
        [Range(0,100)]
        [Display(Name = "Discount %")]
        public float DiscountPercentage { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
