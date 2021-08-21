
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
    internal class ScriptPackageManager
    {
        private static ScriptPackageManager _instance;
        private List<ScriptPackage> _packages;  

        private ScriptPackageManager()
        {
            _packages = new List<ScriptPackage>();
        }

        internal static ScriptPackageManager GetInstance()
        { 
            if (_instance == null)
                _instance = new ScriptPackageManager();
            return _instance;
        }

        internal IEnumerable<ScriptPackage> GetScriptPackages()
        {
            return _packages;
        }
         
        internal void RegisterScriptPackage(ScriptPackage sp)
        {
            var sm = ScriptManager.GetInstance();
            foreach (var scriptName in sp.GetScriptNames())
            {
                try
                {
                    sm.RegisterScript(scriptName, sp);
                }
                catch (ArgumentException e)
                {

                }
                catch (Exception e)
                {
                    throw;
                }
            }
            _packages.Add(sp);
        }

        internal void UnRegisterScriptPackage(ScriptPackage sp)
        {
            var sm = ScriptManager.GetInstance();
            foreach (var scriptName in sp.GetScriptNames())
            {
                try
                {
                    sm.UnRegisterScript(scriptName);
                }
                catch (ArgumentException e)
                {

                }
                catch (Exception e)
                {
                    throw;
                }
            }
            _packages.Add(sp);
        } 
    }
}
