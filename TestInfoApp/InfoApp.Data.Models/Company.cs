using InfoApp.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InfoApp.Data.Models
{
    public class Company
    {
        public Company()
        {
            this.Offices = new HashSet<Office>();
            this.Employees = new HashSet<Employee>();
        }

        [Key]
        public int CompanyId { get; set; }

        [Required]
        [StringLength(maximumLength:GlobalConstants.CompanyNameMaxLength)]
        public string CompanyName { get; set; }

        [Required]
        public DateTime Creationdate { get; set; }

        public IEnumerable<Office> Offices { get; set; }

        public IEnumerable<Employee> Employees { get; set; }
    }
}
