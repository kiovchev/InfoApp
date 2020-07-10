using System.Collections.Generic;

namespace InfoApp.Web.Models.InputOutputModels.OutputModels.OfficeOtputModels
{
    public class OfficesByCompanyOutputModel
    {
        public int CompanyId { get; set; }
        public List<OfficeOutputModel> Offices { get; set; }
    }
}
