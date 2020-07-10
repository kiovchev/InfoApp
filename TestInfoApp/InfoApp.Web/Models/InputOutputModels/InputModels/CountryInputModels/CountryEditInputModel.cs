using InfoApp.Common;
using System.ComponentModel.DataAnnotations;

namespace InfoApp.Web.Models.InputOutputModels.InputModels.CountryInputModels
{
    public class CountryEditInputModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength:GlobalConstants.CountryNameMaxLength)]
        public string Name { get; set; }
    }
}
