using InfoApp.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace InfoApp.Web.Models.InputOutputModels.InputModels.EmployeeInputModels
{
    public class EmployeeCreateInputModel
    {
        [Required]
        [StringLength(maximumLength:GlobalConstants.EmployeeFirstNameMaxLength)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(maximumLength: GlobalConstants.EmployeeLastNameMaxLength)]
        public string LastName { get; set; }

        [Required]
        public DateTime StartingDate { get; set; }

        [Required]
        public decimal Salary { get; set; }

        [Required]
        public int VacationDays { get; set; }

        [Required]
        [StringLength(maximumLength:GlobalConstants.ExpirienceLevelNameMaxLength)]
        public string ExpirienceLevel { get; set; }
    }
}
