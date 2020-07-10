using InfoApp.Common.DtoModels.CountryDtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InfoApp.Services.Data.Contracts
{
    public interface ICountryService
    {
        Task<List<CountryDtoModel>> GetAllCountries();

        bool IfExists(string name);

        Task Create(string name);

        Task<CountryEditInputDto> GetCountryById(int id);

        Task EditCountry(CountryDtoModel model);

        Task DeleteCountry(int id);
    }
}
