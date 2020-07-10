using InfoApp.Common.DtoModels.OfficeDtos;
using InfoApp.Data.Models;
using InfoApp.Data.Repositories;
using InfoApp.Services.Data.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfoApp.Services.Data
{
    // business logic layer for company
    public class OfficeService : IOfficeService
    {
        private readonly IRepository<Office> repository;
        private readonly IRepository<Country> countryRepo;
        private readonly IRepository<City> cityRepo;
        private readonly IRepository<Company> companyRepo;

        public OfficeService(IRepository<Office> repository,
                             IRepository<Country> countryRepo,
                             IRepository<City> cityRepo,
                             IRepository<Company> companyRepo)
        {
            this.repository = repository;
            this.countryRepo = countryRepo;
            this.cityRepo = cityRepo;
            this.companyRepo = companyRepo;
        }

        // Returns information for all offices
        public async Task<List<OfficeDtoModel>> GetAllOffices(int id)
        {
            var offices = await this.repository.GetAllAsync();
            var allOffices = new List<OfficeDtoModel>();

            foreach (var item in offices.Where(x => x.CompanyId == id))
            {
                var country = await countryRepo.GetByIdAsync(item.CountryId);
                var city = await cityRepo.GetByIdAsync(item.CityId);
                var company = await companyRepo.GetByIdAsync(item.CompanyId);

                var model = new OfficeDtoModel
                {
                    OfficeId = item.OfficeId,
                    OfficeName = item.OfficeName,
                    CompanyName = item.Company.CompanyName,
                    CountryName = country.CountryName,
                    CityName = city.Name,
                    Street = item.Street,
                    StreetNumber = item.StreetNumber,
                    Headquarters = item.Headquarters == true ? "yes" : "no"
                };

                allOffices.Add(model);
            }

            return allOffices;
        }

        // Check if such an office exist in database
        public async Task<bool> IfExists(OfficeCreateInputDtoModel model)
        {
            var city = await this.cityRepo.AllAsNoTracking().FirstOrDefaultAsync(x => x.Name == model.CityName);

            var office = await this.repository.AllAsNoTracking()
                                               .Where(x => x.OfficeName == model.OfficeName &&
                                                                            x.Street == model.StreetName &&
                                                                            x.StreetNumber == model.StreetNumber &&
                                                                            x.CityId == city.CityId)
                                               .FirstOrDefaultAsync();



            if (office != null)
            {
                return true;
            }

            return false;
        }

        // Create new office and add it in database
        public async Task Create(OfficeCreateInputDtoModel model)
        {
            var company = await this.companyRepo.GetByIdAsync(model.CompanyId);
            var citiesAll = await this.cityRepo.GetAllAsync();
            var city = citiesAll.FirstOrDefault(x => x.Name == model.CityName);
            var countriesAll = await this.countryRepo.GetAllAsync();
            var country = countriesAll.FirstOrDefault(x => x.CountryId == city.CountryId);

            var office = new Office
            {
                OfficeName = model.OfficeName,
                Company = company,
                Country = country,
                City = city,
                Street = model.StreetName,
                StreetNumber = model.StreetNumber,
                Headquarters = model.Headquaters == "yes" ? true : false
            };
            await this.repository.InsertAsync(office);
            await this.repository.SaveAsync();
        }

        // Get an office from database by id 
        public async Task<OfficeEditOutputDto> GetOfficeById(int id)
        {
            var model = await this.repository.GetByIdAsync(id);
            
            if (model == null)
            {
                return null;
            }

            var company = await this.companyRepo.GetByIdAsync(model.CompanyId);
            var city = await this.cityRepo.GetByIdAsync(model.CityId);

            var currentModel = new OfficeEditOutputDto
            {
                OfficeId = model.OfficeId,
                OfficeName = model.OfficeName,
                CityName = city.Name,
                StreetName = model.Street,
                StreetNumber = model.StreetNumber,
                Headquaters = model.Headquarters == true ? "yes" : "no",
                CompanyId = model.CompanyId
            };

            return currentModel;
        }

        // Update data for office in database
        public async Task EditOffice(OfficeEditInputDto model)
        {
            var company = await this.companyRepo.GetByIdAsync(model.CompanyId);
            var city = this.cityRepo.GetAllAsync().GetAwaiter().GetResult().FirstOrDefault(x => x.Name == model.CityName);
            var country = await this.countryRepo.GetByIdAsync(city.CountryId);
            
            var currentModel = new Office
            {
                OfficeId = model.OfficeId,
                OfficeName = model.OfficeName,
                Company = company,
                Country = country,
                City = city,
                Street = model.StreetName,
                StreetNumber = model.StreetNumber,
                Headquarters = model.Headquaters == "yes" ? true : false
            };

            this.repository.Update(currentModel);
            await this.repository.SaveAsync();
        }

        // Delete office
        public async Task<int> DeleteOffice(int id)
        {
            var companyId = this.repository.GetByIdAsync(id).GetAwaiter().GetResult().CompanyId;

            var office = await this.repository.AllAsNoTracking().FirstOrDefaultAsync(x => x.OfficeId == id);

            if (office != null)
            {
                await this.repository.DeleteAsync(id);
                await this.repository.SaveAsync();
            }
            
            return companyId;
        }

        // Returns count of offices which are in a city, using city id
        public async Task<int> CountAsync(int id)
        {
            var officesAll = await this.repository.GetAllAsync();
            var count = officesAll.Where(x => x.CityId == id).ToList().Count();

            return count;
        }

        // Returns count of all offices in a company, using company id 
        public async Task<int> CountAsyncByCompanyId(int id)
        {
            var officesAll = await this.repository.GetAllAsync();
            var count = officesAll.Where(x => x.CompanyId == id).ToList().Count();

            return count;
        }

        // Check is there in database same office
        public async Task<bool> IsSame(OfficeEditInputDto model)
        {
            var city = await this.cityRepo.AllAsNoTracking().FirstOrDefaultAsync(x => x.Name == model.CityName);

            var office = await  this.repository.AllAsNoTracking()
                                               .Where(x => x.OfficeName == model.OfficeName &&
                                                                            x.Street == model.StreetName &&
                                                                            x.StreetNumber == model.StreetNumber &&
                                                                            x.CityId == city.CityId)
                                               .FirstOrDefaultAsync();
            
            

            if (office != null)
            {
                return true;
            }

            return false;
        }

        // Returns company id using office id
        public int GetCompanyIdByOfficeId(int id)
        {
            var companyId = this.repository.AllAsNoTracking().FirstOrDefault(x => x.OfficeId == id).CompanyId;

            return companyId;
        }
    }
}
