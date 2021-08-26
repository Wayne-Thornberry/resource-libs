

using Proline.Engine;
using Proline.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Example.Extensions
{
    [Client]
    public class ExampleExtension : EngineExtension
    {
        public override void OnEngineAPICall(string apiName, params object[] args)
        {
            LogDebug("Engine Call " + apiName + " ");
        }

        public override void OnComponentLoading(string componentName)
        {
            LogDebug("Component " + componentName + " Loadeding...");
        }

        public override void OnComponentInitialized(string componentName)
        { 
            LogDebug("Component " + componentName + " Loaded Succesfully");
        }

        public override void OnScriptInitialized(string scriptName)
        {
            LogDebug("Script  " + scriptName + " Initialized");
        }
    }
}
