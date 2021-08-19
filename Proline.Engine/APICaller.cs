using Proline.Engine.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Engine
{
    public static class APICaller
    {
        public static object CallAPI(string apiName, params object[] inputParameters)
        {
            var cm = ComponentManager.GetInstance();
            foreach (EngineComponent component in cm.GetComponents())
            {
                if (component.HasAPI(apiName))
                {
                    component.CallAPI(apiName, inputParameters);
                    return null;
                }
            }
            return null;
        } 
    }
}
