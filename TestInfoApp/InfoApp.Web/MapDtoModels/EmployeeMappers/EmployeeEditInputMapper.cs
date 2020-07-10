using InfoApp.Common.DtoModels.EmployeeDtos;
using InfoApp.Web.Models.InputOutputModels.InputModels.EmployeeInputModels;

namespace InfoApp.Web.MapDtoModels.EmployeeMappers
{
    public class EmployeeEditInputMapper
    {
        public EmployeeEditInputDtoModel Map(int employeeId, EmployeeEditInputModel inputModel)
        {
            var model = new EmployeeEditInputDtoModel
            {
                EmployeeId = employeeId,
                FirstName = inputModel.FirstName,
                LastName = inputModel.LastName,
                Salary = inputModel.Salary,
                StartingDate = inputModel.StartingDate,
                VacationDays = inputModel.VacationDays,
                LevelName = inputModel.LevelName,
                OfficeName = inputModel.OfficeName
            };

            return model;
        }
    }
}
