using InfoApp.Web.Models.InputOutputModels.InputModels.OfficeInputModels;
using InfoApp.Web.Models.InputOutputModels.OutputModels.CityOutputModels;
using System.Collections.Generic;

namespace InfoApp.Web.Models.InputOutputModels.OutputModels.OfficeOtputModels
{
    public class OfficeEditOutputModel
    {
        public OfficeEditCurrentOutputModel officeModel { get; set; }

        public List<CityOutputModel> Cities { get; set; }
    }
}
