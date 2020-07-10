using System;
using System.ComponentModel.DataAnnotations;

namespace InfoApp.Web.Models.InputOutputModels.InputModels.CompanyInputModels
{
    public class CompanyEditInputModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength: 200)]
        public string CompanyName { get; set; }

        [Required]
        public DateTime Creationdate { get; set; }
    }
}
