
using Proline.Core.Client;
using Proline.Engine;
using Proline.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Core.Client.Extensions
{
    public class Tracking : EngineExtension
    {
        public override void OnEngineAPICall(string apiName, params object[] args)
        {
           
            Debugger.LogDebug("Engine Call " + apiName + " ");
        }

        public override void OnComponentLoading(string componentName)
        {
           
            Debugger.LogDebug("Component " + componentName + " Loadeding...");
        }

        public override void OnComponentInitialized(string componentName)
        {
           
            Debugger.LogDebug("Component " + componentName + " Loaded Succesfully");
        }
    }
}
