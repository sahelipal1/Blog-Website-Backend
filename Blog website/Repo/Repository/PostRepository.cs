using Blog_website.DAL.DBContext;
using Blog_website.DAL.Entity;
using Blog_website.DAL.Entity.DTO;
using Blog_website.Repo.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Blog_website.Repo.Repository
{
    public class PostRepository : Repository<Post, ApplicationDbtContext>, IPostRepository
    {
        private readonly ApplicationDbtContext _context;
       

        public PostRepository(ApplicationDbtContext context) : base(context)
        {
            _context = context;
        }
        // Step 1: Create the query with IQueryable
        public async Task<ICollection<UserPostDTO>> GetQuery()
        {
            IQueryable<UserPostDTO> query = from posts in _context.Posts
                                            join users in _context.Users on posts.CreatedBy equals users.Id
                                            join category in _context.Categories on posts.CategoryId equals category.Id
                                            select new UserPostDTO
                                            {
                                                UserId = users.Id,  // corrected property name
                                                PostId = posts.Id,
                                                Title = posts.Title,
                                                UserName = users.Name,
                                                CategoryId = category.Id,  // corrected property name
                                                CategoryName = category.CategoryName  // corrected assignment
                                            };

            // Step 2: Execute the query to get data from the database (deferred execution)
            ICollection<UserPostDTO> result = await query.ToListAsync();

            // Now you have an ICollection<PostDTO> containing the data
            return result;
        }


    }
}

        