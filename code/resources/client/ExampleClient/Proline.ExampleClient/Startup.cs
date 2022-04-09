using CitizenFX.Core;
using CitizenFX.Core.Native;
using Proline.Example.Configuration;
using Proline.ResourceFramework;  
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ExampleClient
{
    public class Startup
    {
        protected Log _log = new Log();
        public Startup()
        {

        }

        public void ConfigureServices(IResourceService service)
        {
            service.AddExports();
            service.AddControllers();
            service.AddEvents();
            service.AddScripts();
        }

        public void Configure(IResourceApplication app, IResourceEnvironment env)
        { 
            app.BindConfiguration<ExampleConfig>();
            _log.Debug("Cdsadasd");
            //ExportAPI.CallResourceExport().Test();
            //var client = new EventClient(this);
            //var resposne = await client.SendAsync("es:" + API.GetCurrentResourceName(), "");
            //_log.Debug(resposne.ToString()); 
            //EventManager.InvokeEvent("ExampleEvent");
        }
         
    }
}
