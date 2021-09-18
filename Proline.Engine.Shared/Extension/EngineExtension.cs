using Proline.Engine.Internals;

namespace Proline.Engine.Extension
{
    public partial class EngineExtension : EngineObject
    {
        protected EngineExtension() : base("Extension")
        {

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

        public virtual void Initialize()
        {

        }

        public void OnScriptFinished(string name)
        {

        }
    }
}
