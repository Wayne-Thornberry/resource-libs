using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Resource.Framework
{
    public class ResourceExportsAttribute : Attribute
    {
    }
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
    public class ResourceEventsAttribute : Attribute
    {
    }
    public class ResourceEventAttribute : Attribute
    {
        public ResourceEventAttribute(string name, string type)
        {
            Name = name;
            Type = type;
        }
        public string Name { get; }
        public string Type { get; }
    }
}
