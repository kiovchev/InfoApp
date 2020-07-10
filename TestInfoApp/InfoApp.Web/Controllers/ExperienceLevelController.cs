using InfoApp.Common;
using InfoApp.Services.Data.Contracts;
using InfoApp.Web.MapDtoModels.ExperienceLevelMapper;
using InfoApp.Web.Models.InputOutputModels.InputModels.ExperienceLevelInputModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace InfoApp.Web.Controllers
{
    // Employee Expirience Levels
    public class ExperienceLevelController : Controller
    {
        private readonly IExperienceLevelService experienceLevelService;

        public ExperienceLevelController(IExperienceLevelService experienceLevelService)
        {
            this.experienceLevelService = experienceLevelService;
        }

        // Load view with information for all expirience levels
        public async Task<IActionResult> All()
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return Redirect("/Identity/Account/Login");
            }

            var levels = await this.experienceLevelService.GetAllLevels();

            var mapper = new LevelOutputMapper();
            var levelsAll = mapper.Map(levels);

            return this.View(levelsAll);
        }


        // Load view for creation of expirience level
        [HttpGet]
        public IActionResult Create()
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return Redirect("/Identity/Account/Login");
            }
            return this.View();
        }

        // Input information for creation of expirience level
        [HttpPost]
        public async Task<IActionResult> Create(LevelCreateInputModel model)
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return Redirect("/Identity/Account/Login");
            }
            if (this.ModelState.IsValid && model.Name.Length >= GlobalConstants.ExpirienceLevelNameMinLength) 
            {
                var ifExpirienceLevelExists = this.experienceLevelService.IfExists(model.Name);

                if (ifExpirienceLevelExists)
                {
                    return Redirect("/ExperienceLevel/Exist");
                }

                await this.experienceLevelService.Create(model.Name);
                return Redirect("/ExperienceLevel/All");
            }

            return this.Redirect("/ExperienceLevel/All");
        }

        // Load view for update 
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return Redirect("/Identity/Account/Login");
            }

            var level = await this.experienceLevelService.GetLevelById(id);
            var mapper = new LevelEditInputMapper();
            var currentModel = mapper.Map(level);
            return this.View(currentModel);
        }

        // Input information for update of expirience level
        [HttpPost]
        public async Task<ActionResult> Edit(LevelEditInputModel model)
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return Redirect("/Identity/Account/Login");
            }
            if (!ModelState.IsValid && model.Name.Length < GlobalConstants.ExpirienceLevelNameMinLength)
            {
                return Redirect("/ExperienceLevel/All");
            }

            var ifExpirienceLevelExists = this.experienceLevelService.IfExists(model.Name);

            if (ifExpirienceLevelExists)
            {
                return Redirect("/ExperienceLevel/Exist");
            }

            var mapper = new LevelDtoModelMapper();
            var levelDtoModel = mapper.Map(model);
            await this.experienceLevelService.EditLevel(levelDtoModel);
            return Redirect("/ExperienceLevel/All");
        }

        // Delete expirience level
        public async Task<IActionResult> Delete(int id)
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return Redirect("/Identity/Account/Login");
            }

            await this.experienceLevelService.DeleteLevel(id);
            return Redirect("/ExperienceLevel/All");
        }

        // Load view with message, if Expirience level exists in database
        public IActionResult Exist()
        {
            return this.View();
        }
    }
}
