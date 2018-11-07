using CAArtStudio.Data.Infrastructure;
using CAArtStudio.Model;

namespace CAArtStudio.Data.Repositories
{
    public interface ICourseRepository : IRepository<Course>
    {                                                                                           
    }

    public class CourseRepository : RepositoryBase<Course>, ICourseRepository
	{
        public CourseRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }  
    }
}