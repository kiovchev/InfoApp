using InfoApp.Common.DtoModels.CompanyDtos;
using InfoApp.Data.Models;
using InfoApp.Data.Repositories;
using InfoApp.Services.Data.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfoApp.Services.Data
{
    // business logic layer for company
    public class CompanyService : ICompanyService
    {
        private readonly IRepository<Company> repository;

        public CompanyService(IRepository<Company> repository)
        {
            this.repository = repository;
        }

        // Get all companies from database
        public async Task<List<CompanyDtoModel>> GetAllCompanies()
        {
            var companies = await this.repository.GetAllAsync();
            var allCompanies = new List<CompanyDtoModel>();

            foreach (var item in companies)
            {
                var model = new CompanyDtoModel
                {
                    CompanyId = item.CompanyId,
                    CompanyName = item.CompanyName,
                    CompanyCreationDate = item.Creationdate
                };

                allCompanies.Add(model);
            }

            return allCompanies;
        }

        // Check if current company exists in database
        public bool IfExists(string name)
        {
            var companies = this.repository.AllAsNoTracking();
            var company = companies.FirstOrDefault(x => x.CompanyName == name);

            if (company != null)
            {
                return true;
            }

            return false;
        }

        // Create new company and add it in database
        public async Task Create(string companyName, DateTime createdAt)
        {
            var company = new Company
            {
                CompanyName = companyName,
                Creationdate = createdAt
            };
            await this.repository.InsertAsync(company);
            await this.repository.SaveAsync();
        }

        // Get company from database by id
        public async Task<CompanyEditInputDto> GetCompanyById(int id)
        {
            var model = await this.repository.GetByIdAsync(id);

            if (model == null)
            {
                return null;
            }

            var currentModel = new CompanyEditInputDto
            {
                CompanyId = model.CompanyId,
                CompanyName = model.CompanyName,
                Creationdate = model.Creationdate
            };

            return currentModel;
        }

        // Update data for a company in database 
        public async Task EditCompany(CompanyDtoModel model)
        {
            var currentModel = new Company
            {
                CompanyId = model.CompanyId,
                CompanyName = model.CompanyName,
                Creationdate = model.CompanyCreationDate
            };

            this.repository.Update(currentModel);
            await this.repository.SaveAsync();
        }

        // delete company
        public async Task DeleteCompany(int id)
        {
            var company = await this.repository.AllAsNoTracking().FirstOrDefaultAsync(x => x.CompanyId == id);

            if (company != null)
            {
                await this.repository.DeleteAsync(id);
                await this.repository.SaveAsync();
            }
        }

        // Check is there same city in database
        public bool IsSame(string companyName, DateTime createdAt)
        {
            var company = this.repository.AllAsNoTracking().FirstOrDefault(x => x.CompanyName == companyName);

            if (company != null)
            {
                if (company.Creationdate == createdAt)
                {
                    return true;
                }

                return false;
            }

            return false;
        }
    }
}
