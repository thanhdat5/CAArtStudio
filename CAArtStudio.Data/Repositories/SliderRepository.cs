using CAArtStudio.Data.Infrastructure;
using CAArtStudio.Model;

namespace CAArtStudio.Data.Repositories
{
    public interface ISliderRepository : IRepository<Slider>
    {                                                                                           
    }

    public class SliderRepository : RepositoryBase<Slider>, ISliderRepository
	{
        public SliderRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }  
    }
}