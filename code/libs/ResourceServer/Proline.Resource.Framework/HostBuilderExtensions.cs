using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Resource.Framework
{
    public static class HostBuilderExtensions
    {
        public static IHostBuilder ConfigureHostDefaults(this IHostBuilder builder, Action<IResourceHostBuilder> configure)
        {
            var resourceHostBuild = new ConcreteResourceHostBuilder();
            configure.Invoke(resourceHostBuild);
            var resource = resourceHostBuild.Build();
            builder.Properties.Add("Startup", resource);
            return builder;
        }
    }
}
