using InfoApp.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InfoApp.Data.Models
{
    public class ExperienceLevel
    {
        public ExperienceLevel()
        {
            this.Employees = new HashSet<Employee>();
        }

        [Key]
        public int ExperienceLevelId { get; set; }

        [Required]
        [StringLength(maximumLength:GlobalConstants.ExpirienceLevelNameMaxLength)]
        public string ExperienceLevelName { get; set; }

        public ICollection<Employee> Employees { get; set; }
    }
}
