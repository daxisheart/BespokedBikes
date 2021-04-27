using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BespokedBikes.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [Range(0, float.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public float PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        public ICollection<Sale> Sales { get; set; }
    }
}
