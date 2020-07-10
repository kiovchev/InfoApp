using System.Collections.Generic;

namespace InfoApp.Common.DtoModels.EmployeeDtos
{
    public class EmployeeAllByOfficeDtoModel
    {
        public EmployeeAllByOfficeDtoModel()
        {
            this.Employees = new List<EmployeeDtoModel>();
        }

        public int CompanyId { get; set; }

        public int OfficeId { get; set; }

        public List<EmployeeDtoModel> Employees { get; set; }
    }
}
