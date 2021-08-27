using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Proline.Engine
{
    public interface IScriptSource
    {
        void RemoveTick(Func<Task> task);
        void AddTick(Func<Task> task);
        object GetGlobal(string key);
        void SetGlobal(string key, object data, bool replicated);
    }
}