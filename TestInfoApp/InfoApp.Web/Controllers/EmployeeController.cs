using System;
using System.Threading.Tasks;
using InfoApp.Common;
using InfoApp.Services.Data.Contracts;
using InfoApp.Web.MapDtoModels.EmployeeMappers;
using InfoApp.Web.MapDtoModels.ExperienceLevelMapper;
using InfoApp.Web.MapDtoModels.OfficeMappers;
using InfoApp.Web.Models.InputOutputModels.InputModels.EmployeeInputModels;
using InfoApp.Web.Models.InputOutputModels.InputModels.EmployeeOutputModels;
using InfoApp.Web.Models.InputOutputModels.OutputModels.EmployeeOutputModels;
using Microsoft.AspNetCore.Mvc;

namespace InfoApp.Web.Controllers
{
    // Employees for offices and companies
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService employeeService;
        private readonly IExperienceLevelService levelService;
        private readonly IOfficeService officeService;

        public EmployeeController(IEmployeeService employeeService,
                                  IExperienceLevelService levelService,
                                  IOfficeService officeService)
        {
            this.employeeService = employeeService;
            this.levelService = levelService;
            this.officeService = officeService;
        }

        // Loads view with all employees for a company using company id 
        public async Task<IActionResult> AllByCompany(int id)
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return Redirect("/Identity/Account/Login");
            }
            var employees = await this.employeeService.GetAllEmployeesByCompany(id);
            var mapper = new EmployeeAllMapper();
            var employeesAll = mapper.Map(employees);

            return this.View(employeesAll);
        }

        // Loads view with all employees for an office using office id
        public async Task<IActionResult> AllByOffice(int id)
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return Redirect("/Identity/Account/Login");
            }
            var employees = await this.employeeService.GetAllEmployeesByOffice(id);
            var mapper = new EmployeeAllByOfficeMapper();
            var employeesAll = mapper.Map(employees);
            return this.View(employeesAll);
        }

        // Loads view for creation of new employee
        [HttpGet]
        public async Task<IActionResult> Create(int id)
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return Redirect("/Identity/Account/Login");
            }

            var levels = await this.levelService.GetAllLevels();
            var levelMapper = new LevelOutputMapper();
            var levelsAll = levelMapper.Map(levels);

            var office = await this.officeService.GetOfficeById(id);

            var model = new EmployeeCreateOutputModel();
            model.OfficeId = office.OfficeId;
            model.OfficeName = office.OfficeName;
            model.Levels = levelsAll;

            return this.View(model);
        }

        // Get data for creation of new employee and send it to employee service
        [HttpPost]
        public async Task<IActionResult> Create(int id, EmployeeCreateInputModel model)
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return Redirect("/Identity/Account/Login");
            }
            if (this.ModelState.IsValid && 
                model.FirstName.Length >= GlobalConstants.EmployeeFirstNameMinLength &&
                model.LastName.Length >= GlobalConstants.EmployeeLastNameMinLength && 
                model.ExpirienceLevel.Length >= GlobalConstants.ExpirienceLevelNameMinLength && 
                model.StartingDate <= DateTime.Now &&
                model.Salary >= 500 && 
                model.VacationDays >= 20)
            {
                var mapper = new EmployeeCreateMapper();
                var dtoModel = mapper.Map(id, model);

                var ifEmployeeExists = await this.employeeService.IfExists(dtoModel);
                if (ifEmployeeExists)
                {
                    return Redirect($"/Employee/Exist/{id}");
                }

                await this.employeeService.Create(dtoModel);
                return Redirect($"/Employee/AllByOffice/{id}");
            }

            return this.Redirect($"/Office/All/{id}");
        }

        // Loads view for update of employee 
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return Redirect("/Identity/Account/Login");
            }

            var companyId = this.employeeService.GetCompanyId(id);

            var levels = await this.levelService.GetAllLevels();
            var levelMapper = new LevelEmployeeEditMapper();
            var currentLevels = levelMapper.Map(levels);

            var offices = await this.officeService.GetAllOffices(companyId);
            var officeMapper = new OfficeEmployeeMapper();
            var currentOffices = officeMapper.Map(offices);

            var employee = await this.employeeService.GetEmployeeById(id);
            var employeeMapper = new EmployeeEditMapper();
            var currentEmployee = employeeMapper.Map(employee);

            var model = new FullEmployeeEditOutputModel();
            model.Employee = currentEmployee;
            model.Offices = currentOffices;
            model.Levels = currentLevels;

            return this.View(model);
        }


        // Get updated data for an employee and send it to employee service
        [HttpPost]
        public async Task<ActionResult> Edit(int id, EmployeeEditInputModel model)
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return Redirect("/Identity/Account/Login");
            }
            if (!ModelState.IsValid)
            {
                return Redirect("/Country/All");
            }

            var mapper = new EmployeeEditInputMapper();
            var employeeDtoModel = mapper.Map(id, model);

            var ifEmployeeIsSame = await this.employeeService.isSame(employeeDtoModel);
            if (ifEmployeeIsSame)
            {
                var officeIdByEmployeeId = await this.employeeService.GetOfficeIdAsync(id);
                return Redirect($"/Employee/Exist/{officeIdByEmployeeId}"); // need office id not employeeId
            }

            var officeId = await this.employeeService.EditEmployee(employeeDtoModel);

            return Redirect($"/Employee/AllByOffice/{officeId}");
        }

        // Get employee id and send it to employee sercive to delete it
        public async Task<ActionResult> Delete(int id)
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return Redirect("/Identity/Account/Login");
            }



            var officeId = await this.employeeService.DeleteEmployee(id);
            return Redirect($"/Employee/AllByOffice/{officeId}");
        }

        // Loads view when such an employee exists in a database (for create and edit)
        public IActionResult Exist(int id)
        {
            var model = new EmployeeExistOutputModel();
            model.Id = id;
            return this.View(model);
        }
    }
}
