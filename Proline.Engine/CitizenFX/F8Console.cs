using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Engine
{
    internal static class F8Console
    {
        public static void Write(object obj)
        {
            var ca = EngineService.GetInstance();
            ca.Write(obj);
        }

        public static void WriteLine(object obj)
        { 
            var ca = EngineService.GetInstance();
            ca.WriteLine(obj);
        }

        // Need to come back to this
        //public static void RegisterCommnad(string command, Func<void> function)
        //{

        //}

        public static void ExecuteCommand(string command, params object[] args)
        {

        }
    }
}
