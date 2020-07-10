using InfoApp.Common.DtoModels.OfficeDtos;
using InfoApp.Web.Models.InputOutputModels.OutputModels.OfficeOtputModels;

namespace InfoApp.Web.MapDtoModels.OfficeMappers
{
    public class OfficeEditOtputMapper
    {
        public OfficeEditCurrentOutputModel Map(OfficeEditOutputDto editModel)
        {
            var model = new OfficeEditCurrentOutputModel
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
