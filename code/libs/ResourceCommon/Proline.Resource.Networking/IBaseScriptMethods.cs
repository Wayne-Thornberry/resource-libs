using System;
using System.Threading.Tasks;

namespace Proline.Resource.Networking
{
    public interface IBaseScriptMethods
    {
        void RegisterExport(string eventName, Delegate callback);
        void AddGlobal(string key, object value);
        object GetGlobal(string key);
        dynamic GetResourceExport(string name);
        void AddTick(Func<Task> x);
        void RemoveTick(Func<Task> x);
        void AddEventListener(string eventName, Delegate delegat);
        void RemoveEventListener(string eventName);
    }
}