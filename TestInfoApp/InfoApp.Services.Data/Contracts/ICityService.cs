using InfoApp.Common.DtoModels.CityDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InfoApp.Services.Data.Contracts
{
    public interface ICityService
    {
        Task<List<CityDtoModel>> GetAllCities();

        Task<List<CityDtoModel>> GetAllCitiesByCountry(int id);

        Task<bool> IfExists(string name);

        Task Create(string cityName, string countryName);

        Task<CityEditInputDto> GetCityById(int id);

        Task EditCity(CityDtoModel model);

        Task DeleteCity(int id);

        Task<int> CountAsync(int id);

        Task<bool> IsSame(string cityName, string countryName);
    }
}
