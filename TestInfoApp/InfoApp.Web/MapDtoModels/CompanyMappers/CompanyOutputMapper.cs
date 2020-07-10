using InfoApp.Common.DtoModels.CompanyDtos;
using InfoApp.Web.Models.InputOutputModels.OutputModels.CompanyOutputModels;
using System.Collections.Generic;

namespace InfoApp.Web.MapDtoModels.CompanyMappers
{
    public class CompanyOutputMapper
    {
        public List<CompanyOutputModel> Map(List<CompanyDtoModel> countries)
        {
            var companiesAll = new List<CompanyOutputModel>();
            foreach (var item in countries)
            {
                var currentCompany = new CompanyOutputModel
                {
                    CompanyId = item.CompanyId,
                    CompanyName = item.CompanyName,
                    CompanyCreation = item.CompanyCreationDate.ToString("dd/MM/yyyy")
                };

                companiesAll.Add(currentCompany);
            }

            return companiesAll;
        }
    }
}
