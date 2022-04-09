using Proline.ResourceFramework;
using Proline.ResourceFramework.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ExampleClient
{
    public class Resource : ComponentContext
    {
        public static void Main(string[] args)
        {
            CreateBuilder(args).Build().Run();
        }


        public static IHostBuilder CreateBuilder(string[] args) =>
            FiveResource.CreateDefaultBuilder(args)
                .ConfigureHostDefaults(resourceBuilder =>
                {
                    resourceBuilder.UseStartup<Startup>();
                });
    }
}
