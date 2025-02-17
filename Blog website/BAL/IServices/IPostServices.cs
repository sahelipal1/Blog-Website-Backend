using Blog_website.DAL.Entity.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog_website.BAL.IServices
{
    public interface IPostServices
    {
        Task<PostDTO.ResDTO> AddPostAsync(PostDTO.ResDTO postDTO);
        Task<PostDTO.UpdateDTO> UpdatePostAsync(PostDTO.UpdateDTO postDTO);
        Task<PostDTO.ResDTO?> GetPostByIdAsync(int id);
        Task<List<PostDTO.UpdateDTO>> GetAllPostsAsync();
        Task<bool> DeletePostByIdAsync(int id);
    }
}


