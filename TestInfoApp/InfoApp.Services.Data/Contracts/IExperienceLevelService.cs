using InfoApp.Common.DtoModels.ExperienceLevelDtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InfoApp.Services.Data.Contracts
{
    public interface IExperienceLevelService
    {
        Task<List<ExperienceDtoModel>> GetAllLevels();

        bool IfExists(string name);

        Task Create(string name);

        Task<LevelEditInputDto> GetLevelById(int id);

        Task EditLevel(ExperienceDtoModel model);

        Task DeleteLevel(int id);
    }
}
