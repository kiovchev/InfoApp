using InfoApp.Common.DtoModels.CountryDtos;
using InfoApp.Web.Models.InputOutputModels.OutputModels.CountryOutputModels;
using System.Collections.Generic;

namespace InfoApp.Web.MapDtoModels.CountryMappers
{
    public class CountryOutputMapper
    {
        public List<CountryOutputModel> Map(List<CountryDtoModel> countries)
        {
            var countriesAll = new List<CountryOutputModel>();
            foreach (var item in countries)
            {
                var currentCountry = new CountryOutputModel
                {
                    CountryId = item.CountryId,
                    CountryName = item.CountryName
                };

                countriesAll.Add(currentCountry);
            }

            return countriesAll;
        }
    }
}
