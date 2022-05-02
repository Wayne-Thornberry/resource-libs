using Proline.Resource.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Resource.Framework
{
    public class ResourceService : IResourceService
    {
        private Log _log = new Log();
        private Dictionary<string, Type> _scriptTypes;
        private Dictionary<string, Type> _apiTypes;
        private Dictionary<string, Type> _controllerTypes;

        private List<object> _controllers;
        private List<IResourceScript> _scripts;


        public ResourceService()
        {
            _controllers = new List<object>();
            _scripts = new List<IResourceScript>();

            _scriptTypes = new Dictionary<string, Type>();
            _controllerTypes = new Dictionary<string, Type>();
            _apiTypes = new Dictionary<string, Type>();
        }

        public void AddEvents()
        {
            //var execAssembly = Assembly.GetCallingAssembly();
            //var types = execAssembly.GetTypes();
            //var apiTypes = types.Where(e => e.GetCustomAttribute<EventControllerAttribute>() != null);
            //foreach (var type in apiTypes)
            //{
            //    RegisterControllerType(type);
            //}
        }

        public void AddControllers()
        {
            //var execAssembly = Assembly.GetCallingAssembly();
            //var types = execAssembly.GetTypes();
            //var apiTypes = types.Where(e => e.GetCustomAttribute<ClientControllerAttribute>() != null);
            //foreach (var type in apiTypes)
            //{
            //    RegisterControllerType(type);
            //}
        }

        public void AddAPI<T>()
        {
            var type = typeof(T);
            RegisterAPI(type.Name, type);
        }

        private void RegisterAPI(string name, Type type)
        {
            _apiTypes.Add(name, type);
        }

        public void AddAPI<T>(string v)
        {
            var type = typeof(T);
            RegisterAPI(v, type);
        }

        public void AddExports()
        {
            //var execAssembly = Assembly.GetCallingAssembly();
            //var types = execAssembly.GetTypes();
            //var apiTypes = types.Where(e => e.GetCustomAttribute<ExportControllerAttribute>() != null);
            //foreach (var type in apiTypes)
            //{
            //    RegisterControllerType(type);
            //}
        }

        public void AddScripts()
        {
            //var execAssembly = Assembly.GetCallingAssembly();
            //var types = execAssembly.GetTypes();
            //var apiTypes = types.Where(e => e.BaseType == typeof(ResourceScript));
            //_log.Debug("Found types");
            //foreach (var type in apiTypes)
            //{
            //    RegisterScriptType(type);
            //}
        }
    }
}
