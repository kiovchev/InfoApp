using InfoApp.Common.DtoModels.ExperienceLevelDtos;
using InfoApp.Web.Models.InputOutputModels.InputModels.ExperienceLevelInputModels;

namespace InfoApp.Web.MapDtoModels.ExperienceLevelMapper
{
    public class LevelEditInputMapper
    {
        public LevelEditInputModel Map(LevelEditInputDto levelEditInputDto)
        {
            var model = new LevelEditInputModel
            {
                Id = levelEditInputDto.Id,
                Name = levelEditInputDto.Name
            };

            return model;
        }
    }
}
