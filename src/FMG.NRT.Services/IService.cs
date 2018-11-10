using System;

namespace FMG.NRT.Services
{
    public interface IService : IDisposable
    {
        Int32 CurrentAccountId { get; set; }
    }
}
