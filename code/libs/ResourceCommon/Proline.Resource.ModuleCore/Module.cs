using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Resource.ModuleCore
{
    public class Module
    {
        public ModuleContext Context { get; set; }
        public Dictionary<string, object> Data { get; set; }
        public AssemblyName Name { get; set; }
        public List<IModuleScript> BaseScripts { get; set; }
    }
}
