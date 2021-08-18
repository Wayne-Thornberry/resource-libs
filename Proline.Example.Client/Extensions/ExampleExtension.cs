
using Proline.Engine.Client;
using Proline.Engine.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Example.Client.Extensions
{
    public class ExampleExtension : EngineExtension
    {
        public override void OnEngineAPICall(string apiName, params object[] args)
        {
            Debugger.LogDebug(this, "Engine Call " + apiName + " ");
        }

        public override void OnComponentLoading(string componentName)
        {
           
            Debugger.LogDebug(this, "Component " + componentName + " Loadeding...");
        }

        public override void OnComponentInitialized(string componentName)
        { 
            Debugger.LogDebug(this, "Component " + componentName + " Loaded Succesfully");
        }

        public override void OnScriptInitialized(string scriptName)
        {
            Debugger.LogDebug(this, "Script  " + scriptName + " Initialized");
        }
    }
}
