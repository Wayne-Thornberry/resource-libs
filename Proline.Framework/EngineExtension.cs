using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Engine.Framework
{
    public partial class EngineExtension : IEngineTracker
    {
        public string Name { get; }
        public string Type { get; }

        protected EngineExtension()
        {
            Name = this.GetType().Name;
            Type = "Extension";
        }
        public virtual void OnComponentStarted(string componentName)
        {

        }

        public virtual void OnComponentStopped(string componentName)
        {

        }

        public virtual void OnComponentLoading(string componentName)
        {

        }

        public virtual void OnComponentInitialized(string componentName)
        {

        }

        public virtual void OnEngineAPICall(string apiName, params object[] args)
        {

        }

        public virtual void OnScriptInitialized(string scriptName)
        {

        }

        public virtual void OnScriptStarted(string scriptName)
        {

        }

        public virtual void OnInitialize()
        {

        }
    }
}
