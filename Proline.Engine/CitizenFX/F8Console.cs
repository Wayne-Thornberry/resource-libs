extern alias Client;
extern alias Server;

using Client.CitizenFX.Core;
using Server.CitizenFX.Core;

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
            if (EngineConfiguration.IsClient && !EngineConfiguration.IsIsolated)
            {
                Client.CitizenFX.Core.Debug.Write(obj.ToString());
            }
            else if(!EngineConfiguration.IsClient && !EngineConfiguration.IsIsolated)
            {
                Server.CitizenFX.Core.Debug.Write(obj.ToString());
            }else
            {
                Console.Write(obj);
            }
        }

        public static void WriteLine(object obj)
        {
            if (EngineConfiguration.IsClient && !EngineConfiguration.IsIsolated)
            {
                Client.CitizenFX.Core.Debug.WriteLine(obj.ToString());
            }
            else if (!EngineConfiguration.IsClient && !EngineConfiguration.IsIsolated)
            {
                Server.CitizenFX.Core.Debug.WriteLine(obj.ToString());
            }else
            { 
                Console.WriteLine(obj);
            }
        } 
    }
}
