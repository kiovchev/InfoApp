using InfoApp.Web.Models.InputOutputModels.OutputModels.CityOutputModels;
using InfoApp.Web.Models.InputOutputModels.OutputModels.CountryOutputModels;
using System.Collections.Generic;

namespace InfoApp.Web.Models.InputOutputModels.InputModels.OfficeInputModels
{
    public class OfficeCreateInputModel
    {
        public int CompanyId { get; set; }

        public string CompanyName { get; set; }

        public List<CityOutputModel> Cities { get; set; }
    }
}
