using CitizenFX.Core.Native;
using Proline.Example.Configuration;
using Proline.ResourceFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Example
{
    public class Startup : ComponentContext
    {

        public override async Task ConfigureServices(IResourceEnvironment env)
        {
            env.AddExports();
            env.AddControllers();
            env.AddEvents();
        }


        public override async Task Configure(IResourceApp app)
        {
            app.BindConfiguration<ExampleConfig>();
        }

    }
}
