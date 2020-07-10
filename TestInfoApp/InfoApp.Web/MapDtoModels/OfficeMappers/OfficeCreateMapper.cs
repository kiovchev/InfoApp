using InfoApp.Common.DtoModels.OfficeDtos;
using InfoApp.Web.Models.InputOutputModels.InputModels.OfficeInputModels;

namespace InfoApp.Web.MapDtoModels.OfficeMappers
{
    public class OfficeCreateMapper
    {
        public OfficeCreateInputDtoModel Map(OfficeCreatePostInputModel model)
        {
            var dtoModel = new OfficeCreateInputDtoModel
            {
                OfficeName = model.OfficeName,
                CompanyId = model.CompanyId,
                CompanyName = model.CompanyName,
                CityName = model.CityName,
                StreetName = model.StreetName,
                StreetNumber = model.StreetNumber,
                Headquaters = model.Headquaters
            };

            return dtoModel;
        }
    }
}
