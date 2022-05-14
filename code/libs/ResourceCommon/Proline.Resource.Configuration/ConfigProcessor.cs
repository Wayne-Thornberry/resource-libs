using Proline.Resource.File;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Resource.Configuration
{
    public class ConfigProcessor
    {
        public ConfigFile TransformFileIntoConfig(IResourceFile file)
        {
            var cf = new ConfigFile(file);
            cf.LoadConfig();
            return cf;
        }
    }
}
