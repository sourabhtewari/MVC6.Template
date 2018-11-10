using Microsoft.EntityFrameworkCore.ChangeTracking;
using FMG.NRT.Objects;
using System;
using System.Collections.Generic;

namespace FMG.NRT.Data.Logging
{
    public interface IAuditLogger : IDisposable
    {
        void Log(IEnumerable<EntityEntry<BaseModel>> entries);
        void Save();
    }
}
