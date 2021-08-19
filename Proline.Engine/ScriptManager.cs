
using Newtonsoft.Json;
using Proline.Engine.Data;
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
        private static LevelScriptAssembly[] _scriptAssemblies;
        private static ScriptManager _instance;

        internal static ScriptManager GetInstance()
        { 
            if (_instance == null)
                _instance = new ScriptManager();
            return _instance;
        }

       internal static void InsertScriptAssemblies(LevelScriptAssembly[] levelScriptAssemblies)
        {
            _scriptAssemblies = levelScriptAssemblies;
        }

        internal static LevelScriptAssembly[] GetScriptAssemblies()
        {
            return _scriptAssemblies;
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
    }
}
