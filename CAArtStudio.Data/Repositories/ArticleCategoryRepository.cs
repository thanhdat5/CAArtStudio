using CAArtStudio.Data.Infrastructure;
using CAArtStudio.Model;

namespace CAArtStudio.Data.Repositories
{
    public interface IArticleCategoryRepository : IRepository<ArticleCategory>
    {                                                                                           
    }

    public class ArticleCategoryRepository : RepositoryBase<ArticleCategory>, IArticleCategoryRepository
	{
        public ArticleCategoryRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }  
    }
}