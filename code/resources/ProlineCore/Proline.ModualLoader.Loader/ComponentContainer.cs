using CitizenFX.Core;
using Proline.ClassicOnline.Resource.Config;
using Proline.Resource.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Console = Proline.Resource.Console;

namespace Proline.ClassicOnline.Resource
{

    public class ComponentContainer
    {
        public const string INITCORESCRIPTNAME = "InitCore";
        public const string INITSESSIONSCRIPTNAME = "InitSession";
        public Assembly _assembly;
        private bool _hasLoaded;

        public ComponentContainer(Assembly assembly)
        {
            _assembly = assembly;
        }
         
        public bool Enabled { get; set; }
        public List<ResourceCommand> Commands { get; set; }
        public Dictionary<string, ComponentScript> Scripts { get; internal set; }
        public string Name { get; private set; }
        public bool HasStarted { get; internal set; }
        public bool IsScriptsFinished => Scripts.Values.Where(e => e.IsActive).Count() == 0;

        internal static ComponentContainer Load(string assemblyString)
        {

            var assembly = Assembly.Load(assemblyString);
            var config = ModuleConfigSection.ModuleConfig;
            var componentContainer = new ComponentContainer(assembly);
            componentContainer.Scripts = new Dictionary<string, ComponentScript>();
            componentContainer.Commands = new List<ResourceCommand>();
            var types = assembly.GetTypes();
            var scriptTypes = types.Where(e => e.GetMethod("Execute") != null).ToArray();
            var commandTypes = types.Where(e => e.BaseType == typeof(ResourceCommand)).ToArray();

            componentContainer.Name = assembly.GetName().Name;
            Console.WriteLine($"Getting object from assembly {componentContainer.Name}"); 
            foreach (var item in scriptTypes)
            {
                var obj = Activator.CreateInstance(item);
                var script = ComponentScript.Load(obj);
                componentContainer.Scripts.Add(script.Name, script);
            }

            foreach (var item in commandTypes)
            {
                var command = (ResourceCommand)Activator.CreateInstance(item);
                componentContainer.Commands.Add(command);
            }
            componentContainer._hasLoaded = true;
            return componentContainer;
        }

        internal async Task Run()
        {
            try
            {
                if (!_hasLoaded)
                    throw new Exception("Component cannot run, component has not loaded");
                var runningScripts = Scripts.Values.Where(e => !e.Name.Equals(INITCORESCRIPTNAME) && !e.Name.Equals(INITSESSIONSCRIPTNAME));
                Console.WriteLine(String.Format("Running {0} Tasks {1}", Name, runningScripts.Count()));
                while (Enabled)
                { 
                    foreach (var script in runningScripts)
                    {
                        script.Execute();
                    }
                    await BaseScript.Delay(0);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        internal void Enable()
        { 
            Enabled = true;
        }

        internal void Disable()
        {
            Enabled = false;
        }

        internal void Start()
        {
            try
            {

                if (!_hasLoaded && !HasStarted)
                    throw new Exception("Component cannot run, component has not loaded or has already started");
                ExecuteScript(ComponentContainer.INITSESSIONSCRIPTNAME);
            }
            catch (ScriptDoesNotExistException e)
            {
                // if it fails to start the session start script then its fine, components can do without start or core 
            }
            catch (Exception e) 
            { 
                Console.WriteLine(e);
            } 
            HasStarted = true;
        }

        internal void RegisterCommands()
        { 
            foreach (var item in Commands)
            {
                item.RegisterCommand();
            };
        }

        internal void ExecuteScript(string scriptName)
        {
            if (!Scripts.ContainsKey(scriptName))
                throw new ScriptDoesNotExistException($"{scriptName} does not exist, cannot execute");
            var script = Scripts[scriptName];
            script.Execute();
        } 
         
    }
}
