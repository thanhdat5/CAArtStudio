using CAArtStudio.Data.Infrastructure;
using CAArtStudio.Model;

namespace CAArtStudio.Data.Repositories
{
    public interface ICourseCategoryRepository : IRepository<CourseCategory>
    {                                                                                           
    }

    public class CourseCategoryRepository : RepositoryBase<CourseCategory>, ICourseCategoryRepository
	{
        public CourseCategoryRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }  
    }
}