using Proline.Engine.Extension;

namespace Proline.Classic.Extensions
{
    public class Tracking : EngineExtension
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
    }
}
