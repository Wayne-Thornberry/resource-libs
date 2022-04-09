using Proline.ResourceFramework.Extensions;
using System.Collections.Generic;

namespace Proline.ResourceFramework
{
    public interface IHostBuilder
    {
        IDictionary<object, object> Properties { get; }
        IHost Build();
    }
}