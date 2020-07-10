using InfoApp.Common.DtoModels.ExperienceLevelDtos;
using InfoApp.Web.Models.InputOutputModels.OutputModels.ExperienceLevelOutputModels;
using System.Collections.Generic;

namespace InfoApp.Web.MapDtoModels.ExperienceLevelMapper
{
    public class LevelOutputMapper
    {
        public List<LevelOutputModel> Map(List<ExperienceDtoModel> levels)
        {
            var levelsAll = new List<LevelOutputModel>();
            foreach (var item in levels)
            {
                var currentLevel = new LevelOutputModel
                {
                    LevelId = item.LevelId,
                    LevelName = item.LevelName
                };

                levelsAll.Add(currentLevel);
            }

            return levelsAll;
        }
    }
}
