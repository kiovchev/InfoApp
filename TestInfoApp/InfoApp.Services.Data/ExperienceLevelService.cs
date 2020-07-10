using InfoApp.Common.DtoModels.ExperienceLevelDtos;
using InfoApp.Data.Models;
using InfoApp.Data.Repositories;
using InfoApp.Services.Data.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfoApp.Services.Data
{
    // business logic layer for expirience level
    public class ExperienceLevelService : IExperienceLevelService
    {
        private readonly IRepository<ExperienceLevel> repository;

        public ExperienceLevelService(IRepository<ExperienceLevel> repository)
        {
            this.repository = repository;
        }

        // returns all levels from database
        public async Task<List<ExperienceDtoModel>> GetAllLevels()
        {
            var levels = await this.repository.GetAllAsync();
            var alllevels = new List<ExperienceDtoModel>();

            foreach (var item in levels)
            {
                var model = new ExperienceDtoModel
                {
                    LevelId = item.ExperienceLevelId,
                    LevelName = item.ExperienceLevelName
                };

                alllevels.Add(model);
            }

            return alllevels;
        }

        // check if current level exists in database
        public bool IfExists(string name)
        {
            var levelAll = this.repository.AllAsNoTracking();
            var level = levelAll.FirstOrDefault(x => x.ExperienceLevelName == name);

            if (level != null)
            {
                return true;
            }

            return false;
        }

        // create new level and add it to database
        public async Task Create(string name)
        {
            var level = new ExperienceLevel
            {
                ExperienceLevelName = name
            };

            await this.repository.InsertAsync(level);
            await this.repository.SaveAsync();
        }

        // returns level - get current level from database by id
        public async Task<LevelEditInputDto> GetLevelById(int id)
        {
            var model = await this.repository.GetByIdAsync(id);

            if (model == null)
            {
                return null;
            }

            var currentModel = new LevelEditInputDto
            {
                Id = model.ExperienceLevelId,
                Name = model.ExperienceLevelName
            };

            return currentModel;
        }

        // update information for current level in database
        public async Task EditLevel(ExperienceDtoModel model)
        {
            var currentModel = new ExperienceLevel
            {
                ExperienceLevelId = model.LevelId,
                ExperienceLevelName = model.LevelName
            };

            this.repository.Update(currentModel);
            await this.repository.SaveAsync();
        }

        // delete level from database
        public async Task DeleteLevel(int id)
        {
            var currentLevel = await this.repository.GetByIdAsync(id);

            if (currentLevel != null)
            {
                await this.repository.DeleteAsync(id);
                await this.repository.SaveAsync();
            }
        }
    }
}
