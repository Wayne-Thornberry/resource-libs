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
    public class EngineService : EngineObject
    {
        private CitizenResource _executingResource;
        private InternalManager _internalManager;
        private static CitizenAccess _scriptSource;

        public EngineService(IScriptSource scriptSource) : base("EngineService")
        {
            _internalManager = InternalManager.GetInstance();
            _scriptSource = new CitizenAccess(scriptSource); 
        }

        public async Task Start(params string[] args)
        {
            try
            {
                await Delay(2000);
                if (EngineStatus.IsEngineInitialized) throw new Exception("Cannot Initialize engine, engine already initilized");

                var resourceName = args[0];
                var envType = int.Parse(args[1]);
                var isDebug = bool.Parse(args[2]);
                var isIsolated = resourceName.Equals("ConsoleApp");
                EngineConfiguration.IsClient = envType != -1;
                EngineConfiguration.IsIsolated = isIsolated;
                EngineConfiguration.IsDebugEnabled = isDebug;
                LogDebug("Engine in " + (EngineConfiguration.IsClient ? "Client" : "Server") + " Mode");

                int status = 0;
                if (EngineConfiguration.IsClient && !EngineConfiguration.IsIsolated)
                {
                    status = 1;
                    //status = await ExecuteEngineMethodServer<int>(EngineConstraints.HealthCheck);
                    //if (status == 0)
                    //    throw new Exception("Cannot initialize client side, server side not healthy");
                }
                else
                {
                    // This part should try and communicate with the central server
                    status = 1;
                }

                SetupResource(resourceName);

                LoadConfig(isDebug);
                LoadAssemblies();
                LoadExtensions();
                LoadComponents();
                LoadScripts();

                InitializeExtensions();
                InitializeComponents();

                StartAllComponents();
                StartStartupScripts();

                RunEnvSpecificFunctions(envType);


                EngineStatus.IsEngineInitialized = true;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task Update()
        {
            try
            {
                while (!_internalManager.IsScriptRequestsQueueEmpty())
                {
                    var requests = _internalManager.DequeueStartScriptRequest();
                    Script.StartScript(requests);
                }

               
                while (!_internalManager.IsComponentEventQueueEmpty())
                {
                    var events = _internalManager.DequeueComponentEvent();
                    foreach (var item in _internalManager.GetComponents())
                    {
                        item.InvokeComponentEvent(events);
                    }
                }
            }
            catch (Exception e)
            {
                LogError(e);
                throw e;
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

        private void InitializeComponents()
        {
            if (EngineStatus.IsComponentsInitialized) return;
            var _componentDetails = new List<ComponentDetails>(EngineConfiguration.Components);
            var am = InternalManager.GetInstance();
            if (_componentDetails != null)
            { 
                foreach (var item in am.GetComponents())
                {
                    item.Initalize();
                }
            } 
            LogDebug(string.Format("Components initialized sucessfully, {0} Components loaded, {1} APIs loaded, {2} Commands Loaded", am.GetComponents().Count(), am.GetAPIs().Count(), am.GetCommands().Count()));
            EngineStatus.IsComponentsInitialized = true;
        }

        private void LoadComponents()
        {
            var _componentDetails = new List<ComponentDetails>(EngineConfiguration.Components);
            var am = InternalManager.GetInstance();
            var extensions = am.GetExtensions();
            foreach (var componentDetails in _componentDetails)
            {
                if ((!EngineConfiguration.IsClient && componentDetails.EnvType == 1) || (EngineConfiguration.IsClient && componentDetails.EnvType == -1))continue; 
                if (!EngineConfiguration.IsDebugEnabled && componentDetails.DebugOnly) throw new Exception("Component cannot be started, debug not enabled");
                if (componentDetails == null) throw new Exception("Component path null");
                try
                {
                    var component = EngineComponent.Load(componentDetails);
                    EngineComponent.RegisterComponent(component);
                }
                catch (Exception e)
                {
                    LogDebug(e);
                }
            }
        }

        private void LoadExtensions()
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
                        LoadExtension(extensionPath);
                    }
                    catch (Exception e)
                    {
                        LogDebug(e);
                    }
                }

            }
            EngineStatus.IsExtensionsInitialized = true;
        }

        internal void LoadExtension(ExtensionDetails extensionPath)
        {
            var assembly = Assembly.Load(extensionPath.Assembly);
            var im = InternalManager.GetInstance();
            foreach (var item in extensionPath.ExtensionClasses)
            {
                var types = assembly.GetType(item);
                var extension = (EngineExtension)Activator.CreateInstance(types, null);
                //extension.OnInitialize();
                im.AddExtension(extension);
            }
        }

        private void InitializeExtensions()
        {
            var im = InternalManager.GetInstance();
            foreach (var extension in im.GetExtensions())
            {
                extension.Initialize();
            }
        }

        private void LoadScripts()
        {
            if (EngineStatus.IsScriptsInitialized) return;
            //InsertScriptAssemblies(); 
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
                    LogError(e.ToString());
                    throw;
                }
            }
            LogDebug(string.Format("Scripts initialized sucessfully, {0} Scripts loaded", sm.GetScriptCount()));
            EngineStatus.IsScriptsInitialized = true;
        }

        public void StartStartupScripts()
        {
            foreach (var item in EngineConfiguration.StartupScripts)
            { 
                Script.StartScript(new StartScriptRequest(item, null));
            }
        }

        public void StartAllComponents()
        {
            EngineComponent.StartAllComponents();
        }

        public void StopAllComponents()
        {
            EngineComponent.StopAllComponents();
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
                    LogError(e.ToString()); 
                }
            }
        }

        public object ExecuteEngineMethod(string methodName, params object[] args)
        {
            //LogDebug("Called Engine event " + methodName);
            //switch (methodName)
            //{
            //    case EngineConstraints.LogDebug: 
            //        LogDebug("[Client]" + args[0]);
            //        return null;
            //        break;
            //    case EngineConstraints.LogError:
            //        LogError("[Client]" + args[0]);
            //        return null;
            //        break;
            //    case EngineConstraints.LogWarn:
            //        LogWarn("[Client]" + args[0]);
            //        return null;
            //        break;
            //    case EngineConstraints.HealthCheck:
            //        return EngineStatus.IsEngineInitialized ? 1 : 0;
            //    case EngineConstraints.ExecuteComponentAPI:
            //        var componentNAme = args[0].ToString();
            //        var apiName = int.Parse(args[1].ToString());
            //        var list = JsonConvert.DeserializeObject<object[]>(args[2].ToString());
            //        return APICaller.CallAPI(apiName, list.ToArray());
            //    case EngineConstraints.CreateAndInsertResponse:
            //        var guid = (string)args[0];
            //        var value = args[1];
            //        var isException = (bool)args[2];
            //        var nm = NetworkManager.GetInstance();
            //        nm.CreateAndInsertResponse(guid, value, isException);
            //        return null;
            //            break;

            //    default:
            //        return null;
            //}
            return null;
        }
        public static IScriptSource GetInstance()
        {
            return _scriptSource;
        } 
    }
}
