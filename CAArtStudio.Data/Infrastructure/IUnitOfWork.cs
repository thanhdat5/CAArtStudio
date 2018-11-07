namespace CAArtStudio.Data.Infrastructure
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}