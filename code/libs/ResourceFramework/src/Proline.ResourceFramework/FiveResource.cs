using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ResourceFramework
{
    public static class FiveResource
    {
        public static IHostBuilder CreateDefaultBuilder()
        {
            return new HostBuilder(new string[0]);
        }

        public static IHostBuilder CreateDefaultBuilder(string[] args)
        {
            return new HostBuilder(args);
        }
    }
}
