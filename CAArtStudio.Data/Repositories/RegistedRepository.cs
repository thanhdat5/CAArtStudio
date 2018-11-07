using CAArtStudio.Data.Infrastructure;
using CAArtStudio.Model;

namespace CAArtStudio.Data.Repositories
{
    public interface IRegistedRepository : IRepository<Registed>
    {                                                                                           
    }

    public class RegistedRepository : RepositoryBase<Registed>, IRegistedRepository
	{
        public RegistedRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }  
    }
}