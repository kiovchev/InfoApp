using InfoApp.Common;
using System.ComponentModel.DataAnnotations;

namespace InfoApp.Web.Models.InputOutputModels.InputModels.OfficeInputModels
{
    public class OfficeCreatePostInputModel
    {
        [Required]
        [StringLength(maximumLength:GlobalConstants.OfficeNameMaxLength)]
        public string OfficeName { get; set; }

        [Required]
        public int CompanyId { get; set; }

        [Required]
        [StringLength(maximumLength: GlobalConstants.CompanyNameMaxLength)]
        public string CompanyName { get; set; }

        [Required]
        [StringLength(maximumLength: GlobalConstants.CityNameMaxLength)]
        public string CityName { get; set; }

        [Required]
        [StringLength(maximumLength: GlobalConstants.StreetNameMaxLength)]
        public string StreetName { get; set; }

        [Required]
        public int StreetNumber { get; set; }

        [Required]
        [StringLength(maximumLength: GlobalConstants.HeadquatersMaxLength)]
        public string Headquaters { get; set; }
    }
}
