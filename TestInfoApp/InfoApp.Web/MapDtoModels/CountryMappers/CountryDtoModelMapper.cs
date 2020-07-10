using InfoApp.Common.DtoModels.CountryDtos;
using InfoApp.Web.Models.InputOutputModels.InputModels.CountryInputModels;
using System;

namespace InfoApp.Web.MapDtoModels.CountryMappers
{
    public class CountryDtoModelMapper
    {
        public CountryDtoModel Map(CountryEditInputModel model)
        {
            var currentModel = new CountryDtoModel
            {
                CountryId = model.Id,
                CountryName = model.Name
            };

            return currentModel;
        }
    }
}
