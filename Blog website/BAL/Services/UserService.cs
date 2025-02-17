using Blog_website.DAL.Entity;
using Blog_website.DAL.Entity.DTO;
using AutoMapper;
using Blog_website.Repo.IRepository;
using Blog_website.BAL.IServices;
using Blog_website.DAL.DBContext;
using static Blog_website.DAL.Entity.DTO.UserDTO;
using Microsoft.EntityFrameworkCore;

namespace Blog_website.BAL.Services
{
    public class UserService : IUserServices
    {
        private readonly IRepository<User> _userRepository;
        private readonly IMapper _mapper;
        private readonly int? currentUserId;

        public UserService(IRepository<User> userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        // Add a new user
        public async Task<ResDTO> AddUser(ReqDTO nuserDto)
        {
            try
            {
                var userEntity = _mapper.Map<User>(nuserDto);
                userEntity.Createdby = currentUserId;
                userEntity.Createddate = DateOnly.FromDateTime(DateTime.Now);

                await _userRepository.AddAsync(userEntity);

                var createdUserDto = _mapper.Map<ResDTO>(userEntity);
                return createdUserDto;
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.ToString()); // Log full stack trace

                throw new Exception("An error occurred while adding the user.", ex);
            }
        }

        // Update an existing user
        public async Task<ResDTO> UpdateUser(int id, ReqDTO userDto)
        {
            var existingUser = await _userRepository.GetByIdAsync(id);
            if (existingUser == null)
            {
                throw new KeyNotFoundException("User not found.");
            }

            existingUser.Name = userDto.Name;
            existingUser.Email = userDto.Email;
            existingUser.Phonenumber = userDto.Phonenumber;
            //existingUser.Password = userDto.Password;
            existingUser.Updatedby = userDto.Updatedby;
            existingUser.Updateddate = DateOnly.FromDateTime(DateTime.Now);

            try
            {
                await _userRepository.UpdateAsync(existingUser);
                var updatedUserDto = _mapper.Map<ResDTO>(existingUser);
                return updatedUserDto;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the user.", ex);
            }
        }

        // Fetch all users
        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllAsync();
        }
       

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        // Delete a user (soft delete)

        public async Task<bool> DeleteUserAsync(int id)
        {
            var existingUser = await _userRepository.GetByIdAsync(id);
            if (existingUser == null)
            {
                return false;
            }

            existingUser.IsDeleted = true;
            var updatedUser = await _userRepository.UpdateAsync(existingUser);
            return (bool)updatedUser.IsDeleted;
        }

    }
}

