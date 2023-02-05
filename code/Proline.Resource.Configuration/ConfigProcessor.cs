
using Proline.Resource.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Resource.Configuration
{
    public class ConfigProcessor
    {
        public ConfigFile TransformFileIntoConfig(ResourceFile file)
        {
            var cf = new ConfigFile(file.Resource, file.Path);
            cf.LoadConfig();
            return cf;
        }
    }
}
