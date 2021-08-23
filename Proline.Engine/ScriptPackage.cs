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

        private Dictionary<string, EngineScript> _scripts; 

        private ScriptPackage(ScriptPackageConfig config)
        {
            _assembly = config.Assembly;
            _scriptConfigs = config.ScriptConfigs != null ? config.ScriptConfigs : new ScriptConfig[0];
            _isDebugPackage = config.DebugOnly;
            _scriptClasses = config.ScriptClasses != null ? config.ScriptClasses : new string[0];
            _startupScripts = config.StartScripts != null ? config.StartScripts : new string[0];

            _scripts = new Dictionary<string, EngineScript>();
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

        internal EngineScript GetScriptWrapper(string scriptName)
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
                var scriptName = type.Name;
                var scriptConfig = HasCustomConfig(scriptName) ? GetCustomConfig(scriptName) : new ScriptConfig();
                var wrapper = EngineScript.CreateNewScript(scriptName, assembly, type, scriptConfig);
                _scripts.Add(wrapper.Name, wrapper);
                _loadedScriptCount++; 
            }
        }

        internal IEnumerable<string> GetScriptNames()
        {
            return _scripts.Keys;
        }

        internal IEnumerable<EngineScript> GetScriptWrappers()
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


        internal static void UnregisterPackage(ScriptPackage sp)
        {
            var sm = InternalManager.GetInstance();
            foreach (var scriptName in sp.GetScriptWrappers())
            {
                try
                {
                    EngineScript.UnregisterScript(scriptName);
                }
                catch (ArgumentException e)
                {

                }
                catch (Exception e)
                {
                    throw;
                }
            }
            sm.RemovePackage((sp));
        }
        internal static void RegisterPackage(ScriptPackage sp)
        {
            var sm = InternalManager.GetInstance();
            foreach (var script in sp.GetScriptWrappers())
            {
                try
                {
                    EngineScript.RegisterScript(script);
                }
                catch (ArgumentException e)
                {

                }
                catch (Exception e)
                {
                    throw;
                }
            }
            sm.AddPackage(sp);
        }
    }
}
