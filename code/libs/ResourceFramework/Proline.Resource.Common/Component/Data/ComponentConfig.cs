using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Resource.Common.Component
{
    public class ComponentConfig
    {
        public string Name { get; set; }
        public int EnvType { get; set; }
        public string[] Assemblies { get; set; }
        public ComponentConfigServer Server { get; set; }
        public ComponentConfigClient Client { get; set; }
    }
}
