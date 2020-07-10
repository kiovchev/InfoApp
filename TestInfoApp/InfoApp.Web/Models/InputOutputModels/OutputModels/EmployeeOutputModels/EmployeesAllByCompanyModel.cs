using System.Collections.Generic;

namespace InfoApp.Web.Models.InputOutputModels.OutputModels.EmployeeOutputModels
{
    public class EmployeesAllByCompanyModel
    {
        public EmployeesAllByCompanyModel()
        {
            this.Employees = new List<EmployeeAllOutputModel>();
        }

        public int CompanyId { get; set; }

        public List<EmployeeAllOutputModel> Employees { get; set; }
    }
}
