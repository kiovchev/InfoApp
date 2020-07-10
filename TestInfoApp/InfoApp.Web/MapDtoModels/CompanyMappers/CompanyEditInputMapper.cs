using InfoApp.Common.DtoModels.CompanyDtos;
using InfoApp.Web.Models.InputOutputModels.InputModels.CompanyInputModels;

namespace InfoApp.Web.MapDtoModels.CompanyMappers
{
    public class CompanyEditInputMapper
    {
        public CompanyEditInputModel Map(CompanyEditInputDto companyEditInputDto)
        {
            var model = new CompanyEditInputModel
            {
                Id = companyEditInputDto.CompanyId,
                CompanyName = companyEditInputDto.CompanyName,
                Creationdate = companyEditInputDto.Creationdate
            };

            return model;
        }
    }
}
