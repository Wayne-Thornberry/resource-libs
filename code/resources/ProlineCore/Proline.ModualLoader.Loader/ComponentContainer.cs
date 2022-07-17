using Proline.Resource.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
         
        public List<ResourceCommand> Commands { get; set; }
        public Dictionary<string, ComponentScript> Scripts { get; internal set; }
        public string Name { get; private set; }
        public bool HasStarted { get; internal set; }

        internal void Load()
        {
            Scripts = new Dictionary<string, ComponentScript>();
            Commands = new List<ResourceCommand>();
            var types = _assembly.GetTypes();
            var scriptTypes = types.Where(e => e.GetMethod("Execute") != null).ToArray();
            var commandTypes = types.Where(e => e.BaseType == typeof(ResourceCommand)).ToArray();

            Name = _assembly.FullName;
            OutputToConsole($"Getting object from assembly {Name}");
            var scripts = new List<ComponentScript>();
            var commands = new List<ResourceCommand>();
            foreach (var item in scriptTypes)
            {
                var obj = Activator.CreateInstance(item);
                var script = new ComponentScript(obj);
                script.Load();
                Scripts.Add(script.Name, script);
            }

            foreach (var item in commandTypes)
            {
                var command = (ResourceCommand)Activator.CreateInstance(item);
                Commands.Add(command);
            }
            _hasLoaded = true;
        }

        internal void Run()
        {
            if (!_hasLoaded)
                throw new Exception("Component cannot run, component has not loaded");
            foreach (var script in Scripts.Values)
            {
                if (script.Name.Equals(INITCORESCRIPTNAME) || script.Name.Equals(INITSESSIONSCRIPTNAME)) continue;
                script.Execute();
            }
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
            var script = Scripts[scriptName];
            if (script == null)
                throw new Exception($"{scriptName} does not exist, cannot execute");
            script.Execute();
        } 

        private void OutputToConsole(string data)
        {
            Console.WriteLine(data);
        }
    }
}
