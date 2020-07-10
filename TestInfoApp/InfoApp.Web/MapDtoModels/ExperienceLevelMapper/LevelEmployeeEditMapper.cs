using InfoApp.Common.DtoModels.ExperienceLevelDtos;
using InfoApp.Web.Models.InputOutputModels.OutputModels.ExperienceLevelOutputModels;
using System.Collections.Generic;

namespace InfoApp.Web.MapDtoModels.ExperienceLevelMapper
{
    public class LevelEmployeeEditMapper
    {
        public List<LevelOutputModel> Map(List<ExperienceDtoModel> levelDtos)
        {
            var levels = new List<LevelOutputModel>();

            foreach (var item in levelDtos)
            {
                var level = new LevelOutputModel
                {
                    LevelId = item.LevelId,
                    LevelName = item.LevelName
                };

                levels.Add(level);
            }

            return levels;
        }
    }
}
