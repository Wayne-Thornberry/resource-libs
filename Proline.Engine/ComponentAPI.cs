using System;
using System.Reflection;

namespace Proline.Engine
{
    internal class ComponentAPI
    {
        private object _source;
        private MethodInfo _item;
        private bool _debugOnly;

        public ComponentAPI(object source, MethodInfo item, bool debugOnly = false)
        {
            _source = source;
            _item = item;
            _debugOnly = debugOnly;
            Type = "API";
        }

        public string Name => _item.Name;

        public string Type { get; private set; }

        internal object Invoke(params object[] args)
        {
            if (_debugOnly && !EngineConfiguration.IsDebugEnabled) throw new Exception("Debug mode not enabled on debug only API");
           return _item.Invoke(_source, args);
        }
    }
}