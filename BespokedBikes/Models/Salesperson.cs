using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BespokedBikes.Models
{
    public class Salesperson
    {
        [Key]
        public int SalespersonId { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public string Address { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        public float PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Termination Date")]
        public DateTime? TerminationDate { get; set; }

        public string Manager { get; set; }

        public ICollection<Sale> Sales { get; set; }
    }
}
