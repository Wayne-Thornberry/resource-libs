using Proline.ModuleFramework.Core;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace Proline.ModuleFramework.TestModule
{
    public class TestModuleScript : ModuleScript
    {
        public TestModuleScript(Assembly source) : base(source)
        {

        }

        public override async Task OnStart()
        { 
            Console.WriteLine(ModuleManager.GetCurrentModuleName());
        }

        public override async Task OnUpdate()
        {
            //Console.WriteLine("Updating...");
        }
    }
}
