using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CitizenFX.Core;
using Proline.Resource.Logging;


namespace Proline.Resource.Framework
{
    public abstract class ResourceContext : BaseScript
    {
        protected Log _log = new Log(); 
        private Assembly _sourcAssembly;
        private List<ResourceScript> _scripts;

        public string Name { get; internal set; } 

        public ResourceContext()
        {
            _scripts = new List<ResourceScript>();
            _sourcAssembly = Assembly.GetCallingAssembly();
            Tick += InternalOnTick;
        }

        public void AddTick(Func<Task> x)
        {
            Tick += x;
        }

        public async Task InternalOnTick()
        { 
            try
            {
                var main = (object) GetMainMethod();

                CreateScriptInstances();


                if (main == null)
                {
                    Console.Console.WriteLine(_log.Debug("A main method could not be found, Resource cannot be started properly")); 
                }
                else
                {
                    ((MethodInfo)main).Invoke(null, new object[] { new string[] { (ResourceType.CLIENT.ToString()) } }); 
                    SubscribeScriptsToTick();
                }
            }
            catch (Exception e)
            {
                Console.Console.WriteLine(_log.Error(e.ToString()));
                //throw;
            }
            finally
            {
                Tick -= InternalOnTick;
            }
        }

        private void SubscribeScriptsToTick()
        {
            foreach (var item in _scripts)
            {
                item.State = 0;
                Console.Console.WriteLine(_log.Debug(item.GetType().Name));
                AddTick(item.OnTick);
            }
        }

        private void CreateScriptInstances()
        {
            var types = _sourcAssembly.GetTypes().Where(e=>e.BaseType == typeof(ResourceScript));
            foreach (var item in types)
            {
                Console.Console.WriteLine(_log.Debug(item.Name));
                var instance = Activator.CreateInstance(item);
                _scripts.Add((ResourceScript)instance);
            } 
        }

        private MethodInfo GetMainMethod()
        {
            var types = _sourcAssembly.GetTypes();
            foreach (var item in types)
            {
                Console.Console.WriteLine(_log.Debug(item.Name)); 
                var method = (object) item.GetMethod("Main", BindingFlags.Public | BindingFlags.Static);
                if (method == null) continue;
                return (MethodInfo) method;
            }
            return null;
        }
    }
}
