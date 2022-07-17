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
        private long _nextSchedualedTicks;

        public ComponentScript(object instance)
        {
            _nextSchedualedTicks = 0;
            _script = instance;
        }

        internal void Load()
        {
            if (_isLoaded)
                throw new Exception("Cannot load script, script has already loaded");
            var scriptType = _script.GetType();
            Name = scriptType.Name;
            _execute  = scriptType.GetMethod("Execute");
            _isLoaded = true;
        }

        internal void Execute()
        {
            if(!_isLoaded)
                throw new Exception("Cannot execute script, script has not loaded");

            if (DateTime.UtcNow.Ticks > _nextSchedualedTicks)
            {
                _execute.Invoke(_script, null);
                _nextSchedualedTicks = DateTime.UtcNow.Ticks + 10000000; 
            }
        }
    }
}
