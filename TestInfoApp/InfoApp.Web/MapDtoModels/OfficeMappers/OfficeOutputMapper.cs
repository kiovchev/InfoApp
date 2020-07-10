using InfoApp.Common.DtoModels.OfficeDtos;
using InfoApp.Web.Models.InputOutputModels.OutputModels.OfficeOtputModels;
using System.Collections.Generic;

namespace InfoApp.Web.MapDtoModels.OfficeMappers
{
    public class OfficeOutputMapper
    {
        public List<OfficeOutputModel> Map(List<OfficeDtoModel> offices)
        {
            var officesAll = new List<OfficeOutputModel>();
            foreach (var item in offices)
            {
                var currentCountry = new OfficeOutputModel
                {
                    OfficeId = item.OfficeId,
                    OfficeName = item.OfficeName,
                    CompanyName = item.CompanyName,
                    CountryName = item.CountryName,
                    CityName = item.CityName,
                    Street = item.Street,
                    StreetNumber = item.StreetNumber,
                    Headquarters = item.Headquarters
                };

                officesAll.Add(currentCountry);
            }

            return officesAll;
        }
    }
}
