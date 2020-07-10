using InfoApp.Web.Models.InputOutputModels.OutputModels.ExperienceLevelOutputModels;
using System.Collections.Generic;

namespace InfoApp.Web.Models.InputOutputModels.InputModels.EmployeeOutputModels
{
    public class EmployeeCreateOutputModel
    {
        public int OfficeId { get; set; }

        public string OfficeName { get; set; }

        public List<LevelOutputModel> Levels { get; set; }
    }
}
