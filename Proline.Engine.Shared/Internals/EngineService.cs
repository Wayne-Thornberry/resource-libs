using System;
using System.Collections.Generic;
using System.Reflection;
using Proline.Engine.Componentry;
using Proline.Engine.Data;
using Proline.Engine.Extension;
using Proline.Engine.Networking;
using Proline.Engine.Scripting;

namespace Proline.Engine.Internals
{
    public abstract class EngineService
    {
        //protected CitizenResource _executingResource;
        private InternalManager _internalManager;
        //private static CitizenAccess _scriptSource;
        private NetworkManager _nm;

        protected EngineService()
        { 
            _internalManager = InternalManager.GetInstance();
            //_scriptSource = new CitizenAccess(scriptSource);
            _nm = NetworkManager.GetInstance();
        }

        protected void Initialize()
        {
            try
            {
                if (EngineStatus.IsEngineInitialized) throw new Exception("Cannot Initialize engine, engine already initilized");

             
                //Debugger.LogDebug("Engine in " + (EngineConfiguration.IsClient ? "Client" : "Server") + " Mode");

                int status = 0;

                //await ExecuteEnvFunctions();


                LoadConfig(EngineConfiguration.IsDebugEnabled);
                var componentLoader = new ComponentLoader();
                var scriptLoader = new ScriptLoader();
                LoadAssemblies();
                LoadExtensions();
                componentLoader.LoadComponents();
                scriptLoader.LoadScripts();

                InitializeExtensions();
                InitializeComponents();

                Component.StartAllComponents();
                StartStartupScripts();

                //RunEnvSpecificFunctions(sourceHandle);



                EngineStatus.IsEngineInitialized = true;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        protected abstract void LoadConfig(bool isDebug);

        private void LoadAssemblies()
        {
            foreach (var item in EngineConfiguration.Assemblies)
            {
                try
                {
                    if (EngineConfiguration.IsClient && item.EnvType == 1)
                        Assembly.Load(item.Assembly);
                    else if (!EngineConfiguration.IsClient && item.EnvType == -1)
                        Assembly.Load(item.Assembly);
                    else if (item.EnvType == 0)
                        Assembly.Load(item.Assembly);
                }
                catch (Exception e)
                {
                   // Debugger.LogError(e.ToString());
                }
            }
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
            //Debugger.LogDebug(string.Format("Components initialized sucessfully, {0} Components loaded, {1} APIs loaded, {2} Commands Loaded", am.GetComponents().Count(), am.GetAPIs().Count(), am.GetCommands().Count()));
            EngineStatus.IsComponentsInitialized = true;
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
                        //Debugger.LogDebug(e);
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
        private static void StartStartupScripts()
        {
            foreach (var item in EngineConfiguration.StartupScripts)
            {
                Script.StartScript(item);
            }
        }
    }
}