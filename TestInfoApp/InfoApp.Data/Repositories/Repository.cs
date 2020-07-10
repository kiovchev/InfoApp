using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfoApp.Data.Repositories
{
    // Abstraction layer between the data access layer and business logic layer of an application
    public class Repository<T> : IRepository<T> where T : class
    {
        private InfoAppDbContext context = null;
        private DbSet<T> table = null;
        public Repository(InfoAppDbContext context)
        {
            this.context = context;
            table = context.Set<T>();
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var needed = await table.ToListAsync();
            return needed;
        }
        public IQueryable<T> AllAsNoTracking()
        {
            return this.table.AsNoTracking();
        }

        public async Task<T> GetByIdAsync(object id)
        {
            var needed = await table.FindAsync(id);
            return needed;
        }
        public async Task InsertAsync(T obj)
        {
            await table.AddAsync(obj);
        }
        public void Update(T obj)
        {
            table.Attach(obj);
            context.Entry(obj).State = EntityState.Modified;
        }
        public async Task DeleteAsync(object id)
        {
            T existing = await table.FindAsync(id);
            table.Remove(existing);
        }
        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}

   
