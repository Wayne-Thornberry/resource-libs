using Newtonsoft.Json;
using Proline.Engine.Data;
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

        public void Initialize(params string[] args)
        {
            try
            { 
                if (EngineStatus.IsEngineInitialized) throw new Exception("Cannot Initialize engine, engine already initilized");

                var resourceName = args[0];
                var envType = int.Parse(args[1]);
                var isDebug = bool.Parse(args[2]);

                LoadConfig(isDebug);
                LoadAssemblies();

                InitializeExtensions();
                InitializeComponents();
                InitializeScripts();

                SetupResource(resourceName);
                RunEnvSpecificFunctions(envType);


                EngineStatus.IsEngineInitialized = true;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        private void RunEnvSpecificFunctions(int envType)
        { 
            if (envType == 0)
            {

            }
            else
            {

            }
        }

        private void LoadConfig(bool isDebugEnabled)
        {
            EngineConfiguration.LoadConfig();
            EngineConfiguration.IsDebugEnabled = isDebugEnabled;
        }

        private void SetupResource(string executingResource)
        {
            _executingResource = new CitizenResource(executingResource);
        }

        private static void InitializeComponents()
        {
            if (EngineStatus.IsComponentsInitialized) return;
            var _componentDetails = new List<ComponentDetails>(EngineConfiguration.Components);

            if (_componentDetails != null)
            {
                var cm = ComponentManager.GetInstance();
                foreach (var componentPath in _componentDetails)
                {
                    try
                    {
                        cm.InitializeComponent(componentPath);
                    }
                    catch (Exception e)
                    {
                        Debugger.LogDebug(e);
                    }
                }
            }
            EngineStatus.IsComponentsInitialized = true;
        }


        private static void InitializeExtensions()
        {
            if (EngineStatus.IsExtensionsInitialized) return;
            var _extensionDetails = new List<ExtensionDetails>(EngineConfiguration.Extensions);
            var em = ExtensionManager.GetInstance();
            if (_extensionDetails != null)
            {
                foreach (var extensionPath in _extensionDetails)
                {
                    try
                    {
                        em.InitializeExtension(extensionPath);
                    }
                    catch (Exception e)
                    {
                        Debugger.LogDebug(e);
                    }
                }

            }
            EngineStatus.IsExtensionsInitialized = true;
        }

        private static void InitializeScripts()
        {
            if (EngineStatus.IsScriptsInitialized) return;
            //InsertScriptAssemblies();

            var spm = ScriptPackageManager.GetInstance();
            var sm = ScriptManager.GetInstance();
            foreach (var item in EngineConfiguration.ScriptPackages)
            {
                try
                { 
                    var sp = ScriptPackage.Load(item); 
                    spm.RegisterScriptPackage(sp);
                }
                catch (Exception e)
                {
                    Debugger.LogError(e.ToString(), true);
                    throw;
                }
            }
            Debugger.LogDebug(sm.GetRegisteredScriptCount() + " Scripts Registered");
            EngineStatus.IsScriptsInitialized = true;
        }

        public void StartStartupScripts()
        {
            ScriptAccess.StartStartupScripts();
        }

        private void LoadAssemblies()
        { 
            foreach (var item in EngineConfiguration.Assemblies)
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
