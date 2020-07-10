using InfoApp.Common.DtoModels.EmployeeDtos;
using InfoApp.Data.Models;
using InfoApp.Data.Repositories;
using InfoApp.Services.Data.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfoApp.Services.Data
{
    // business logic layer for company
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository<Employee> repository;
        private readonly IRepository<ExperienceLevel> levelRepo;
        private readonly IRepository<Office> officeRepo;
        private readonly IRepository<Company> companyRepo;

        public EmployeeService(IRepository<Employee> repository, 
                               IRepository<ExperienceLevel> levelRepo,
                               IRepository<Office> officeRepo,
                              IRepository<Company> companyRepo)
        {
            this.repository = repository;
            this.levelRepo = levelRepo;
            this.officeRepo = officeRepo;
            this.companyRepo = companyRepo;
        }

        // Returns all employees for a company using company id 
        public async Task<EmployeeAllDtoModel> GetAllEmployeesByCompany(int id)
        {
            var levels = await this.levelRepo.GetAllAsync();
            var employees = this.repository.GetAllAsync().GetAwaiter()
                                                         .GetResult()
                                                         .Where(x => x.CompanyId == id)
                                                         .OrderBy(x => x.FirstName)
                                                         .ThenBy(x => x.LastName)
                                                         .ToList();
            var allemployees = new List<EmployeeDtoModel>();

            foreach (var item in employees)
            {
                var model = new EmployeeDtoModel
                {
                    EmploeeId = item.EmploeeId,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    StartingDate = item.StartingDate,
                    Salary = item.Salary,
                    VacationDays = item.VacationDays,
                    ExperienceLevel = levels.FirstOrDefault(x => x.ExperienceLevelId == item.ExperienceLevelId).ExperienceLevelName
                };

                allemployees.Add(model);
            }

            var currentModel = new EmployeeAllDtoModel();
            currentModel.CompanyId = id;
            currentModel.Employees = allemployees;

            return currentModel;
        }

        // Returns all employees for a office using office id
        public async Task<EmployeeAllByOfficeDtoModel> GetAllEmployeesByOffice(int id) // WE TAKE EMPLOYEE ID
        {
            var companyId = this.officeRepo.GetByIdAsync(id).GetAwaiter().GetResult().CompanyId;
            var levels = await this.levelRepo.GetAllAsync();
            var employees = this.repository.GetAllAsync().GetAwaiter().GetResult().Where(x => x.OfficeId == id).ToList();
            var allemployees = new List<EmployeeDtoModel>();

            foreach (var item in employees)
            {
                var model = new EmployeeDtoModel
                {
                    EmploeeId = item.EmploeeId,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    StartingDate = item.StartingDate,
                    Salary = item.Salary,
                    VacationDays = item.VacationDays,
                    ExperienceLevel = levels.FirstOrDefault(x => x.ExperienceLevelId == item.ExperienceLevelId).ExperienceLevelName
                };

                allemployees.Add(model);
            }

            var currentModel = new EmployeeAllByOfficeDtoModel();
            currentModel.CompanyId = companyId;
            currentModel.OfficeId = id;
            currentModel.Employees = allemployees;

            return currentModel;
        }

        // Create new office and add it in database
        public async Task Create(EmployeeCreateInputDtoModel model)
        {
            var office = await this.officeRepo.GetByIdAsync(model.OfficeId);
            var company = await this.companyRepo.GetByIdAsync(office.CompanyId);
            var expirienceLevel = this.levelRepo.GetAllAsync()
                                                .GetAwaiter()
                                                .GetResult()
                                                .FirstOrDefault(x => x.ExperienceLevelName == model.ExpirienceLevel);

            var employee = new Employee
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Office = office,
                Company = company,
                Level = expirienceLevel,
                Salary = model.Salary,
                StartingDate = model.StartingDate,
                VacationDays = model.VacationDays
            };
            await this.repository.InsertAsync(employee);
            await this.repository.SaveAsync();
        }

        //Delete employee and returns office id
        public async Task<int> DeleteEmployee(int id)
        {
            var offceId = this.repository.GetByIdAsync(id).GetAwaiter().GetResult().OfficeId;
            var employee = await this.repository.AllAsNoTracking().FirstOrDefaultAsync(x => x.EmploeeId == id);

            if (employee != null)
            {
                await this.repository.DeleteAsync(id);
                await this.repository.SaveAsync();
            }

            return offceId;
        }

        // Returns company id, using employee id
        public int GetCompanyId(int id)
        {
            var companyId = this.repository.GetByIdAsync(id).GetAwaiter().GetResult().CompanyId;

            return companyId;
        }

        // Returns employee by employee id
        public async Task<EmployeeEditOutputDtoModel> GetEmployeeById(int id)
        {
            var employee = await this.repository.GetByIdAsync(id);
            var officeName = this.officeRepo.GetByIdAsync(employee.OfficeId).GetAwaiter().GetResult().OfficeName;
            var levelName = this.levelRepo.GetByIdAsync(employee.ExperienceLevelId).GetAwaiter().GetResult().ExperienceLevelName;

            var employeeDto = new EmployeeEditOutputDtoModel
            {
                EmployeeId = employee.EmploeeId,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Salary = employee.Salary,
                StartingDate = employee.StartingDate,
                VacationDays = employee.VacationDays,
                CompanyId = employee.CompanyId,
                LevelName = levelName,
                OfficeName = officeName
            };

            return employeeDto;
        }

        // Update data for an employee and save changes in database
        public async Task<int> EditEmployee(EmployeeEditInputDtoModel model)
        {
            var employee = await this.repository.AllAsNoTracking().FirstOrDefaultAsync(x => x.EmploeeId == model.EmployeeId);
            var level = await this.levelRepo.AllAsNoTracking().FirstOrDefaultAsync(x => x.ExperienceLevelName == model.LevelName);
            var office = await this.officeRepo.AllAsNoTracking().Where(x => x.CompanyId == employee.CompanyId)
                                                                .FirstOrDefaultAsync(x => x.OfficeName == model.OfficeName);
            var company = await this.companyRepo.GetByIdAsync(office.CompanyId);

            var currentModel = new Employee
            {
                EmploeeId = model.EmployeeId,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Salary = model.Salary,
                StartingDate = model.StartingDate,
                VacationDays = model.VacationDays,
                Company = company,
                Level = level,
                Office = office
            };

            this.repository.Update(currentModel);
            await this.repository.SaveAsync();

            var officeId = office.OfficeId;

            return officeId;
        }


        // Returns count of all employees in a company, using company id
        public async Task<int> CountAsyncByCompanyId(int id)
        {
            var employeesAll = await this.repository.GetAllAsync();
            var count = employeesAll.Where(x => x.CompanyId == id).ToList().Count();

            return count;
        }

        // Check if an employee exists in database
        public async Task<bool> IfExists(EmployeeCreateInputDtoModel model)
        {
            var level = await this.levelRepo.AllAsNoTracking().FirstOrDefaultAsync(x => x.ExperienceLevelName == model.ExpirienceLevel);

            var employee = await this.repository.AllAsNoTracking()
                                               .Where(x => x.FirstName == model.FirstName &&
                                                                            x.LastName == model.LastName &&
                                                                            x.OfficeId == model.OfficeId &&
                                                                            x.VacationDays == model.VacationDays && 
                                                                            x.ExperienceLevelId == level.ExperienceLevelId &&
                                                                            x.Salary == model.Salary &&
                                                                            x.StartingDate == model.StartingDate)
                                               .FirstOrDefaultAsync();



            if (employee != null)
            {
                return true;
            }

            return false;
        }

        // Check if there is same employee in database
        public async Task<bool> isSame(EmployeeEditInputDtoModel model)
        {
            var employeeAll = await this.repository.AllAsNoTracking().ToListAsync();
            var employee = employeeAll.FirstOrDefault(x => x.EmploeeId == model.EmployeeId);
            var level = await this.levelRepo.AllAsNoTracking()
                                            .FirstOrDefaultAsync(x => x.ExperienceLevelName == model.LevelName);
            var office = await this.officeRepo.AllAsNoTracking()
                                              .Where(x => x.CompanyId == employee.CompanyId)
                                              .FirstOrDefaultAsync(x => x.OfficeName == model.OfficeName);
            
            var currentEmployee = employeeAll.Where(x => x.FirstName == model.FirstName &&
                                                                            x.LastName == model.LastName &&
                                                                            x.OfficeId == office.OfficeId &&
                                                                            x.VacationDays == model.VacationDays &&
                                                                            x.ExperienceLevelId == level.ExperienceLevelId &&
                                                                            x.Salary == model.Salary &&
                                                                            x.StartingDate == model.StartingDate)
                                              .FirstOrDefault();


            if (currentEmployee != null)
            {
                return true;
            }

            return false;
        }

        // Returns count of all employees in database for any office using office id
        public async Task<int> CountByOfficeIdAsync(int id)
        {
            var employees = await this.repository.AllAsNoTracking().Where(x => x.OfficeId == id).ToListAsync();
            var count = employees.Count();

            return count;
        }

        // Find employee by id and after that returns office id 
        public async Task<int> GetOfficeIdAsync(int id)
        {
            var employee = await this.repository.GetByIdAsync(id);
            var neededId = employee.OfficeId;

            return neededId;
        }
    }
}
