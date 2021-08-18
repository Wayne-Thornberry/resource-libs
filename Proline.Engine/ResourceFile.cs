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
            var ca = CitizenAccess.GetInstance();
            return ca.LoadResourceFile(resourceName, filePath);
        }

        public static string Load(CitizenResource resource, string filePath)
        {
            return Load(resource.Name, filePath);
        }
    }
}
