using System.Collections.Generic;

namespace InfoApp.Web.Models.InputOutputModels.OutputModels.EmployeeOutputModels
{
    public class EmployeeAllByOfficeModel
    {
        public EmployeeAllByOfficeModel()
        {
            this.Employees = new List<EmployeeAllOutputModel>();
        }

        public int CompanyId { get; set; }

        public int OfficeId { get; set; }

        public List<EmployeeAllOutputModel> Employees { get; set; }
    }
}
