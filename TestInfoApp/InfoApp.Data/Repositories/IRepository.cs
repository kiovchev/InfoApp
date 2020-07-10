using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfoApp.Data.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        IQueryable<T> AllAsNoTracking();
        Task<T> GetByIdAsync(object id);
        Task InsertAsync(T obj);
        void Update(T obj);
        Task DeleteAsync(object id);
        Task SaveAsync();
    }
}
