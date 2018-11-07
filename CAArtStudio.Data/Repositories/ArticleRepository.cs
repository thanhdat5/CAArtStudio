using CAArtStudio.Data.Infrastructure;
using CAArtStudio.Model;

namespace CAArtStudio.Data.Repositories
{
    public interface IArticleRepository : IRepository<Article>
    {                                                                                           
    }

    public class ArticleRepository : RepositoryBase<Article>, IArticleRepository
	{
        public ArticleRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }  
    }
}