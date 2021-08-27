extern alias Server;
extern alias Client;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Engine
{
    public static class ResourceFile
    {
        public static string Load(string resourceName, string filePath)
        {
            var configJson = "";
            if (EngineConfiguration.IsClient)
            {
                resourceName = Client.CitizenFX.Core.Native.API.GetCurrentResourceName();
                configJson = Client.CitizenFX.Core.Native.API.LoadResourceFile(resourceName, filePath);
            }
            else
            {
                resourceName = Server.CitizenFX.Core.Native.API.GetCurrentResourceName();
                configJson = Server.CitizenFX.Core.Native.API.LoadResourceFile(resourceName, filePath);
            }

            return configJson;
        }

        public static string Load(CitizenResource resource, string filePath)
        {
            return Load(resource.Name, filePath);
        }
    }
}
