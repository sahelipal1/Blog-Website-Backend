using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Blog_website.DAL.Entity;
using Blog_website.DAL.Entity.DTO;

namespace Blog_website.Repo.IRepository
{
    public interface IRepository<T> where T : class
    {
        // Add an entity
        

        Task<T> AddAsync(T entity);

        // Update an entity
        Task<T> UpdateAsync(T entity);
        //Task<User?> GetUserById(int id);

        // Get an entity by its ID
        Task<T?> GetByIdAsync(int id);

        // Get all entities
        Task<List<T>> GetAllAsync();

        Task<T> GetSingleAsync(Expression<Func<T, bool>> condition);
        
            // Save changes to the database
            Task SaveChangesAsync();
      //  Task UpdateAsync(PostDTO.ResDTO existingPost);
        //Task<bool> SoftDeleteAsync(int id);
    }
}
