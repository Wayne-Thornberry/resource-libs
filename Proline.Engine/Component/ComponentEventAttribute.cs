using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Engine
{
    public class ComponentEventAttribute : Attribute
    {
        private string _name;

        public ComponentEventAttribute(string name)
        {
            _name = name;
        }
    }
}
