using InfoApp.Common.DtoModels.CountryDtos;
using InfoApp.Web.Models.InputOutputModels.InputModels.CountryInputModels;

namespace InfoApp.Web.MapDtoModels.CountryMappers
{
    public class ContryEditInputMapper
    {
        public CountryEditInputModel Map(CountryEditInputDto countryEditInputDto)
        {
            var model = new CountryEditInputModel 
            {
                Id = countryEditInputDto.Id,
                Name = countryEditInputDto.Name
            };

            return model;
        }
    }
}
