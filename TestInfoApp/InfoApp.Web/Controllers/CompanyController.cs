using System;
using System.Threading.Tasks;
using InfoApp.Common;
using InfoApp.Services.Data.Contracts;
using InfoApp.Web.MapDtoModels.CompanyMappers;
using InfoApp.Web.Models.InputOutputModels.InputModels.CompanyInputModels;
using Microsoft.AspNetCore.Mvc;

namespace InfoApp.Web.Controllers
{
    // Companies for employees and for offices
    public class CompanyController : Controller
    {
        private readonly ICompanyService companyService;
        private readonly IOfficeService officeService;
        private readonly IEmployeeService employeeService;

        public CompanyController(ICompanyService companyService, 
                                 IOfficeService officeService,
                                 IEmployeeService employeeService)
        {
            this.companyService = companyService;
            this.officeService = officeService;
            this.employeeService = employeeService;
        }

        // Load view with all cities
        public async Task<IActionResult> All()
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return Redirect("/Identity/Account/Login");
            }
            var companies = await this.companyService.GetAllCompanies();
            var mapper = new CompanyOutputMapper();
            var companiesAll = mapper.Map(companies);

            return this.View(companiesAll);
        }

        // Load view for creation of new Company
        public IActionResult Create()
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return Redirect("/Identity/Account/Login");
            }
            return this.View();
        }

        // Input information for creation of new company
        [HttpPost]
        public async Task<IActionResult> Create(CompanyCreateInputModel model)
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return Redirect("/Identity/Account/Login");
            }
            if (this.ModelState.IsValid && model.Creationdate <= DateTime.Now
                                        && model.CompanyName.Length >= GlobalConstants.CompanyNameMinLength)
            {
                var ifCountryExists = this.companyService.IfExists(model.CompanyName);
                if (ifCountryExists)
                {
                    return Redirect("/Company/Exist");
                }
                else
                {
                    await this.companyService.Create(model.CompanyName, model.Creationdate);
                    return Redirect("/Company/All");
                }
            }

            return Redirect("/Company/All");
        }

        // Load view for update 
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return Redirect("/Identity/Account/Login");
            }

            var company = await this.companyService.GetCompanyById(id);
            var mapper = new CompanyEditInputMapper();
            var currentModel = mapper.Map(company);
            return this.View(currentModel);
        }

        // Input information for update of company
        [HttpPost]
        public async Task<ActionResult> Edit(CompanyEditInputModel model)
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return Redirect("/Identity/Account/Login");
            }
            if (!ModelState.IsValid && model.CompanyName.Length < GlobalConstants.CompanyNameMinLength)
            {
                return Redirect("/Country/All");
            }

            var isSame = this.companyService.IsSame(model.CompanyName, model.Creationdate);
            if (isSame)
            {
                return Redirect("/Company/Exist");
            }

            var mapper = new CompanyDtoModelMapper();
            var companyDtoModel = mapper.Map(model);
            await this.companyService.EditCompany(companyDtoModel);
            return Redirect("/Company/All");
        }

        // Delete company
        public async Task<IActionResult> Delete(int id)
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return Redirect("/Identity/Account/Login");
            }

            var officesCount = await this.officeService.CountAsyncByCompanyId(id);
            var employeesCount = await this.employeeService.CountAsyncByCompanyId(id);

            if (officesCount > 0 || employeesCount > 0)
            {
                return Redirect("/Company/ExistOfficesOrEMployees");
            }

            var company = await this.companyService.GetCompanyById(id);
            if (company == null) 
            {
                return Redirect("/Company/All");
            }

            await this.companyService.DeleteCompany(id);
            return Redirect("/Company/All");
        }

        // Load view with message - if company exists 
        public IActionResult Exist()
        {
            return this.View();
        }

        // Load view with message - if there is any office or employee for current company 
        public IActionResult ExistOfficesOrEMployees()
        {
            return this.View();
        }
    }
}
