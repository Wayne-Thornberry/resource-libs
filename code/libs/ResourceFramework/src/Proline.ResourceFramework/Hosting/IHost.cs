using System;

namespace Proline.ResourceFramework
{
    public interface IHost
    {
        IServiceProvider Services { get; }
        void Start();
    }
}