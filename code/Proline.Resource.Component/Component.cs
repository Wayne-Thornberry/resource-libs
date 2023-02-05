using Newtonsoft.Json;
using Proline.Resource.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.EventQueue
{
    public static class Component
    {

        public static void InitializeComponents()
        {
            var _components = new Dictionary<string, ComponentContainer>();
            // Load the components
            var componentJson = ResourceFile.Load("components.json");
            var components = JsonConvert.DeserializeObject<string[]>(componentJson.Load());

            foreach (var item in components)
            {
                Console.WriteLine($"Loading {item}");
                var container = ComponentContainer.Load(item);
                Console.WriteLine($"Succesfully Loaded {container.Name}");
                container.RegisterCommands();
                _components.Add(container.Name, container);
            }

            // Init_Core
            // - Finds all scripts that are marked InitializeCore
            // - Execute Core Initializations
            foreach (var container in _components.Values)
            {
                Console.WriteLine(string.Format("Invoking {0} {1}", container.Name, ComponentContainer.INITCORESCRIPTNAME));
                try
                {
                    container.ExecuteScript(ComponentContainer.INITCORESCRIPTNAME);
                }
                catch (ComponentScriptDoesNotExistException e)
                {

                }
                catch (Exception e)
                {
                    throw;
                }
            }

            // Init_Session
            // - Find all scripts that are marked InitializeSession
            // - Execute Session Intializations
            foreach (var container in _components.Values)
            {
                try
                {
                    Console.WriteLine(string.Format("Invoking {0} {1}", container.Name, ComponentContainer.INITSESSIONSCRIPTNAME));
                    try
                    {
                        container.ExecuteScript(ComponentContainer.INITSESSIONSCRIPTNAME);
                    }
                    catch (ComponentScriptDoesNotExistException e)
                    {

                    }
                    catch (Exception e)
                    {
                        throw;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }
    }
}
