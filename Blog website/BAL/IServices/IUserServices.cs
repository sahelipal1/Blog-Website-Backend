using Blog_website.DAL.DBContext;
using Blog_website.DAL.Entity;
using Blog_website.DAL.Entity.DTO;
using static Blog_website.DAL.Entity.DTO.UserDTO;

namespace Blog_website.BAL.IServices
{
 
        public interface IUserServices
        {


        Task<ResDTO> AddUser(ReqDTO nuserDto);


        public Task<ResDTO> UpdateUser(int id, ReqDTO userDto);

        //Task<User?> GetUserById(int id);
        Task<User?> GetByIdAsync(int id);
        Task<List<User>> GetAllUsersAsync();
        Task<bool> DeleteUserAsync(int id);
        
    }


}