using InfoApp.Common.DtoModels.CityDto;
using InfoApp.Web.Models.InputOutputModels.InputModels.CityInputModels;

namespace InfoApp.Web.MapDtoModels.CityMappers
{
    public class CityEditInputMapper
    {
        public CityEditInputModel Map(CityEditInputDto cityEditInputDto)
        {
            var model = new CityEditInputModel
            {
                Id = cityEditInputDto.Id,
                Name = cityEditInputDto.Name,
                CountryName = cityEditInputDto.CountryName
            };

            return model;
        }
    }
}
