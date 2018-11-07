using CAArtStudio.Data.Infrastructure;
using CAArtStudio.Model;

namespace CAArtStudio.Data.Repositories
{
    public interface ICourseTeacherRepository : IRepository<CourseTeacher>
    {                                                                                           
    }

    public class CourseTeacherRepository : RepositoryBase<CourseTeacher>, ICourseTeacherRepository
	{
        public CourseTeacherRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }  
    }
}