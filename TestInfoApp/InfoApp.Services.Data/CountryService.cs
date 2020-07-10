using InfoApp.Common.DtoModels.CountryDtos;
using InfoApp.Data.Models;
using InfoApp.Data.Repositories;
using InfoApp.Services.Data.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfoApp.Services.Data
{
    // business logic layer for Country
    public class CountryService : ICountryService
    {
        private readonly IRepository<Country> repository;

        public CountryService(IRepository<Country> repository)
        {
            this.repository = repository;
        }

        // Get all countries from database
        public async Task<List<CountryDtoModel>> GetAllCountries()
        {
            var countries = await this.repository.GetAllAsync();
            var allCountries = new List<CountryDtoModel>();

            foreach (var item in countries)
            {
                var model = new CountryDtoModel
                {
                    CountryId = item.CountryId,
                    CountryName = item.CountryName
                };

                allCountries.Add(model);
            }

            return allCountries;
        }

        // Check if current country exists in database 
        public bool IfExists(string name)
        {
            var countryAll = this.repository.AllAsNoTracking();
            var country = countryAll.FirstOrDefault(x => x.CountryName == name);

            if (country != null)
            {
                return true;
            }

            return false;
        }

        // Create a new country in database
        public async Task Create(string name)
        {
            var country = new Country
            {
                CountryName = name
            };
            await this.repository.InsertAsync(country);
            await this.repository.SaveAsync();
        }

        // Get information for country from database using id 
        public async Task<CountryEditInputDto> GetCountryById(int id)
        {
            var model = await this.repository.GetByIdAsync(id);

            if (model == null)
            {
                return null;
            }

            var currentModel = new CountryEditInputDto
            {
                Id = model.CountryId,
                Name = model.CountryName
            };

            return currentModel;
        }

        // Update information for current country
        public async Task EditCountry(CountryDtoModel model)
        {
            var currentModel = new Country
            {
                CountryId = model.CountryId,
                CountryName = model.CountryName
            };

            this.repository.Update(currentModel);
            await this.repository.SaveAsync();
        }

        // Delete country
        public async Task DeleteCountry(int id)
        {
            var currentCountry = await this.repository.GetByIdAsync(id);

            if (currentCountry != null)
            {
                await this.repository.DeleteAsync(id);
                await this.repository.SaveAsync();
            }
        }
    }
}
