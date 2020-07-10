using InfoApp.Common.DtoModels.CityDto;
using InfoApp.Data.Models;
using InfoApp.Data.Repositories;
using InfoApp.Services.Data.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfoApp.Services.Data
{
    // business logic layer for city
    public class CityService : ICityService
    {
        private readonly IRepository<City> repository;
        private readonly IRepository<Country> countryRepository;

        public CityService(IRepository<City> repository, IRepository<Country> countryRepository)
        {
            this.repository = repository;
            this.countryRepository = countryRepository;
        }

        // Get all cities from database
        public async Task<List<CityDtoModel>> GetAllCities()
        {
            var cities = await this.repository.GetAllAsync();
            var allCcities = new List<CityDtoModel>();

            foreach (var item in cities)
            {
                var model = new CityDtoModel
                {
                    CityId = item.CityId,
                    CityName = item.Name
                };

                allCcities.Add(model);
            }

            return allCcities;
        }

        // Get all cities from database by country id
        public async Task<List<CityDtoModel>> GetAllCitiesByCountry(int id)
        {
            var cities = await this.repository.GetAllAsync();
            cities = cities.Where(x => x.CountryId == id).ToList();
            var allCities= new List<CityDtoModel>();

            foreach (var item in cities)
            {
                var model = new CityDtoModel
                {
                    CityId = item.CityId,
                    CityName = item.Name
                };

                allCities.Add(model);
            }

            return allCities;
        }

        // Check if current city exists in database
        public async Task<bool> IfExists(string name)
        {
            var cityAll = await this.repository.GetAllAsync();
            var city = cityAll.FirstOrDefault(x => x.Name == name);

            if (city != null)
            {
                return true;
            }

            return false;
        }

        // Check is there same city in database   
        public async Task<bool> IsSame(string cityName, string countryName)
        {
            var country = await this.countryRepository.AllAsNoTracking().FirstOrDefaultAsync(x => x.CountryName == countryName);
            var cityAll = this.repository.AllAsNoTracking();
            var city = await cityAll.FirstOrDefaultAsync(x => x.Name == cityName && x.CountryId == country.CountryId);

            if (city != null)
            {
                return true;
            }

            return false;
        }

        // Create new city and add it to database
        public async Task Create(string cityName, string countryName)
        {
            var countryall = await this.countryRepository.GetAllAsync();
            var country = countryall.ToList().FirstOrDefault(x => x.CountryName == countryName);

            var city = new City
            {
                Name = cityName,
                Country = country
            };
            await this.repository.InsertAsync(city);
            await this.repository.SaveAsync();
        }

        // Get current city by city id from database
        public async Task<CityEditInputDto> GetCityById(int id)
        {
            var model = await this.repository.GetByIdAsync(id);
            var countryName = this.countryRepository.GetByIdAsync(model.CountryId).GetAwaiter().GetResult().CountryName;

            if (model == null)
            {
                return null;
            }

            var currentModel = new CityEditInputDto
            {
                Id = model.CityId,
                Name = model.Name,
                CountryName = countryName

            };

            return currentModel;
        }

        // Update data for city in database
        public async Task EditCity(CityDtoModel model)
        {
            var country = this.countryRepository.GetAllAsync().GetAwaiter().GetResult().FirstOrDefault(x => x.CountryName == model.CountryName);

            var currentModel = new City
            {
                CityId = model.CityId,
                Name = model.CityName,
                Country = country
            };

            this.repository.Update(currentModel);
            await this.repository.SaveAsync();
        }

        // Delete city from database
        public async Task DeleteCity(int id)
        {
            var city = await this.repository.AllAsNoTracking().FirstOrDefaultAsync(x => x.CityId == id);

            if (city != null)
            {
                await this.repository.DeleteAsync(id);
                await this.repository.SaveAsync();
            }
        }

        // Get count of all cities in any country  from database by country id  
        public async Task<int> CountAsync(int id)
        {
            var cityAll = await this.repository.GetAllAsync();
            var count = cityAll.Where(x => x.CountryId == id).ToList().Count();

            return count;
        }
    }
}
