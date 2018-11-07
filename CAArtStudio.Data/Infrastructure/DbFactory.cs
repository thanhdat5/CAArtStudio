using CAArtStudio.Model;

namespace CAArtStudio.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        private CAArtStudio_dbContext dbContext;

        public CAArtStudio_dbContext Init()
        {
            return dbContext ?? (dbContext = new CAArtStudio_dbContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}