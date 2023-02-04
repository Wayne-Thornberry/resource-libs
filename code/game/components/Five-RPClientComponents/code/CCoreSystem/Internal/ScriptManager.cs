using CitizenFX.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CCoreSystem.Internal
{
    internal static class ScriptManager
    {
        private static List<LiveScript> _liveScripts;
        public static List<LiveScript> LiveScripts
        {
            get
            {
                if (_liveScripts == null)
                    _liveScripts = new List<LiveScript>();
                return _liveScripts;
            }
        }

        internal static void ManageScript(LiveScript script)
        {
            LiveScripts.Add(script);
        }

        internal static void UnmanageScript(LiveScript liveScript)
        {
            LiveScripts.Remove(liveScript);
        }

        internal static void CleanUpCompletedScripts()
        {
            var instances = LiveScripts.Where(e => e.IsCompleted).ToArray();
            if (instances.Count() > 0)
                Console.WriteLine(string.Format("Removing {0} script instances that have been completed", instances.Count()));
            int count = 0;
            foreach (var instance in instances)
            {
                var instanceCount = LiveScripts.Remove(instance);
                count++;
                Console.WriteLine(string.Format("{0} Removed {1} instances", instance.Name, 1));
            }
            if (instances.Count() > 0)
                Console.WriteLine(string.Format("Removed {0} script instances that have been completed", count));
        }

        internal static LiveScript GetLiveScript(object scriptInstance)
        {
            return LiveScripts.FirstOrDefault(e => e.Instance == scriptInstance);
        }

        internal static IEnumerable<string> GetScriptNames()
        {
            var types = GetAllScriptTypes();
            return types.Keys;
        }

        internal static Dictionary<string, Type> GetAllScriptTypes()
        {
            // Get scripting config
            var scriptingConfig = ScriptingConfigSection.ModuleConfig;

            // If we have a config, then we can load the levelscripts
            var scriptTypes = new Dictionary<string, Type>();
            if (scriptingConfig != null)
            {
                // Load Assemblies 
                Console.WriteLine("Retrived config section");
                Console.WriteLine($"Loading level scripts. from {scriptingConfig.LevelScriptAssemblies.Count()} assemblies");
                foreach (var assemblyStrings in scriptingConfig.LevelScriptAssemblies)
                {
                    Console.WriteLine($"Loading assembly {assemblyStrings}");
                    var assembly = Assembly.Load(assemblyStrings.ToString());
                    Console.WriteLine($"Scanning assembly {assemblyStrings} for scripts");
                    var types = assembly.GetTypes().Where(e => (object)e.GetMethod("Execute") != null);
                    Console.WriteLine($"Found {types.Count()} scripts that have an execute method");
                    foreach (var item in types)
                    {
                        if (!scriptTypes.ContainsKey(item.Name))
                            scriptTypes.Add(item.Name, item);
                        else
                            Console.WriteLine($"{item.Name} DUPLICATE?????");
                    }
                    Console.WriteLine($"Loading complete");
                }
            }
            else
            {
                Console.WriteLine("Cannot start script {0} because the config failed to load or is not set");
            }
            return scriptTypes;
        }


        internal static void TerminateMarkedAsNoLongerNeedeScripts()
        {
            var instances = LiveScripts.Where(e => e.IsMarkedForNolongerNeeded).ToArray();
            if (instances.Count() > 0)
                Console.WriteLine(string.Format("Several scripts have been marked as no longer needed {0}, terminating them", instances.Count()));
            foreach (var instance in instances)
            {
                TaskManager.StartNew(instance.Terminate);
            }
        }
    }
}
