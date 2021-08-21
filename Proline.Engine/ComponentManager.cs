 using Proline.Engine.Data;
using Proline.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Engine
{
    internal class ComponentManager
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
            var comManager = CommandManager.GetInstance();
            foreach (ComponentCommand command in component.GetCommands())
            {
                comManager.RegisterCommand(command);
            } 

            foreach (ComponentAPI apiName in component.GetAPIs())
            {
                apiManager.RegisterAPI(apiName);
            }
            Debugger.LogDebug("Registered " + component.Type + " " + component.Name);
            _components.Add(component.Name, component);
        }

        internal void UnregisterComponent(EngineComponent component)
        {
            if (!IsComponentRegistered(component.Name)) return;
            var apiManager = APIManager.GetInstance();
            var comManager = CommandManager.GetInstance();
            foreach (ComponentCommand command in component.GetCommands())
            {
                comManager.UnregisterCommand(command);
            }

            foreach (ComponentAPI apiName in component.GetAPIs())
            {
                apiManager.UnregisterAPI(apiName);
            }
            _components.Remove(component.Name);
        }
    }
}
