using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ResourceFramework.Control
{
    public class ControllerMethodAttribute : Attribute
    {
        public ControllerMethodAttribute(string name, string type)
        {
            Name = name;
            Type = type;
        }
        public string Name { get; }
        public string Type { get; }
    }
}
