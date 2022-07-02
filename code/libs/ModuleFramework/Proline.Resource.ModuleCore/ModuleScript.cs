using CitizenFX.Core;
using Proline.Resource.Scripting;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace Proline.Modularization.Core
{
    public abstract class ModuleScript 
    {
        public string Name { get; private set; }
        public ModuleState State { get; private set; }
        public bool EnablePeriodicRunning { get; private set; }
        public bool IsFinished { get; private set; }

        public ModuleScript(bool enablePeriodicRunning = false)
        {
            EnablePeriodicRunning = enablePeriodicRunning;
            // RunAfterLoad
            // Run Perodicly
            // Interval Timer
            // Name - Special names can be called to facitiate loading  

            // Scripts will be created when the module loads
            // As the module goes through its stages, it can execute scripts with specific script names, for example OnInit
            // When a script has executed, it will exit not be rerun unless RunPerodicly = true and LastCallTime > IntervalTimer
            // Only one script may execute at a given time, if a script is in progress when a reacurring call to execute it happens, the script will not rerun
            // These are background scripts that always exist, unlike level scripts where when a execute is called, it finishes and cleansup the script object
            // Scripts need to be configured in the config file under the module specific area
        }

        public async Task Execute()
        {

            try
            {
                if (!IsFinished)
                { 
                    State = ModuleState.Loading;
                    await OnExecute();
                    if (!EnablePeriodicRunning)
                        IsFinished = true;
                }
                // could look for events outside of script
                // - this includes callback events
                // could look for exports
                // could look for commands
            }
            catch (Exception)
            { 
                throw;
            }
            finally
            {
                State = ModuleState.Active;
            }
        }

        public virtual async Task OnExecute() { }

    }
}
