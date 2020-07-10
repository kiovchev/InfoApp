using InfoApp.Common;
using InfoApp.Services.Data.Contracts;
using InfoApp.Web.MapDtoModels.CountryMappers;
using InfoApp.Web.Models.InputOutputModels.InputModels.CountryInputModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace InfoApp.Web.Controllers
{
    // Countries for offices
    public class CountryController : Controller
    {
        private readonly ICountryService countryService;
        private readonly ICityService cityService;

        public CountryController(ICountryService countryService, ICityService cityService)
        {
            this.countryService = countryService;
            this.cityService = cityService;
        }

        // Load view with all countries
        public async Task<IActionResult> All()
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return Redirect("/Identity/Account/Login");
            }
            var countries = await this.countryService.GetAllCountries();
            var mapper = new CountryOutputMapper();
            var countriesAll = mapper.Map(countries);

            return this.View(countriesAll);
        }


        // Load view for creation of new country
        [HttpGet]
        public IActionResult Create()
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return Redirect("/Identity/Account/Login");
            }
            return this.View();
        }


        // Input information for creation of new country
        [HttpPost]
        public async Task<IActionResult> Create(CountryCreateInputModel model)
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return Redirect("/Identity/Account/Login");
            }
            if (this.ModelState.IsValid && model.Name.Length >= GlobalConstants.CountryNameMinLength)
            {
                var ifCountryExists = this.countryService.IfExists(model.Name);
                if (ifCountryExists)
                {
                    return Redirect("/Country/Exist");
                }
                else
                {
                    await this.countryService.Create(model.Name);
                    return Redirect("/Country/All");
                }
            }

            return Redirect("/Country/All");
        }


        // Load view for update 
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return Redirect("/Identity/Account/Login");
            }
            var country = await this.countryService.GetCountryById(id);
            var mapper = new ContryEditInputMapper();
            var currentModel = mapper.Map(country);
            return this.View(currentModel);
        }


        // Input information for update of country
        [HttpPost]
        public async Task<ActionResult> Edit(CountryEditInputModel model)
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return Redirect("/Identity/Account/Login");
            }
            if (!ModelState.IsValid && model.Name.Length < GlobalConstants.CountryNameMinLength)
            {
                return Redirect("/Country/All");
            }

            var isExist = this.countryService.IfExists(model.Name);

            if (isExist)
            {
                return Redirect("/Country/Exist");
            }

            var mapper = new CountryDtoModelMapper();
            var countryDtoModel = mapper.Map(model);
            await this.countryService.EditCountry(countryDtoModel);
            return Redirect("/Country/All");
        }


        // Delete country
        public async Task<IActionResult> Delete(int id)
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return Redirect("/Identity/Account/Login");
            }
            var count = await this.cityService.CountAsync(id);
            if (count > 0)
            {
                return Redirect("/Country/ExistsCities");
            }
            var country = await this.countryService.GetCountryById(id);
            if (country == null)
            {
                return Redirect("/Country/All");
            }

            await this.countryService.DeleteCountry(id);
            return Redirect("/Country/All");
        }

        // Load view with message - if country exists 
        public IActionResult Exist() 
        {
            return this.View();
        }

        // Load view with message - if there is any city for current country 
        public IActionResult ExistsCities()
        {
            return this.View();
        }
    }
}
