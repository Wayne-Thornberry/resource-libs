using System;

namespace Proline.Resource.Framework
{
    public interface IHost
    {
        IServiceProvider Services { get; }
        void Start();
    }
}