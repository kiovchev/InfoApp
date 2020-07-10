using System.Threading.Tasks;
using InfoApp.Common;
using InfoApp.Services.Data.Contracts;
using InfoApp.Web.MapDtoModels.CityMappers;
using InfoApp.Web.MapDtoModels.OfficeMappers;
using InfoApp.Web.Models.InputOutputModels.InputModels.OfficeInputModels;
using InfoApp.Web.Models.InputOutputModels.OutputModels.OfficeOtputModels;
using Microsoft.AspNetCore.Mvc;

namespace InfoApp.Web.Controllers
{
    // Offices for employees
    public class OfficeController : Controller
    {
        private readonly IOfficeService officeService;
        private readonly ICityService cityService;
        private readonly ICompanyService companyService;
        private readonly IEmployeeService employeeService;

        public OfficeController(IOfficeService officeService,
                                ICityService cityService,
                                ICompanyService companyService,
                                IEmployeeService employeeService)
        {
            this.officeService = officeService;
            this.cityService = cityService;
            this.companyService = companyService;
            this.employeeService = employeeService;
        }

        // Loads a view all offices for a company
        public async Task<IActionResult> All(int id)
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return Redirect("/Identity/Account/Login");
            }
            var offices = await this.officeService.GetAllOffices(id);
            var mapper = new OfficeOutputMapper();
            var officesAll = mapper.Map(offices);

            var model = new OfficesByCompanyOutputModel();
            model.CompanyId = id;
            model.Offices = officesAll;

            //TODO if there are no offices

            return this.View(model);
        }

        // Loads form for creating of new office 
        [HttpGet]
        public async Task<IActionResult> Create(int id)
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return Redirect("/Identity/Account/Login");
            }

            var cities = await this.cityService.GetAllCities();
            var cityMapper = new CityOutputMapper();
            var citiesAll = cityMapper.Map(cities);

            var company = await this.companyService.GetCompanyById(id);

            var model = new OfficeCreateInputModel();
            model.CompanyId = company.CompanyId;
            model.CompanyName = company.CompanyName;
            model.Cities = citiesAll;

            return this.View(model);
        }

        // Get info from office creation from and send it in office service 
        [HttpPost]
        public async Task<IActionResult> Create(OfficeCreatePostInputModel model)
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return Redirect("/Identity/Account/Login");
            }
            if (this.ModelState.IsValid && 
                model.OfficeName.Length >= GlobalConstants.OfficeNameMinLength &&
                model.StreetName.Length >= GlobalConstants.StreetNameMinLength &&
                model.Headquaters.Length >= GlobalConstants.HeadquatersMinLength &&
                model.CityName.Length >= GlobalConstants.CityNameMinLength)
            {
                var mapper = new OfficeCreateMapper();
                var dtoModel = mapper.Map(model);

                var ifCountryExists = await this.officeService.IfExists(dtoModel);
                
                if (ifCountryExists)
                {
                    return Redirect($"/Office/Exist/{model.CompanyId}");
                }

                await this.officeService.Create(dtoModel);
                return Redirect($"/Office/All/{model.CompanyId}");
            }

            return this.Redirect("/Office/All");
        }

        // Loads form for update of office
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return Redirect("/Identity/Account/Login");
            }

            var office = await this.officeService.GetOfficeById(id);
            var mapper = new OfficeEditOtputMapper();
            var currentModel = mapper.Map(office);

            var cities = await this.cityService.GetAllCities();
            var cityMapper = new CityOutputMapper();
            var citiesAll = cityMapper.Map(cities);

            var model = new OfficeEditOutputModel
            {
                officeModel = currentModel,
                Cities = citiesAll
            };

            return this.View(model);
        }

        // Get updated data from office edit form and send it in office service
        [HttpPost]
        public async Task<ActionResult> Edit(OfficeEditInputModel model)
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return Redirect("/Identity/Account/Login");
            }
            if (!ModelState.IsValid)
            {
                return Redirect("/Country/All");
            }

            var mapper = new OfficeEditInputMapper();
            var officeDtoModel = mapper.Map(model);

            var isSame = await this.officeService.IsSame(officeDtoModel);
            if (isSame)
            {
                return Redirect($"/Office/Exist/{model.CompanyId}");
            }

            await this.officeService.EditOffice(officeDtoModel);
            return Redirect($"/Office/All/{model.CompanyId}");
        }

        // send office id to office servise to delete currernt offcie by id
        public async Task<ActionResult> Delete(int id)
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return Redirect("/Identity/Account/Login");
            }

            var countOfEmployees = await this.employeeService.CountByOfficeIdAsync(id);
            if (countOfEmployees > 0)
            {
                return Redirect($"/Office/ExistEmployees/{id}");
            }

            var companyId = await this.officeService.DeleteOffice(id);
            return Redirect($"/Office/All/{companyId}");
        }

        // Loads view when want to create an offcie which exists in database
        public IActionResult Exist(int id)
        {
            var model = new OfficeExistOutputModel();
            model.Id = id;
            return this.View(model);
        }

        // Loads view when data after update is same like data for any office in database
        public IActionResult ExistEmployees(int id)
        {
            var companyId = this.officeService.GetCompanyIdByOfficeId(id);
            var model = new ExistEmployeesOfficeOutputModel();
            model.Id = companyId;
            return this.View(model);
        }
    }
}
