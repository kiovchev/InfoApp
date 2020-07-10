using InfoApp.Common.DtoModels.OfficeDtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InfoApp.Services.Data.Contracts
{
    public interface IOfficeService
    {
        Task<List<OfficeDtoModel>> GetAllOffices(int id);

        Task<bool> IfExists(OfficeCreateInputDtoModel model);

        Task Create(OfficeCreateInputDtoModel model);

        Task<OfficeEditOutputDto> GetOfficeById(int id);

        Task EditOffice(OfficeEditInputDto model);

        Task<int> DeleteOffice(int id);

        Task<int> CountAsync(int id);

        Task<int> CountAsyncByCompanyId(int id);

        Task<bool> IsSame(OfficeEditInputDto model);

        int GetCompanyIdByOfficeId(int id);
    }
}
