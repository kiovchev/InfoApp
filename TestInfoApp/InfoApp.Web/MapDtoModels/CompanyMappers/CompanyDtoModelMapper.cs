using InfoApp.Common.DtoModels.CompanyDtos;
using InfoApp.Web.Models.InputOutputModels.InputModels.CompanyInputModels;

namespace InfoApp.Web.MapDtoModels.CompanyMappers
{
    public class CompanyDtoModelMapper
    {
        public CompanyDtoModel Map(CompanyEditInputModel model)
        {
            var currentModel = new CompanyDtoModel
            {
                CompanyId = model.Id,
                CompanyName = model.CompanyName,
                CompanyCreationDate = model.Creationdate
            };

            return currentModel;
        }
    }
}
