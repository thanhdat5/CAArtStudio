using CAArtStudio.Data.Infrastructure;
using CAArtStudio.Model;

namespace CAArtStudio.Data.Repositories
{
    public interface ICourseFeedbackRepository : IRepository<CourseFeedback>
    {                                                                                           
    }

    public class CourseFeedbackRepository : RepositoryBase<CourseFeedback>, ICourseFeedbackRepository
	{
        public CourseFeedbackRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }  
    }
}