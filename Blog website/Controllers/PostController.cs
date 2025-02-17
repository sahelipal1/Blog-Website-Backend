using Microsoft.AspNetCore.Mvc;
using Blog_website.BAL.IServices;
using Blog_website.DAL.Entity.DTO;
using System.Threading.Tasks;
using System;
using Blog_website.BAL.Services;
using Blog_website.DAL.Entity;
using Microsoft.EntityFrameworkCore;
using Blog_website.DAL.DBContext;

namespace Blog_website.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly IPostServices _postServices;
        private readonly ApplicationDbtContext _context;
        // Inject the EF Core context

        public PostController(IPostServices postServices, ApplicationDbtContext context)
        {
            _postServices = postServices;
            _context = context;
        }

        [HttpPost("CreatePost")]
        public async Task<IActionResult> CreatePost([FromBody] PostDTO.ResDTO postDTO)
        {
            if (postDTO == null) return BadRequest("Post data cannot be null.");

            try
            {
                var result = await _postServices.AddPostAsync(postDTO);
                return Ok(new { message = "Post created successfully.", post = result });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return StatusCode(500, $"Internal server error: {ex.Message}");
               
            }
        }

        [HttpGet("GetPostById/{id}")]
        public async Task<IActionResult> GetPostById(int id)
        {
            try
            {
                var post = await _postServices.GetPostByIdAsync(id);
                if (post == null) return NotFound(new { message = "Post not found." });

                return Ok(post);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetAllPosts")]
        public async Task<IActionResult> GetAllPosts()
        {
            try
            {
                var posts = await _postServices.GetAllPostsAsync();
                return Ok(posts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("Pagination")]
        [Produces("application/json")]
        public async Task<IActionResult> GetPosts(
            [FromQuery] int page = 1,
            [FromQuery] int limit = 6,
            [FromQuery] string? search = null)
        {
            // Start with the posts query and include related category data
            var query = _context.Posts
                .Include(p => p.CategoryNavigation)
                 .Where(p => p.IsDeleted == false || p.IsDeleted == null)
                .AsQueryable();

            //// Apply search filter if provided (searches in title and category name)
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(p =>
                    p.Title.Contains(search) ||
                    p.CategoryNavigation.CategoryName.Contains(search) ||
                     p.Description.Contains(search)); 
            }

            // Get the total count for pagination
            int totalCount = await query.CountAsync();

            // Apply pagination (skip records for previous pages and take the current page's records)
            var posts = await query
                .Skip((page - 1) * limit)
                .Take(limit)
                .ToListAsync();

            // Return the paginated list of posts along with the total count
            return Ok(new { items = posts, totalCount });
        }
    

        [HttpPut("UpdatePost")]
        public async Task<IActionResult> UpdatePost([FromBody] PostDTO.UpdateDTO postDTO)
        {
            if (postDTO == null) return BadRequest("Post data cannot be null.");

            try
            {
                var updatedPost = await _postServices.UpdatePostAsync(postDTO);
                if (updatedPost != null) return Ok(new { message = "Post updated successfully." });

                return NotFound(new { message = "Post not found." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("DeletePost")]
        public async Task<IActionResult> DeletePost( [FromBody] int id)
        {
            Console.WriteLine(id);
            try
            {
                var result = await _postServices.DeletePostByIdAsync(id);
                if (result) return Ok(new { message = "Post deleted successfully." });

                return NotFound(new { message = "Post not found." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");

            }
        }

    }
}

