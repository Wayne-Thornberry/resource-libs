
using Newtonsoft.Json;

using Proline.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Engine
{
    internal static class ScriptFactory
    {
        internal static void CreateNewScript(string scriptName, object[] args)
        {
            try
            {
                ScriptCache sc = ScriptCache.GetInstance();
                Type type = null;
                if (sc.DoesScriptExist(scriptName))
                {
                    Debugger.LogDebug("Found existing script type for " + scriptName);
                    type = sc.GetScriptType(scriptName);
                }

                try
                {
                    if (type != null)
                    {
                        // Need to track scripts that start
                        Debugger.LogDebug("Attempting to start " + scriptName + " with args " + args);
                        LevelScript script = (LevelScript)Activator.CreateInstance(type);
                        script.Parameters = args;
                        var task = new Task(async () => {
                            try
                            {
                                ScriptManager.RegisterScript(script);
                                if (EngineConfiguration.IsEngineConsoleApp()) return;
                                var em = ExtensionManager.GetInstance();
                                var extensions = em.GetExtensions();
                                foreach (var extension in extensions)
                                {
                                    extension.OnScriptInitialized(script.Name);
                                }
                                await script.Execute(args);
                                ScriptManager.UnregisterScript(script);
                            }
                            catch (Exception)
                            {
                                throw;
                            }
                        });

                        task.Start();
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                Debugger.LogError(ex);
                return;
            }
            return;
        }

    }
}
