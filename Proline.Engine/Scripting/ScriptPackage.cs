using Proline.Engine.Data;
using Proline.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Engine
{ 

    internal class ScriptPackage : EngineObject
    {
        private string _assembly;
        private ScriptConfig[] _scriptConfigs;
        private int _loadedScriptCount;
        private bool _isDebugPackage;
        private string[] _scriptClasses;
        private string[] _startupScripts;
        private int _envType;
        private string _name;

        private Dictionary<string, Script> _scripts; 

        private ScriptPackage(ScriptPackageConfig config) : base("ScriptPackage")
        {
            _assembly = config.Assembly;
            _scriptConfigs = config.ScriptConfigs != null ? config.ScriptConfigs : new ScriptConfig[0];
            _isDebugPackage = config.DebugOnly;
            _scriptClasses = config.ScriptClasses != null ? config.ScriptClasses : new string[0];
            _envType = config.EnvType;

            _scripts = new Dictionary<string, Script>();
        }


        internal void StartNewScript(string scriptName, params object[] args)
        {

            try
            {
                _scripts[scriptName].StartNewInstance(args); 
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }

        internal Script GetScriptWrapper(string scriptName)
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
                var wrapper = Script.CreateNewScript(scriptName, assembly, type, scriptConfig, _envType);
                _scripts.Add(wrapper.Name, wrapper);
                _loadedScriptCount++; 
            }
        }

        internal IEnumerable<string> GetScriptNames()
        {
            return _scripts.Keys;
        }

        internal IEnumerable<Script> GetScriptWrappers()
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
                    Script.UnregisterScript(scriptName);
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
                    Script.RegisterScript(script);
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
