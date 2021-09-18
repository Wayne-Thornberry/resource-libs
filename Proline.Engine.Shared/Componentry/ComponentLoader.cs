using Proline.Engine.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proline.Engine.Internals;

namespace Proline.Engine.Componentry
{
    internal class ComponentLoader
    {
        internal void LoadComponents()
        {
            var _componentDetails = new List<ComponentDetails>(EngineConfiguration.Components);
            var am = InternalManager.GetInstance();
            var extensions = am.GetExtensions();
            foreach (var componentDetails in _componentDetails)
            {
                if (!EngineConfiguration.IsDebugEnabled && componentDetails.DebugOnly) throw new Exception("Component cannot be started, debug not enabled");
                if (componentDetails == null) throw new Exception("Component path null");
                try
                {
                    var component = EngineComponent.Load(componentDetails);
                    if (component == null) continue;
                    EngineComponent.RegisterComponent(component);
                }
                catch (Exception e)
                {
                    //Debugger.LogDebug(e);
                }
            }
        }
    }
}
