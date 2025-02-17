using System.Linq.Expressions;
using Blog_website.DAL.DBContext;
using Blog_website.DAL.Entity;
using Blog_website.Repo.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Blog_website.Repo.Repository
{
    public class Repository<T, Tcontext> : IRepository<T> where T : class where Tcontext : DbContext
    {
        protected readonly Tcontext _DbContext;
        private readonly DbSet<T> _dbSet;
        public Repository(Tcontext context)
        {
            _DbContext = context;
            _dbSet = _DbContext.Set<T>();
        }

       

        // Asynchronous add
        public async Task<T> AddAsync(T entity)
        {
            await _DbContext.Set<T>().AddAsync(entity);
            await SaveChangesAsync();
            return entity;
        }

        

        // Asynchronous update
        public async Task<T> UpdateAsync(T entity)
        {
            _DbContext.Entry(entity).State = EntityState.Modified;
            await SaveChangesAsync();
            return entity;
        }

        // Fetch all data
        //public async Task<List<T>> GetAllAsync()
        //{
        //    return await _DbContext.Set<T>().ToListAsync();
        //}

        //public async Task<List<T>> GetAllAsync()
        //{
        //    return await _DbContext.Set<T>()
        //                           .Where(e => !EF.Property<bool>(e, "IsDeleted"))
        //                           .ToListAsync();
        //}
        public async Task<List<T>> GetAllAsync()
        {
            return await _DbContext.Set<T>()
                .Where(e => EF.Property<bool?>(e, "IsDeleted") == false || EF.Property<bool?>(e, "IsDeleted") == null)
                .ToListAsync();
        }





        // Asynchronous save changes
        public async Task SaveChangesAsync()
        {
            await _DbContext.SaveChangesAsync();
        }

        // Get an entity by its ID (asynchronously)

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);
        }
        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> condition)
        {
            return await _DbContext.Set<T>().FirstOrDefaultAsync(condition);
        }

    }
}
