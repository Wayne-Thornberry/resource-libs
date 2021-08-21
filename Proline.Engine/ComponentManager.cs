 using Proline.Engine.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Engine
{
    public class ComponentManager
    {
        private static ComponentManager _instance;
        private Dictionary<string, EngineComponent> _components; 

        private ComponentManager()
        { 
            _components = new Dictionary<string, EngineComponent>();
        }

        internal static ComponentManager GetInstance()
        {
            if (_instance == null)
                _instance = new ComponentManager();
            return _instance;
        }

        internal void InitializeComponent(ComponentDetails componentDetails)
        {
            if (!EngineConfiguration.IsDebugEnabled && componentDetails.DebugOnly) throw new Exception("Component cannot be started, debug not enabled");
            if (componentDetails == null) throw new Exception("Component path null or empty");
            if (_components.ContainsKey(componentDetails.ComponentName)) throw new Exception("Component by that path already exists");
            var component = new EngineComponent(componentDetails);
            var em = ExtensionManager.GetInstance();
            var extensions = em.GetExtensions();
            foreach (var item in extensions)
            {
                item.OnComponentLoading(componentDetails.ComponentName);
            }
            component.Load();
            CitizenAccess.GetInstance().AddTick(component.Tick);
            Debugger.LogDebug(componentDetails.ComponentName);
            _components.Add(componentDetails.ComponentName, component);
            foreach (var item in extensions)
            {
                item.OnComponentInitialized(componentDetails.ComponentName);
            }
        }

        internal IEnumerable<EngineComponent> GetComponents()
        {
            return _components.Values;
        }
       

        internal EngineComponent GetComponent(string componentName)
        {
            return _components[componentName];
        }

        internal bool IsComponentRegistered(string componentName)
        {
            return _components.ContainsKey(componentName);
        }

        internal void RegisterComponent(EngineComponent component)
        {
            if (IsComponentRegistered(component.Name)) return; 
            var apiManager = APIManager.GetInstance();
            // Register its commands
            // Register its handles

            foreach (string apiName in component.GetAPIs())
            {
                apiManager.RegisterAPI(apiName, component);
            }
            _components.Add(component.Name, component);
        }
    }
}
