using InfoApp.Common;
using System.ComponentModel.DataAnnotations;

namespace InfoApp.Web.Models.InputOutputModels.InputModels.CountryInputModels
{
    public class CountryCreateInputModel
    {
        [Required]
        [StringLength(maximumLength:GlobalConstants.CountryNameMaxLength)]
        public string Name { get; set; }
    }
}
