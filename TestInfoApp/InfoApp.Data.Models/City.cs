using InfoApp.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InfoApp.Data.Models
{
    public class City
    {
        public City()
        {
            this.Offices = new HashSet<Office>();
        }

        [Key]
        public int CityId { get; set; }

        [Required]
        [StringLength(maximumLength:GlobalConstants.CityNameMaxLength)]
        public string Name { get; set; }

        [Required]
        public int CountryId { get; set; }
        public Country Country { get; set; }

        public ICollection<Office> Offices { get; set; }
    }
}
