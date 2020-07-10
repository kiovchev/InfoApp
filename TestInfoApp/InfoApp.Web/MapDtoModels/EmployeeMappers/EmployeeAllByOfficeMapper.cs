using InfoApp.Common.DtoModels.EmployeeDtos;
using InfoApp.Web.Models.InputOutputModels.OutputModels.EmployeeOutputModels;
using System;
using System.Collections.Generic;

namespace InfoApp.Web.MapDtoModels.EmployeeMappers
{
    public class EmployeeAllByOfficeMapper
    {
        public EmployeeAllByOfficeModel Map(EmployeeAllByOfficeDtoModel model)
        {
            var employees = new List<EmployeeAllOutputModel>();

            foreach (var item in model.Employees)
            {
                var employee = new EmployeeAllOutputModel
                {
                    EmploeeId = item.EmploeeId,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    StartingDate = item.StartingDate.ToString("dd/MM/yyyy"),
                    Salary = Math.Round(item.Salary, 2),
                    VacationDays = item.VacationDays,
                    ExperienceLevel = item.ExperienceLevel
                };

                employees.Add(employee);
            }

            var employeesAll = new EmployeeAllByOfficeModel();
            employeesAll.CompanyId = model.CompanyId;
            employeesAll.OfficeId = model.OfficeId;
            employeesAll.Employees = employees;

            return employeesAll;
        }
    }
}
