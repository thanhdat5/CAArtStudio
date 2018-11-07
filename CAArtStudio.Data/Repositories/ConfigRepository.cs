using CAArtStudio.Data.Infrastructure;
using CAArtStudio.Model;

namespace CAArtStudio.Data.Repositories
{
    public interface IConfigRepository : IRepository<Config>
    {                                                                                           
    }

    public class ConfigRepository : RepositoryBase<Config>, IConfigRepository
	{
        public ConfigRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }  
    }
}