using SR.DataAccess;
using SR.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace SR.Repository
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {

        protected readonly SchoolContext _dbContext;

        protected GenericRepository(SchoolContext context)
        {
            _dbContext = context;
        }

        public async Task Add(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }

        public async Task<T> Get(int id)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return await _dbContext.Set<T>().FindAsync(id);
#pragma warning restore CS8603 // Possible null reference return.
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public void Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
        }
    }
}
