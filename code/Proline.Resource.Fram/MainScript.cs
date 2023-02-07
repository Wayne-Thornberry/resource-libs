using CitizenFX.Core;
using Proline.ClassicOnline.EventQueue;
using Proline.Resource;
using Proline.Resource.Configuration;
using Proline.Resource.Framework;
using Proline.Resource.IO;
using Proline.Resource.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Console = Proline.Resource.Console;

namespace Proline.ClassicOnline.Resource
{
    public abstract class ResourceMainScript : ResourceScript
    {
        
        private List<ResourceCommand> _commands;
        private Dictionary<string, ComponentContainer> _components;

        public override async Task OnLoad()
        {
            try
            {
                Console.WriteLine("Loading Resources...");
                foreach (var item in Configuration.GetSection<string[]>("Resources"))
                {
                    Assembly.Load(item);
                }
                Console.WriteLine("Loaded Resources");

                Console.WriteLine($"Started Engine");
                try
                {
                    _components = Component.InitializeComponents();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                var assembly = Assembly.GetCallingAssembly();
                var types = assembly.GetTypes();
                var commandTypes = types.Where(e => (object)e.BaseType == typeof(ResourceCommand)).ToArray();

                foreach (var item in commandTypes)
                {
                    var command = (ResourceCommand)Activator.CreateInstance(item);
                    command.RegisterCommand();
                    _commands.Add(command);
                } 
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public override async Task OnStart()
        {
            try
            {
                Component.StartComponents(_components);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public override async Task OnUpdate()
        {
            try
            {

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        } 

    }
}
