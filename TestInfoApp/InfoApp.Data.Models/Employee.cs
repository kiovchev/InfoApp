using InfoApp.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace InfoApp.Data.Models
{
    public class Employee
    {
        [Key]
        public int EmploeeId { get; set; }

        [Required]
        [StringLength(maximumLength: GlobalConstants.EmployeeFirstNameMaxLength)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(maximumLength: GlobalConstants.EmployeeLastNameMaxLength)]
        public string LastName { get; set; }

        [Required]
        public DateTime StartingDate { get; set; }

        public decimal Salary { get; set; }

        public int VacationDays { get; set; }

        [Required]
        public int OfficeId { get; set; }
        public Office Office { get; set; }

        [Required]
        public int CompanyId { get; set; }
        public Company Company { get; set; }

        [Required]
        public int ExperienceLevelId { get; set; }
        public ExperienceLevel Level { get; set; }
    }
}
