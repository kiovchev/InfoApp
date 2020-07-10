using InfoApp.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InfoApp.Data.Models
{
    public class Country
    {
        public Country()
        {
            this.Offices = new HashSet<Office>();
            this.Cities = new HashSet<City>();
        }

        [Key]
        public int CountryId { get; set; }


        [Required]
        [StringLength(maximumLength:GlobalConstants.CountryNameMaxLength)]
        public string CountryName { get; set; }

        public ICollection<Office> Offices { get; set;  }

        public ICollection<City> Cities { get; set;  }
    }
}
