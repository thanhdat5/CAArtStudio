using CAArtStudio.Data.Infrastructure;
using CAArtStudio.Model;

namespace CAArtStudio.Data.Repositories
{
    public interface IUserRepository : IRepository<User>
    {                                                                                           
    }

    public class UserRepository : RepositoryBase<User>, IUserRepository
	{
        public UserRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }  
    }
}