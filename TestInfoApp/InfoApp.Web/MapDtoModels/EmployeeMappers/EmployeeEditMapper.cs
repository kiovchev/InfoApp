using InfoApp.Common.DtoModels.EmployeeDtos;
using InfoApp.Web.Models.InputOutputModels.OutputModels.EmployeeOutputModels;

namespace InfoApp.Web.MapDtoModels.EmployeeMappers
{
    public class EmployeeEditMapper
    {
        public EmployeeEditOutputModel Map(EmployeeEditOutputDtoModel model)
        {
            var employee = new EmployeeEditOutputModel
            {
                EmployeeId = model.EmployeeId,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Salary = model.Salary,
                StartingDate = model.StartingDate,
                VacationDays = model.VacationDays,
                CompanyId = model.CompanyId,
                LevelName = model.LevelName,
                OfficeName = model.OfficeName
            };

            return employee;
        }
    }
}
