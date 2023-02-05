using System;
using System.Reflection;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.EventQueue
{
    public class ComponentScript
    {
        public bool IsActive => _executionTask == null ? false : !_executionTask.IsCompleted;
        private const int TIMETILLNEXTRESCHEDUAL = 1000000;
        private bool _isLoaded;
        public string Name { get; private set; }
        private object _script;
        private MethodInfo _execute;
        private Task _executionTask;
        private long _nextSchedualedTicks;

        private ComponentScript(object instance)
        {
            _nextSchedualedTicks = 0;
            _script = instance;
        }

        internal static ComponentScript Load(object instance)
        {
            var script = new ComponentScript(instance);
            var scriptType = instance.GetType();
            script.Name = scriptType.Name;
            script._execute = scriptType.GetMethod("Execute");
            script._isLoaded = true;
            return script;
        }

        internal int Execute()
        {
            if (!_isLoaded)
                throw new Exception("Cannot execute script, script has not loaded");

            if (IsActive)
                return -1;

            if (DateTime.UtcNow.Ticks > _nextSchedualedTicks)
            {
                Console.WriteLine(string.Format("{0} Invoking Execute", Name));
                _executionTask = (Task)_execute.Invoke(_script, null);
                _nextSchedualedTicks = DateTime.UtcNow.Ticks + TIMETILLNEXTRESCHEDUAL;
            }
            return 0;
        }
    }
}
