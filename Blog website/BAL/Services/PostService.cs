using Blog_website.BAL.IServices;
using Blog_website.DAL.Entity;
using Blog_website.DAL.Entity.DTO;

using Blog_website.Repo.IRepository;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blog_website.Repo.Repository;

namespace Blog_website.BAL.Services
{
    public class PostService : IPostServices
    {
        private readonly IRepository<Post> _postRepository;
        private readonly IMapper _mapper;

        public PostService(IRepository<Post> postRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }

        public async Task<PostDTO.ResDTO> AddPostAsync(PostDTO.ResDTO postDTO)
        {
            var post = _mapper.Map<Post>(postDTO);
            var addedPost = await _postRepository.AddAsync(post);  // AddAsync is assumed to return Task<Post>
            return _mapper.Map<PostDTO.ResDTO>(addedPost);
        }


        public async Task<PostDTO.UpdateDTO?> UpdatePostAsync(PostDTO.UpdateDTO postDTO)
        {
            // Step 1: Search for the post by its ID
            var existingPost = await _postRepository.GetByIdAsync(postDTO.Id);  // Assuming `postDTO.Id` contains the ID to update

            // Step 2: Check if the post exists
            if (existingPost == null)
            {
                // Return null if the post doesn't exist
                return null;
            }
            // Step 3: Map the values from UpdateDTO to the existing post
            _mapper.Map(postDTO, existingPost);  // AutoMapper will update existingPost with values from postDTO

            // Step 4: Update the post in the repository
            var updatedPost = await _postRepository.UpdateAsync(existingPost);

            // Step 5: Return the updated post as a DTO
            return _mapper.Map<PostDTO.UpdateDTO>(updatedPost);
            


        }


        public async Task<PostDTO.ResDTO?> GetPostByIdAsync(int id)
        {
            var post = await _postRepository.GetByIdAsync(id);


            return post == null ? null : _mapper.Map<PostDTO.ResDTO>(post);
        }

        public async Task<List<PostDTO.UpdateDTO>> GetAllPostsAsync()
        {
            var posts = await _postRepository.GetAllAsync();
            return _mapper.Map<List<PostDTO.UpdateDTO>>(posts);
        }

        public async Task<bool> DeletePostByIdAsync(int id)
        {
            var existingPost = await _postRepository.GetSingleAsync(p =>
    p.Id == id && (p.IsDeleted == false || p.IsDeleted == null));
            if (existingPost == null) return false;
            
            existingPost.IsDeleted = true;
            
            try
            {
                await _postRepository.UpdateAsync(existingPost);
                return true;
            }
            catch (Exception)
            {
                return false;  // Logs should be added for debugging.
            }
        }

    }

}
