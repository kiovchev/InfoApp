using InfoApp.Common.DtoModels.CityDto;
using InfoApp.Web.Models.InputOutputModels.InputModels.CityInputModels;

namespace InfoApp.Web.MapDtoModels.CityMappers
{
    public class CityDtoModelMapper
    {
        public CityDtoModel Map(CityEditInputModel model)
        {
            var currentModel = new CityDtoModel
            {
                CityId = model.Id,
                CityName = model.Name,
                CountryName = model.CountryName
            };

            return currentModel;
        }
    }
}
