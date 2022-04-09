using CitizenFX.Core.Native;
using Proline.ResourceFramework.Common; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ResourceFramework.APIs
{
    public class ResourceAPI : IFiveAPI, IResourceMethods
    { 
        public string LoadResourceFile(string resourceName, string fileName)
        {
            return API.LoadResourceFile(resourceName, fileName);
        }

        public string GetCurrentResourceName()
        {
            return API.GetCurrentResourceName();
        }
    }
}
