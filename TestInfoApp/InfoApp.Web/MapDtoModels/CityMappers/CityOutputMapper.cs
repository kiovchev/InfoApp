using InfoApp.Common.DtoModels.CityDto;
using InfoApp.Web.Models.InputOutputModels.OutputModels.CityOutputModels;
using System.Collections.Generic;

namespace InfoApp.Web.MapDtoModels.CityMappers
{
    public class CityOutputMapper
    {
        public List<CityOutputModel> Map(List<CityDtoModel> cities)
        {
            var citiesAll = new List<CityOutputModel>();
            foreach (var item in cities)
            {
                var currentCity = new CityOutputModel
                {
                    CityId = item.CityId,
                    CityName = item.CityName
                };

                citiesAll.Add(currentCity);
            }

            return citiesAll;
        }
    }
}
