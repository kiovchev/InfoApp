using InfoApp.Web.Models.InputOutputModels.OutputModels.CountryOutputModels;
using System.Collections.Generic;

namespace InfoApp.Web.Models.InputOutputModels.InputModels.CityInputModels
{
    public class CityALlCountriesEditModel
    {
        public CityEditInputModel City { get; set; }

        public List<CountryOutputModel> Countries { get; set; }
    }
}
