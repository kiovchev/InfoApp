using InfoApp.Common.DtoModels.CompanyDtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InfoApp.Services.Data.Contracts
{
    public interface ICompanyService
    {
        Task<List<CompanyDtoModel>> GetAllCompanies();

        bool IfExists(string name);

        Task Create(string companyName, DateTime createdAt);

        Task<CompanyEditInputDto> GetCompanyById(int id);

        Task EditCompany(CompanyDtoModel model);

        Task DeleteCompany(int id);

        bool IsSame(string companyName, DateTime createdAt);
    }
}
