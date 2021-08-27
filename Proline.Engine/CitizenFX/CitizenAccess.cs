
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Engine
{
    public class CitizenAccess : IScriptSource
    {
        private IScriptSource _source;
        internal CitizenAccess(IScriptSource source)
        {
            _source = source;
        }

        public void RemoveTick(Func<Task> task)
        {
            _source.RemoveTick(task);
        }

        public void AddTick(Func<Task> task)
        {
            _source.AddTick(task);
        }

        public object GetGlobal(string key)
        {
            return _source.GetGlobal(key);
        }

        public void SetGlobal(string key, object data, bool replicated)
        {
            _source.SetGlobal(key, data, replicated);
        }
    }
}
