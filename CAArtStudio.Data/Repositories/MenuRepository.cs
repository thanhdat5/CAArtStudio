using CAArtStudio.Data.Infrastructure;
using CAArtStudio.Model;

namespace CAArtStudio.Data.Repositories
{
    public interface IMenuRepository : IRepository<Menu>
    {                                                                                           
    }

    public class MenuRepository : RepositoryBase<Menu>, IMenuRepository
	{
        public MenuRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }  
    }
}