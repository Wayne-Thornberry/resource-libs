extern alias Client;
extern alias Server;

using Newtonsoft.Json;
using Proline.Engine.Data;
using Proline.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Proline.Engine.Networking;

namespace Proline.Engine
{
    public class EngineService : EngineObject
    {
        private CitizenResource _executingResource;
        private InternalManager _internalManager;
        private static CitizenAccess _scriptSource;
        private NetworkManager _nm;

        public static bool IsClient => EngineConfiguration.IsClient;

        public EngineService(IScriptSource scriptSource) : base("EngineService")
        {
            _internalManager = InternalManager.GetInstance();
            _scriptSource = new CitizenAccess(scriptSource);
            _nm = NetworkManager.GetInstance();
        }

        public async Task Start(params string[] args)
        {
            try
            {
                if (EngineStatus.IsEngineInitialized) throw new Exception("Cannot Initialize engine, engine already initilized");

                var resourceName = args[0];
                var sourceHandle = int.Parse(args[1]);
                var isDebug = bool.Parse(args[2]);
                var isIsolated = resourceName.Equals("ConsoleApp");
                EngineConfiguration.IsClient = sourceHandle != -1;
                EngineConfiguration.IsIsolated = isIsolated;
                EngineConfiguration.IsDebugEnabled = isDebug;
                EngineConfiguration.OwnerHandle = sourceHandle;
                EngineConfiguration.EnvTypeName = (EngineConfiguration.IsClient ? "Client" : "Server");
                LogDebug("Engine in " + (EngineConfiguration.IsClient ? "Client" : "Server") + " Mode");

                int status = 0;
                if (EngineConfiguration.IsClient && !EngineConfiguration.IsIsolated)
                {
                    var client = new NetClient();
                    status = await client.ExecuteEngineMethodServer<int>(EngineConstraints.HealthCheck);
                    if (status == 0)
                        throw new Exception("Cannot initialize client side, server side not healthy");
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

                RunEnvSpecificFunctions(sourceHandle);


                _scriptSource.AddTick(Update);
                EngineStatus.IsEngineInitialized = true;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public void PlayerConnectingHandler(object player, string playerName, dynamic setKickReason, dynamic deferrals)
        {
            //deferrals.defer();

            //// mandatory wait!
            //await Delay(0);

            //var licenseIdentifier = player.Identifiers["license"];

            ////_service.LogDebug(_header, $"A player with the name {playerName} (Identifier: [{licenseIdentifier}]) is connecting to the server.");

            //deferrals.update($"Hello {playerName}, your license [{licenseIdentifier}] is being checked");

            //// Check if the connecting client (Player) has a matching identifier with any identifier passed
            //// If a match is found, fetch the user identity and prepare to tie this player 
            //// If over 50% of the identiies match from this player and what we have in the DB, use the existing identity row
            //// Fetch the player profile if one exists
            //// Search the user table for any active bans
            //// If one exists, check if its a global ban, if true, defer connection on the bases of a global ban
            //// If false, check if the instance id matches the instance id stored in a data object here
            //// If the user has no bans, check if the player has a ban
            //// if true, check if its a global ban.... same as above
            //// If everything checks out, continue connection

            //// Priority, if the server has a queue enabled, prepare to do some queue stuff
            //// This involves looking at the user and player priority.
            //// On a per instance level, instances have a table for player priority, while users have a priority Id
            //// We get both user and player priorty (if one exists) and take the highest of the two numbers 
            //// Thats your priority into the queue, the higher the number, the better the postition in queue

            //if (true)
            //{
            //    //deferrals.done("Rejected..");
            //}

            //deferrals.done();
        }

        public void PlayerDroppedHandler(object player, string reason)
        {
            throw new NotImplementedException();
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

                var queue = _nm.GetEnqueuedRequests();
                while (queue.Count > 0)
                {
                    var request = queue.Dequeue();
                    var guid = request.Header.Guid;
                    object result = null;
                    bool isException = false;
                    try
                    {
                        result = ExecuteEngineMethod(request.Call.MethodName, request.Call.MethodArgs);
                        if (result != null)
                        {
                            if (!result.GetType().IsPrimitive)
                            {
                                result = JsonConvert.SerializeObject(result);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        isException = true;
                        throw;
                    }
                    finally
                    {
                        /// THIS NEEDS TO BE HERE, OTHERWISE IT WILL CAUSE A CIRCLAR DEPENENCY OF THE CLIENT CALLING THE SERVER AND THE SERVER CALLING THE CLIENT
                        if (request.Header.PlayerId != -1)
                        {
                            var response = new object[] { result, isException };
                            var data = JsonConvert.SerializeObject(response);
                            // LogDebug(guid + methodName + argData + data + playerId);
                            if (!EngineConfiguration.IsClient)
                            { 
                                Server.CitizenFX.Core.BaseScript.TriggerClientEvent(EngineConstraints.ExecuteEngineMethodHandler, EngineConfiguration.OwnerHandle, guid, EngineConstraints.CreateAndInsertResponse, data);
                            }
                            else if (EngineConfiguration.IsClient)
                                Server.CitizenFX.Core.BaseScript.TriggerEvent(EngineConstraints.ExecuteEngineMethodHandler, EngineConfiguration.OwnerHandle, guid, EngineConstraints.CreateAndInsertResponse, data);
                        }
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
                if (!EngineConfiguration.IsDebugEnabled && componentDetails.DebugOnly) throw new Exception("Component cannot be started, debug not enabled");
                if (componentDetails == null) throw new Exception("Component path null");
                try
                {
                    var component = EngineComponent.Load(componentDetails);
                    if (component == null) continue;
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

        public void StartScript(string scriptName)
        {
            Script.StartScript(new StartScriptRequest(scriptName, null));
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

        private object ExecuteEngineMethod(string methodName, params object[] args)
        {
            //LogDebug("Called Engine event " + methodName);
            var im = InternalManager.GetInstance();
            switch (methodName)
            {
                case EngineConstraints.Log:
                    var type = long.Parse(args[0].ToString());
                    var data = args[1].ToString();
                    switch (type)
                    {
                        case 0: LogDebug(data); break;
                        case 1: LogWarn(data); break;
                        case 2: LogError(data); break;
                    }
                    return null;
                case EngineConstraints.HealthCheck:
                    return EngineStatus.IsEngineInitialized ? 1 : 0;
                case EngineConstraints.ExecuteAPI:
                    var apiName = int.Parse(args[0].ToString());
                    var list = JsonConvert.DeserializeObject<object[]>(args[1].ToString());
                    return ComponentAPI.CallAPI(apiName, list.ToArray());
                case EngineConstraints.PullHandler:
                    var cn = args[0].ToString();
                    var c = im.GetComponent(cn);
                    return c.PullData();
                case EngineConstraints.PushHandler:
                    var compeonentName = args[0].ToString();
                    var data2 = args[1].ToString();
                    var component = im.GetComponent(compeonentName);
                    component.PushData(data2);
                    break;
                default:
                    return null;
            }
            return null;
        }

        public void RequestResponseHandler(int playerId, string guid, string methodName, string argData)
        {
            var args = JsonConvert.DeserializeObject<object[]>(argData);
            var nm = NetworkManager.GetInstance();
            if (!methodName.Equals(EngineConstraints.CreateAndInsertResponse))
            {
                nm.CreateAndInsertRequest(guid, playerId, methodName, args); 
            }
            else
            { 
                var value = args[0];
                var isException = bool.Parse(args[1].ToString());
                nm.CreateAndInsertResponse(guid, value, isException);
            }
        }

        public static IScriptSource GetInstance()
        {
            return _scriptSource;
        } 
    }
}
