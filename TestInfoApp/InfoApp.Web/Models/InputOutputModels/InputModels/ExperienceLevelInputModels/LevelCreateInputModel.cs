using InfoApp.Common;
using System.ComponentModel.DataAnnotations;

namespace InfoApp.Web.Models.InputOutputModels.InputModels.ExperienceLevelInputModels
{
    public class LevelCreateInputModel
    {
        [Required]
        [StringLength(maximumLength: GlobalConstants.ExpirienceLevelNameMaxLength)]
        public string Name { get; set; }
    }
}
