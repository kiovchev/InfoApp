using InfoApp.Common;
using InfoApp.Services.Data.Contracts;
using InfoApp.Web.MapDtoModels.CityMappers;
using InfoApp.Web.MapDtoModels.CountryMappers;
using InfoApp.Web.Models.InputOutputModels.InputModels.CityInputModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace InfoApp.Web.Controllers
{
    // Cities for offices
    public class CityController : Controller
    {
        private readonly ICityService cityService;
        private readonly ICountryService countryService;
        private readonly IOfficeService officeService;

        public CityController(ICityService cityService,
                              ICountryService countryService,
                              IOfficeService officeService)
        {
            this.cityService = cityService;
            this.countryService = countryService;
            this.officeService = officeService;
        }

        // Load view with all cities
        public async Task<IActionResult> All()
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return Redirect("/Identity/Account/Login");
            }
            var cities = await this.cityService.GetAllCities();
            var mapper = new CityOutputMapper();
            var citiesAll = mapper.Map(cities);

            return this.View(citiesAll);
        }

        // Load view with all cities by current country
        public async Task<IActionResult> AllByCountry(int id)
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return Redirect("/Identity/Account/Login");
            }
            var cities = await this.cityService.GetAllCitiesByCountry(id);
            var mapper = new CityOutputMapper();
            var citiesAll = mapper.Map(cities);

            return this.View(citiesAll);
        }

        // Load view for creation of new city
        [HttpGet]
        public async Task<IActionResult> Create()
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

        // Input information for creation of new city
        [HttpPost]
        public async Task<IActionResult> Create(CityCreateInputModel model)
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return Redirect("/Identity/Account/Login");
            }
            if (this.ModelState.IsValid && model.Name.Length >= GlobalConstants.CityNameMinLength)
            {
                var ifCityExists = await this.cityService.IfExists(model.Name);
                if (ifCityExists)
                {
                    return Redirect("/City/Exist");
                }

                await this.cityService.Create(model.Name, model.CountryName);
                return Redirect("/City/All");

            }
            return this.Redirect("/City/All");
        }

        // Load view for update 
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return Redirect("/Identity/Account/Login");
            }
            var city = await this.cityService.GetCityById(id);
            var mapper = new CityEditInputMapper();
            var currentModel = mapper.Map(city);

            var countries = await this.countryService.GetAllCountries();
            var countryMapper = new CountryOutputMapper();
            var countriesAll = countryMapper.Map(countries);

            CityALlCountriesEditModel model = new CityALlCountriesEditModel();
            model.City = currentModel;
            model.Countries = countriesAll;

            return this.View(model);
        }


        // Input information for update of city
        [HttpPost]
        public async Task<ActionResult> Edit(CityEditInputModel model)
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return Redirect("/Identity/Account/Login");
            }
            if (!ModelState.IsValid && model.Name.Length < GlobalConstants.CityNameMinLength)
            {
                return Redirect("/City/All");
            }

            var isSame = await this.cityService.IsSame(model.Name, model.CountryName);

            if (isSame)
            {
                return Redirect("/City/Exist");
            }

            var mapper = new CityDtoModelMapper();
            var cityDtoModel = mapper.Map(model);
            await this.cityService.EditCity(cityDtoModel);
            return Redirect("/City/All");
        }

        // Delete city
        public async Task<IActionResult> Delete(int id)
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return Redirect("/Identity/Account/Login");
            }
            var city = await this.cityService.GetCityById(id);

            if (city == null)
            {
                return Redirect("/City/All");
            }

            var officesCount = await this.officeService.CountAsync(id);

            if (officesCount != 0)
            {
                return Redirect("/City/ExistOffices");
            }

            await this.cityService.DeleteCity(id);
            return Redirect("/City/All");
        }

        // Load view with message - if city exists 
        public IActionResult Exist()
        {
            return this.View();
        }

        // Load view with message - if there is any office for current city 
        public IActionResult ExistOffices()
        {
            return this.View();
        }
    }
}
