using Proline.Resource.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.MScripting
{
    internal class LWScriptManager
    {
        private Log _log => new Log();
        private List<ScriptContainer> _liveScripts;
        private Dictionary<string, Type> _scriptTypes;
        private static LWScriptManager _instance;

        public LWScriptManager()
        {
            _scriptTypes = new Dictionary<string, Type>();
        }

        internal static LWScriptManager GetInstance()
        {
            if (_instance == null)
                _instance = new LWScriptManager();
            return _instance;
        }


        internal int StartNewScriptInstance(string scriptName, object[] args = null)
        {
            if (!DoesScriptExist(scriptName)) return -1;

            if (_liveScripts == null)
                _liveScripts = new List<ScriptContainer>();

            if (args == null)
                args = new object[0];
            try
            {
                var type = _scriptTypes[scriptName];
                var instance = Activator.CreateInstance(type);

                var sc = new ScriptContainer(instance, args);
                return sc.Start();
            }
            catch (Exception e)
            {
                _log.Error(e.ToString());
            }

            return -1;
        }

        internal bool DoesScriptExist(string scriptName)
        {
            return _scriptTypes.ContainsKey(scriptName);
        }

        internal void ProcessAssembly(string assemblyString)
        {
            _log.Debug($"Loading assembly {assemblyString.ToString()}");
            var ass = Assembly.Load(assemblyString.ToString());
            _log.Debug($"Scanning assembly {assemblyString.ToString()} for scripts");
            var types = ass.GetTypes().Where(e => (object)e.GetMethod("Execute") != null);
            _log.Debug($"Found {types.Count()} scripts that have an execute method");
            foreach (var item in types)
            {
                _scriptTypes.Add(item.Name, item);
            }
            _log.Debug($"Loading complete");
        }
    }
}
