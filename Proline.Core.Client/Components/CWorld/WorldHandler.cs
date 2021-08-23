

using CitizenFX.Core;
using CitizenFX.Core.Native;
using Proline.Core.Client;
using Proline.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Core.Client.Components.CWorld
{
    public class WorldHandler : ComponentHandler
    {
        public override void OnComponentInitialized()
        {
            Console.WriteLine("World Component Initialized");
        }

        public override void OnComponentStart()
        {
            Console.WriteLine("World Component Started");
        }

       
    }
}
