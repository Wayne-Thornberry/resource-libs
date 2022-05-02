using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Resource.Framework
{
    public class ResourceExportAttribute : Attribute
    {
        public ResourceExportAttribute(string name, string type)
        {
            Name = name;
            Type = type;
        }
        public string Name { get; }
        public string Type { get; }
    }
}
