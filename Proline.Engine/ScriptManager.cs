
using Newtonsoft.Json;
using Proline.Engine.Data;
using Proline.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Engine
{
    internal class ScriptManager
    {
        private static ScriptManager _instance;
        private LevelScriptAssembly[] _scriptAssemblies;
        private List<LevelScript> _scripts;

        ScriptManager()
        {
            _scripts = new List<LevelScript>();
        }

        internal static ScriptManager GetInstance()
        { 
            if (_instance == null)
                _instance = new ScriptManager();
            return _instance;
        }

       internal static void InsertScriptAssemblies(LevelScriptAssembly[] levelScriptAssemblies)
        {
            GetInstance()._scriptAssemblies = levelScriptAssemblies;
        }

        internal static LevelScriptAssembly[] GetScriptAssemblies()
        {
            return GetInstance()._scriptAssemblies;
        }

        internal static IEnumerable<LevelScript> GetScripts()
        {
            return GetInstance()._scripts;
        }

        internal static IEnumerable<LevelScript> GetScripts(string name)
        {
            return GetInstance()._scripts.Where(e => e.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        internal static void Initialize()
        {
            if (EngineStatus.IsScriptsInitialized) return;
            InsertScriptAssemblies(EngineConfiguration.GetScripts());
            var sm = GetInstance();
             
            ScriptCache sc = ScriptCache.GetInstance();

            var s = ScriptManager.GetScriptAssemblies();
            Debugger.LogDebug("Loaded Script Libraries File");
            Debugger.LogDebug("Library count: " + s.Length);
            foreach (var item in s)
            {
                Debugger.LogDebug(item.StartupScript);
                var assembly = Assembly.Load(item.Assembly);
                foreach (var t in item.ScriptClasses)
                {
                    var type = assembly.GetType(t);
                    sc.CacheScriptType(type);
                }
            }
            EngineStatus.IsScriptsInitialized = true;
        }

        internal static void UnregisterScript(LevelScript script)
        {
            GetInstance()._scripts.Remove(script);
        }

        internal static void RegisterScript(LevelScript script)
        {
            GetInstance()._scripts.Add(script);
        }
    }
}
