using Newtonsoft.Json;
using Proline.Engine.Data;
using Proline.Engine;
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
        private long _lastCheck;

        public EngineService(IScriptSource tickSubscriber)
        {
            CitizenAccess.SetScriptSource(tickSubscriber);  
        }

        public async Task Initialize(params string[] args)
        {
            try
            { 
                if (EngineStatus.IsEngineInitialized) throw new Exception("Cannot Initialize engine, engine already initilized");

                var resourceName = args[0];
                var envType = int.Parse(args[1]);
                var isDebug = bool.Parse(args[2]);
                EngineConfiguration.IsClient = envType != -1;
                var ty = EngineConfiguration.IsClient ? "Client" : "Server";

                int status = 0;
                if (EngineConfiguration.IsClient)
                {
                    status = await EngineAccess.ExecuteEngineMethodServer<int>("Healthcheck");
                }
                else
                {
                    status = 1;
                }

                if (status == 0)
                    throw new Exception("Cannot initialize client side, server side not healthy");
                LoadConfig(isDebug);
                LoadAssemblies();
                Debugger.LogDebug("Engine in " + ty + " Mode");

                InitializeExtensions();
                InitializeComponents();

                // Create the components
                // Register all apis from components
                // Initialize all components


                InitializeScripts();

                //_startupScripts = config.StartScripts != null ? config.StartScripts : new string[0];

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
            var am = InternalManager.GetInstance();
            var com = InternalManager.GetInstance();
            var cm = InternalManager.GetInstance();
            if (_componentDetails != null)
            {
                foreach (var componentDetails in _componentDetails)
                {
                    if (!EngineConfiguration.IsClient && componentDetails.EnvType == 1) continue;
                    if (EngineConfiguration.IsClient && componentDetails.EnvType == -1) continue;
                    if (!EngineConfiguration.IsDebugEnabled && componentDetails.DebugOnly) throw new Exception("Component cannot be started, debug not enabled");
                    if (componentDetails == null) throw new Exception("Component path null");
                    try
                    {
                        if (cm.IsComponentRegistered(componentDetails.ComponentName)) throw new Exception("Component by that path already exists");
                        var component = new EngineComponent(componentDetails);
                        var em = InternalManager.GetInstance();
                        var extensions = em.GetExtensions();
                        component.Load();
                        EngineComponent.RegisterComponent(component);
                        Debugger.LogDebug(string.Format("{0} Component loaded sucessfully, {1} APIs loaded, {2} Commands Loaded {3} Scripts Loaded", component.Name, component.GetAPIs().Count(), component.GetCommands().Count(), component.GetScripts().Count()));
                    }
                    catch (Exception e)
                    {
                        Debugger.LogDebug(e);
                    }
                }
            }
            Debugger.LogDebug(string.Format("Components initialized sucessfully, {0} Components loaded, {1} APIs loaded, {2} Commands Loaded", cm.GetComponents().Count(), am.GetAPIs().Count(), com.GetCommands().Count()));
            EngineStatus.IsComponentsInitialized = true;
        }


        private static void InitializeExtensions()
        {
            if (EngineStatus.IsExtensionsInitialized) return;
            var _extensionDetails = new List<ExtensionDetails>(EngineConfiguration.Extensions);
            var em = InternalManager.GetInstance();
            if (_extensionDetails != null)
            {
                foreach (var extensionPath in _extensionDetails)
                {
                    try
                    {
                        InitializeExtension(extensionPath);
                    }
                    catch (Exception e)
                    {
                        Debugger.LogDebug(e);
                    }
                }

            }
            EngineStatus.IsExtensionsInitialized = true;
        }

        internal static void InitializeExtension(ExtensionDetails extensionPath)
        {
            var assembly = Assembly.Load(extensionPath.Assembly);
            var im = InternalManager.GetInstance();
            foreach (var item in extensionPath.ExtensionClasses)
            {
                var types = assembly.GetType(item);
                var extension = (EngineExtension)Activator.CreateInstance(types, null);
                extension.OnInitialize();
                im.AddExtension(extension);
            }
        }

        private static void InitializeScripts()
        {
            if (EngineStatus.IsScriptsInitialized) return;
            //InsertScriptAssemblies();

            var spm = InternalManager.GetInstance();
            var sm = InternalManager.GetInstance();
            foreach (var item in EngineConfiguration.ScriptPackages)
            {
                try
                { 
                    var sp = ScriptPackage.Load(item);
                    if (sp == null) continue;
                    ScriptPackage.RegisterPackage(sp);
                    //Debugger.LogDebug("Successfully loaded script package");
                }
                catch (Exception e)
                {
                    Debugger.LogError(e.ToString(), true);
                    throw;
                }
            }
            Debugger.LogDebug(string.Format("Scripts initialized sucessfully, {0} Scripts loaded", sm.GetScriptCount()));
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
                    if(EngineConfiguration.IsClient && item.EnvType == 1)
                        Assembly.Load(item.Assembly);
                    else  if (!EngineConfiguration.IsClient && item.EnvType == -1)
                        Assembly.Load(item.Assembly);
                    else if(item.EnvType == 0)
                        Assembly.Load(item.Assembly);
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

        //public static ComponentAPI GetComponentAPI(string componentName)
        //{
        //    var cm = InternalManager.GetInstance();
        //    var component = cm.GetComponent(componentName);
        //    return component.GetAPI();
        //}

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
                case "Healthcheck":
                    return EngineStatus.IsEngineInitialized ? 1 : 0;
                case "ExecuteComponentControl":
                    var componentNAme = args[0].ToString();
                    var apiName = int.Parse(args[1].ToString());
                    var list = JsonConvert.DeserializeObject<object[]>(args[2].ToString());
                    return APICaller.CallAPI(apiName, list.ToArray());
                case "CreateAndInsertResponse":
                    var guid = (string)args[0];
                    var value = args[1];
                    var isException = (bool)args[2];
                    var nm = NetworkManager.GetInstance();
                    nm.CreateAndInsertResponse(guid, value, isException);
                    return null;
                        break;

                default:
                    return null;
            }
        }

        //public static object ExecuteComponentControl(string componentName, string control, object[] args)
        //{
        //   return ComponentAccess.ExecuteComponentControl(componentName, control, args);
        //}

        //public static void ExecuteComponentCommand(string componentName, string command, object[] args)
        //{
        //    ComponentAccess.ExecuteComponentCommand(componentName, command, args);
        //} 
    }
}
