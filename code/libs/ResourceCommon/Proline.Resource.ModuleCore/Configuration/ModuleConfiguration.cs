using Proline.Resource.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Resource.ModuleCore.Configuration
{
    public class ModuleConfiguration : ConfigSection
    {
        public string[] Modules => GetConfigSection<string[]>("Modules");


    }
}
