using InfoApp.Common.DtoModels.EmployeeDtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InfoApp.Services.Data.Contracts
{
    public interface IEmployeeService
    {
        Task<EmployeeAllDtoModel> GetAllEmployeesByCompany(int id);

        Task<EmployeeAllByOfficeDtoModel> GetAllEmployeesByOffice(int id);

        Task Create(EmployeeCreateInputDtoModel model);

        Task<int> DeleteEmployee(int id);

        int GetCompanyId(int id);

        Task<EmployeeEditOutputDtoModel> GetEmployeeById(int id);

        Task<int> EditEmployee(EmployeeEditInputDtoModel model);

        Task<int> CountAsyncByCompanyId(int id);

        Task<bool> IfExists(EmployeeCreateInputDtoModel model);

        Task<bool> isSame(EmployeeEditInputDtoModel model);

        Task<int> CountByOfficeIdAsync(int id);

        Task<int> GetOfficeIdAsync(int id);
    }
}
