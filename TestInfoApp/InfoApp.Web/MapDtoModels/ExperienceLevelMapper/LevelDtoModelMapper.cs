using InfoApp.Common.DtoModels.ExperienceLevelDtos;
using InfoApp.Web.Models.InputOutputModels.InputModels.ExperienceLevelInputModels;

namespace InfoApp.Web.MapDtoModels.ExperienceLevelMapper
{
    public class LevelDtoModelMapper
    {
        public ExperienceDtoModel Map(LevelEditInputModel model)
        {
            var currentModel = new ExperienceDtoModel
            {
                LevelId = model.Id,
                LevelName = model.Name
            };

            return currentModel;
        }
    }
}
