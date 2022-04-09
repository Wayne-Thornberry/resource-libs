
using Proline.CScripting.Framework;
using Proline.Resource.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Classic.Client.Engine.CScripting
{
    public class ScriptLibrary
    {
        private Dictionary<string, string> _scriptNameKeyPairAssembly;
        private Dictionary<string, string> _scriptNameKeyPairTypeString;
        protected Log _log => Logger.GetInstance().GetLog();

        public ScriptLibrary()
        {
            _scriptNameKeyPairAssembly = new Dictionary<string, string>();
            _scriptNameKeyPairTypeString = new Dictionary<string, string>();
        }

        public void ProcessAssembly(string assemblyString)
        {
            ProcessAssembly(Assembly.Load(assemblyString));
        }

        public string[] GetAvalibleScripts()
        {
            return _scriptNameKeyPairTypeString.Keys.ToArray();
        }

        private void ProcessAssembly(Assembly assembly)
        {
            var types = GetScriptTypesFromAssembly(assembly);
            var count = 0;
            _log.Debug(types.Length.ToString());
            foreach (var item in types)
            {
                _scriptNameKeyPairAssembly.Add(item.Name, assembly.FullName);
                _scriptNameKeyPairTypeString.Add(item.Name, item.FullName);
                _log.Debug(item.FullName);
                count++;
            }
            // count amount of scripts found in library
        }

        private Type[] GetScriptTypesFromAssembly(Assembly assembly)
        { 
            return assembly.GetTypes().Where(e => e.IsSubclassOf(typeof(ScriptInstance)) && e.IsClass && !e.IsAbstract).ToArray();
        }

        private IEnumerable<Type[]> GetScriptTypesFromAssemblies(Assembly[] assembly)
        {
            return assembly.Select(a => a.GetTypes().Where(e => e.IsInstanceOfType(typeof(ScriptInstance)) && e.IsClass && !e.IsAbstract).ToArray()).ToArray();
        }

        internal Type GetScriptType(string scriptName)
        {
            if (!_scriptNameKeyPairTypeString.ContainsKey(scriptName)) {
                _log.Debug(scriptName + " Not found");
                return null;
            }
            var typeString = _scriptNameKeyPairTypeString[scriptName];
            var assembly = Assembly.Load(_scriptNameKeyPairAssembly[scriptName]); 
            return assembly.GetType(typeString); 
        }
    }
}
