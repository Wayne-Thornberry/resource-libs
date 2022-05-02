using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Resource.Framework
{
    public static class ResourceHostBuilderExtensions
    {
        public static IResourceHostBuilder UseStartup<T>(this IResourceHostBuilder hostBuilder) where T : new()
        {
            hostBuilder.UseSetting("Startup", typeof(T).FullName);
            return hostBuilder;
        }
    }
}
