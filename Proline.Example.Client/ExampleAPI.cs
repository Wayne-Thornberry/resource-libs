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
        public static void UnlockNeareastVehicle(int x, out int y)
        {
            var args = new object[] { x, null };
            APICaller.CallAPI("UnlockNeareastVehicle", args);
            y = (int)args[1];
        }
    }
}
