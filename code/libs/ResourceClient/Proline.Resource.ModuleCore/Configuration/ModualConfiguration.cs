using Newtonsoft.Json;
using Proline.ResourceConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Resource.ModuleCore.Configuration
{
    internal class ModualConfiguration : ConfigSection
    {
        public string[] Modules => GetConfigSection<string[]>("Modules");


    }
}
