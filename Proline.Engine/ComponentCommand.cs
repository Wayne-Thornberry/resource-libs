using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Engine
{
    internal class ComponentCommand
    {
        private object _source;
        private MethodInfo _item;

        public ComponentCommand(object source, MethodInfo item)
        {
            _source = source;
            _item = item;
            Name = item.Name;
            Type = "Command";
        }

        public string Name { get; internal set; }
        public string Type { get; internal set; }

        internal void Invoke(params object[] args)
        {

        }
    }
}
