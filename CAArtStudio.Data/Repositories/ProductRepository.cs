using CAArtStudio.Data.Infrastructure;
using CAArtStudio.Model;

namespace CAArtStudio.Data.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {                                                                                           
    }

    public class ProductRepository : RepositoryBase<Product>, IProductRepository
	{
        public ProductRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }  
    }
}