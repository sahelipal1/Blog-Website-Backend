using Blog_website.DAL.Entity;
using Blog_website.DAL.Entity.DTO;

namespace Blog_website.Repo.IRepository
{
   public interface IUser:IRepository<User>
     //public interface IUser<T> where T : class
    {
       // bool AddUser(User user);
       // bool UpdateUser(User user); // To update an existing user
       // new Task<User> UpdateAsync(User user);
        Task SaveChangesAsync();
    }
}


