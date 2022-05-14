using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Resource.ModuleCore
{
    public class ModuleConfig
    {
        public string Name { get; set; }
        public string Assembly { get; set; }
        public Dictionary<string, object> Data { get; set; }
    }
}
