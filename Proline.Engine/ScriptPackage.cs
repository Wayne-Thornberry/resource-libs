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

    internal class ScriptPackage
    {
        private string _assembly;
        private ScriptConfig[] _scriptConfigs;
        private int _loadedScriptCount;
        private bool _isDebugPackage;
        private string[] _scriptClasses;
        private string[] _startupScripts;
        private string _name;

        private Dictionary<string, ScriptWrapper> _scripts; 

        private ScriptPackage(ScriptPackageConfig config)
        {
            _assembly = config.Assembly;
            _scriptConfigs = config.ScriptConfigs != null ? config.ScriptConfigs : new ScriptConfig[0];
            _isDebugPackage = config.DebugOnly;
            _scriptClasses = config.ScriptClasses != null ? config.ScriptClasses : new string[0];
            _startupScripts = config.StartScripts != null ? config.StartScripts : new string[0];

            _scripts = new Dictionary<string, ScriptWrapper>();
        }

        internal void StartStartupScripts()
        {
            foreach (var item in _startupScripts)
            {
                StartNewScript(item);
            }
        }

        internal void StartNewScript(string scriptName, params object[] args)
        {

            try
            {
          _scripts[scriptName].StartNew(args);

            }
            catch (Exception ex)
            {
                Debugger.LogError(ex);
            }
        }

        internal ScriptWrapper GetScriptWrapper(string scriptName)
        {
            return _scripts[scriptName];
        }

        private bool HasCustomConfig(string scriptName)
        {
            foreach (var item in _scriptConfigs)
            {
                if (item.ScriptName.Equals(scriptName))
                    return true;
            }
            return false;
        }


        internal static ScriptPackage Load(ScriptPackageConfig config)
        {
            try
            {
                var assembly = Assembly.Load(config.Assembly);
                var package = new ScriptPackage(config);
                package.Load();
                Debugger.LogDebug("Successfully loaded script package");
                return package;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private void Load()
        {
            var assembly = Assembly.Load(_assembly);
            foreach (var classPath in _scriptClasses)
            {
                var type = assembly.GetType(classPath);
                if (type != null)
                {
                    // if type has custom attributes like name, take that instead
                    var scriptName = type.Name;
                    var scriptConfig = HasCustomConfig(scriptName) ? GetCustomConfig(scriptName) : new ScriptConfig();
                    var wrapper = new ScriptWrapper(_assembly, classPath, scriptConfig);
                    _scripts.Add(scriptName, wrapper);
                    _loadedScriptCount++;
                }
            }
            Debugger.LogDebug("Loaded " + _loadedScriptCount + " Scripts");
        }

        internal IEnumerable<string> GetScriptNames()
        {
            return _scripts.Keys;
        }

        internal IEnumerable<ScriptWrapper> GetScriptWrappers()
        {
            return _scripts.Values;
        }

        private ScriptConfig GetCustomConfig(string scriptName)
        {
            foreach (var item in _scriptConfigs)
            {
                if (item.ScriptName.Equals(scriptName))
                    return item;
            }
            return null; 
        }
    }
}
