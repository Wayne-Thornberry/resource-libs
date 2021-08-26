using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Engine
{
    internal class ComponentCommand :EngineObject
    {
        private object _source;
        private MethodInfo _item;
        private bool _debugOnly;
        private int _type;

        public ComponentCommand(object source, MethodInfo item, int type = 0, bool debugOnly = false) : base("Command")
        {
            _source = source;
            _item = item;
            Name = item.Name;
            _debugOnly = debugOnly;
            _type = type;
        }

        public string Name { get; internal set; }

        internal void Invoke(params object[] args)
        {
            _item.Invoke(_source, args);
        }

        internal static void RegisterCommand(ComponentCommand command)
        {
            var im = InternalManager.GetInstance();
            im.AddCommand(command);
        }
        internal static void UnregisterCommand(ComponentCommand command)
        {
            var im = InternalManager.GetInstance();
            im.RemoveCommand(command);
        }
    }
}
