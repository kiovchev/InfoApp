using InfoApp.Common.DtoModels.OfficeDtos;
using InfoApp.Web.Models.InputOutputModels.InputModels.OfficeInputModels;

namespace InfoApp.Web.MapDtoModels.OfficeMappers
{
    public class OfficeEditInputMapper
    {
        public OfficeEditInputDto Map(OfficeEditInputModel editModel)
        {
            var model = new OfficeEditInputDto
            {
                OfficeId = editModel.OfficeId,
                OfficeName = editModel.OfficeName,
                CityName = editModel.CityName,
                StreetName = editModel.StreetName,
                StreetNumber = editModel.StreetNumber,
                Headquaters = editModel.Headquaters,
                CompanyId = editModel.CompanyId
            };

            return model;
        }
    }
}
