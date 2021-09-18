using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proline.Engine.Data;
using Proline.Engine.Internals;

namespace Proline.Engine.Scripting
{
    internal class ScriptLoader
    {
        internal void LoadScripts()
        {
            if (EngineStatus.IsScriptsInitialized) return;
            //InsertScriptAssemblies(); 
            var sm = InternalManager.GetInstance();
            foreach (var item in EngineConfiguration.ScriptPackages)
            {
                try
                {
                    var sp = ScriptPackage.Load(item);
                    if (sp == null) continue;
                    ScriptPackage.RegisterPackage(sp);
                    //Debugger.LogDebug("Successfully loaded script package");
                }
                catch (Exception e)
                {
                    //Debugger.LogError(e.ToString());
                    throw;
                }
            }
            //Debugger.LogDebug(string.Format("Scripts initialized sucessfully, {0} Scripts loaded", sm.GetScriptCount()));
            EngineStatus.IsScriptsInitialized = true;
        }
    }
}
