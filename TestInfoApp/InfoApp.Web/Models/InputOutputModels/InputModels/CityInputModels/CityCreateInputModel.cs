using InfoApp.Common;
using System.ComponentModel.DataAnnotations;

namespace InfoApp.Web.Models.InputOutputModels.InputModels.CityInputModels
{
    public class CityCreateInputModel
    {
        [Required]
        [StringLength(maximumLength:GlobalConstants.CityNameMaxLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(maximumLength:GlobalConstants.CountryNameMaxLength)]
        public string CountryName { get; set; }
    }
}
