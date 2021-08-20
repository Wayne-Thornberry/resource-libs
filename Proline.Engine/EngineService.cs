using Newtonsoft.Json;

using Proline.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Proline.Engine
{
    public class EngineService
    { 
        private CitizenResource _executingResource;

        public EngineService(IScriptSource tickSubscriber)
        {
            CitizenAccess.SetScriptSource(tickSubscriber);  
        }

        public void Initialize(string executingResource = "")
        {
            if (EngineStatus.IsEngineInitialized) throw new Exception("Cannot Initialize engine, engine already initilized");
            PreLoadAssemblies();

            _executingResource = new CitizenResource(executingResource);

            // If the engine is in client mode
            if(EngineConfiguration.EnvType == 0)
            {

            }
            else
            {

            }

            ExtensionManager.Initialize();
            ComponentManager.Initialize();
            ScriptManager.Initialize();

            EngineStatus.IsEngineInitialized = true;
        }

        public void StartStartupScripts()
        {
            try
            { 
                var sm = ScriptManager.GetScriptAssemblies();
                foreach (var item in sm)
                {
                    EngineAccess.StartNewScript(item.StartupScript, null);
                }
            }
            catch (Exception e)
            {
                Debugger.LogDebug(e.ToString());
                
            }
        }

        private void PreLoadAssemblies()
        {
            var _assemblies = EngineConfiguration.GetAssemblies().ToArray();
            foreach (var item in _assemblies)
            {
                try
                { 
                    Assembly.Load(item);
                }
                catch (Exception e)
                {
                    Debugger.LogError(e.ToString()); 
                }
            }
        }

        public void StartAllComponents()
        {
            ComponentControl.StartAllComponents();
        }

        public void StopAllComponents()
        { 
            ComponentControl.StopAllComponents();
        }

        public static ComponentAPI GetComponentAPI(string componentName)
        {
            var cm = ComponentManager.GetInstance();
            var component = cm.GetComponent(componentName);
            return component.GetAPI();
        }

        public static object ExecuteEngineMethod(string methodName, params object[] args)
        {
            Debugger.LogDebug("Called Engine event " + methodName);
            switch (methodName)
            {
                case "LogDebug": 
                    Debugger.LogDebug(args[0]);
                    return null;
                    break;
                case "LogError":
                    Debugger.LogError(args[0]);
                    return null;
                    break;
                case "LogWarn":
                    Debugger.LogWarn(args[0]);
                    return null;
                    break;
                case "ExecuteComponentControl":
                    var componentNAme = args[0].ToString();
                    var apiName = args[1].ToString();
                    var list = JsonConvert.DeserializeObject<object[]>(args[2].ToString());
                    return APICaller.CallAPI(apiName, list.ToArray()); 
                default:
                    return null;
            }
        }

        public static object ExecuteComponentControl(string componentName, string control, object[] args)
        {
           return ComponentAccess.ExecuteComponentControl(componentName, control, args);
        }

        public static void ExecuteComponentCommand(string componentName, string command, object[] args)
        {
            ComponentAccess.ExecuteComponentCommand(componentName, command, args);
        } 
    }
}
