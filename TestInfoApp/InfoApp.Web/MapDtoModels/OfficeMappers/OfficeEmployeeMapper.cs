using InfoApp.Common.DtoModels.OfficeDtos;
using InfoApp.Web.Models.InputOutputModels.OutputModels.OfficeOtputModels;
using System.Collections.Generic;

namespace InfoApp.Web.MapDtoModels.OfficeMappers
{
    public class OfficeEmployeeMapper
    {
        public List<OfficeNamesByCompanyModel> Map(List<OfficeDtoModel> models)
        {
            var offices = new List<OfficeNamesByCompanyModel>();

            foreach (var item in models)
            {
                var office = new OfficeNamesByCompanyModel
                {
                    OfficeName = item.OfficeName
                };

                offices.Add(office);
            }

            return offices;
        }
    }
}
