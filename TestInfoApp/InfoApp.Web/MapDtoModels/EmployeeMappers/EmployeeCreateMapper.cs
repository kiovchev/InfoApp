using InfoApp.Common.DtoModels.EmployeeDtos;
using InfoApp.Web.Models.InputOutputModels.InputModels.EmployeeInputModels;

namespace InfoApp.Web.MapDtoModels.EmployeeMappers
{
    public class EmployeeCreateMapper
    {
        public EmployeeCreateInputDtoModel Map(int officeId, EmployeeCreateInputModel model)
        {
            var employee = new EmployeeCreateInputDtoModel
            {
                OfficeId = officeId,
                FirstName = model.FirstName,
                LastName = model.LastName,
                StartingDate = model.StartingDate,
                Salary = model.Salary,
                VacationDays = model.VacationDays,
                ExpirienceLevel = model.ExpirienceLevel
            };

            return employee;
        }
    }
}
