using InfoApp.Web.Models.InputOutputModels.OutputModels.ExperienceLevelOutputModels;
using InfoApp.Web.Models.InputOutputModels.OutputModels.OfficeOtputModels;
using System.Collections.Generic;

namespace InfoApp.Web.Models.InputOutputModels.OutputModels.EmployeeOutputModels
{
    public class FullEmployeeEditOutputModel
    {
        public EmployeeEditOutputModel Employee { get; set; }

        public List<OfficeNamesByCompanyModel> Offices { get; set; }

        public List<LevelOutputModel> Levels { get; set; }
    }
}
