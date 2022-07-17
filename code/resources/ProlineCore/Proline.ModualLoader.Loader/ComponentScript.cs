using System;
using System.Reflection;

namespace Proline.ClassicOnline.Resource
{
    public class ComponentScript
    {
        private bool _isLoaded;
        public string Name { get; private set; }
        private object _script;
        private MethodInfo _execute;

        public ComponentScript(object instance)
        {
            _script = instance;
        }

        internal void Load()
        {
            var scriptType = _script.GetType();
            Name = scriptType.Name;
            _execute  = scriptType.GetMethod("Execute");
            _isLoaded = true;
        }

        internal void Execute()
        {
            if (_isLoaded)
                _execute.Invoke(_script, null);
            else
                throw new Exception("Cannot execute script, script has not loaded");
        }
    }
}
