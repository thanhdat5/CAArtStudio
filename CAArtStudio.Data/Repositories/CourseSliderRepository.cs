using CAArtStudio.Data.Infrastructure;
using CAArtStudio.Model;

namespace CAArtStudio.Data.Repositories
{
    public interface ICourseSliderRepository : IRepository<CourseSlider>
    {                                                                                           
    }

    public class CourseSliderRepository : RepositoryBase<CourseSlider>, ICourseSliderRepository
	{
        public CourseSliderRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }  
    }
}