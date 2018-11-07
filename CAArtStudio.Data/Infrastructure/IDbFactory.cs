using System;
using CAArtStudio.Model;

namespace CAArtStudio.Data.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        CAArtStudio_dbContext Init();
    }
}