using Blog_website.DAL.DBContext;
using Blog_website.DAL.Entity;
using Blog_website.Repo.IRepository;

namespace Blog_website.Repo.Repository
{
    public class UserRepository : Repository<User, ApplicationDbtContext>, IUser
    {
        public UserRepository(ApplicationDbtContext context) : base(context)
        {
        }

  
    }
}

