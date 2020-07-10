using InfoApp.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InfoApp.Data.Models
{
    public class Office
    {
        public Office()
        {
            this.Employees = new HashSet<Employee>();
        }

        [Key]
        public int  OfficeId { get; set; }

        [Required]
        [StringLength(maximumLength:GlobalConstants.OfficeNameMaxLength)]
        public string  OfficeName { get; set; }

        [Required]
        public int CompanyId { get; set; }
        public Company Company { get; set; }

        [Required]
        public int CountryId { get; set; }
        public Country Country { get; set; }

        [Required]
        public int CityId { get; set; }
        public City City { get; set; }

        [Required]
        [StringLength(maximumLength:GlobalConstants.StreetNameMaxLength)]
        public string Street { get; set; }

        [Required]
        public int StreetNumber { get; set; }

        [Required]
        public bool Headquarters { get; set; }

        public IEnumerable<Employee> Employees { get; set; }
    }
}
