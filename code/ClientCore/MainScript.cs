using CitizenFX.Core; 
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
    public class ResourceMainScript : ResourceScript
    {
        private MethodInfo _method;

        public ResourceMainScript()
        { 

        }

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

                var run = ResourceFile.Load("run.txt");
                var assembly = Assembly.Load(run.Load());
                if(assembly == null)
                    Console.WriteLine("Failed to load assembly"); 
                var program = assembly.GetType("Program");
                if (program == null)
                { 
                    Console.WriteLine("Failed to load program file");
                    foreach (var type in assembly.GetTypes())
                    {
                        if (type.Name.Equals("Program"))
                            program = type;
                        Console.WriteLine(type.Name);
                    }
                }
                _method = program.GetMethod("Main", BindingFlags.Static | BindingFlags.NonPublic);
                foreach (var type in program.GetMethods())
                {
                    if (type.Name.Equals("Main"))
                        _method = type;
                    Console.WriteLine(type.Name);
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
                _method.Invoke(null, new object[] {null});
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
