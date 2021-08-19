using Proline.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Example.Client
{
    public static class ExampleAPI
    {
        public static bool UnlockNeareastVehicle(int x, out int y)
        {
            var args = new object[] { x, null };
            var result = (bool) APICaller.CallAPI("UnlockNeareastVehicle", args);
            y = (int)args[1];
            return result;
        }
    }
}
