using CAArtStudio.Data.Infrastructure;
using CAArtStudio.Model;

namespace CAArtStudio.Data.Repositories
{
    public interface IGroupRepository : IRepository<Group>
    {                                                                                           
    }

    public class GroupRepository : RepositoryBase<Group>, IGroupRepository
	{
        public GroupRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }  
    }
}