using System.Collections.Generic;

namespace InfoApp.Common.DtoModels.EmployeeDtos
{
    public class EmployeeAllDtoModel
    {
        public EmployeeAllDtoModel()
        {
            this.Employees = new List<EmployeeDtoModel>();
        }

        public int CompanyId { get; set; }

        public List<EmployeeDtoModel> Employees { get; set; }
    }
}
