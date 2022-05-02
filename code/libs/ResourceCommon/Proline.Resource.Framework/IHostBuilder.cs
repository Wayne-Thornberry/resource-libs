using System.Collections.Generic;

namespace Proline.Resource.Framework
{
    public interface IHostBuilder
    {
        IDictionary<object, object> Properties { get; }
        IHost Build();
    }
}