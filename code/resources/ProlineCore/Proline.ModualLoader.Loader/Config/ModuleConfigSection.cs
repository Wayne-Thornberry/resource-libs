using Proline.Resource.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.Resource.Config
{
    public class ModuleConfigSection : ConfigSection
    {
        public static ModuleConfigSection ModuleConfig => Configuration.GetSection<ModuleConfigSection>("moduleConfigSection");

        public ModuleInstanceCollection Modules => GetConfigSection<ModuleInstanceCollection>("moduleCollection");


    }
}
