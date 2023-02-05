using CitizenFX.Core;
using Proline.Resource.Scripting;
using System;
using System.Reflection;
using System.Threading.Tasks;
using Console = Proline.Resource.Console;

namespace Proline.ClassicOnline.Resource
{
    public abstract class ModuleScript
    {
        private bool _isScriptDone;

        internal int Interval { get; set; }
        internal long NextRun { get; set; }
        internal int ExecutionCount { get; set; }
        internal Task ExecutionTask { get; set; }
        public string Name { get; private set; }
        public ModuleState State { get; private set; }
        public bool EnablePeriodicRunning { get; }
        public bool IsFinished => _isScriptDone && !IsProcessing;
        public bool IsProcessing => ExecutionTask != null ? !ExecutionTask.IsCompleted : false;

        public ModuleScript(bool enablePeriodicRunning = false)
        {
            EnablePeriodicRunning = enablePeriodicRunning;
            Interval = 10000000;

            // Scripts will be created when the module loads
            // As the module goes through its stages, it can execute scripts with specific script names, for example OnInit
            // When a script has executed, it will exit not be rerun unless RunPerodicly = true and LastCallTime > IntervalTimer
            // Only one script may execute at a given time, if a script is in progress when a reacurring call to execute it happens, the script will not rerun
            // These are background scripts that always exist, unlike level scripts where when a execute is called, it finishes and cleansup the script object
            // Scripts need to be configured in the config file under the module specific area
        }

        public void Execute()
        {
            try
            {
                if (DateTime.UtcNow.Ticks > NextRun)
                {
                    if (ExecutionTask != null)
                    {
                        if (ExecutionTask.IsCompleted)
                        {
                            ExecutionTask.Dispose();
                            ExecutionTask = null;
                            //Console.WriteLine(string.Format("{0} Execution has finished, removing task reference", GetType().Name));
                        }
                    }

                    if ((EnablePeriodicRunning || ExecutionCount == 0) && !_isScriptDone && ExecutionTask == null)
                    {
                        // If EnablePeriodicRunning is true or its the first time execution run as long as
                        // There is no current execution task, the script is not done and the current tick count is greater than next run ticks (which at the start is 0)

                        State = ModuleState.Loading;
                        ExecutionTask = OnExecute();
                        ExecutionCount++;
                        //Console.WriteLine(string.Format("{0} New execution has begun", GetType().Name));
                    }

                    if (!EnablePeriodicRunning && ExecutionCount == 1 && !_isScriptDone && ExecutionTask == null)
                    {
                        _isScriptDone = true;
                        // Console.WriteLine(string.Format("{0} Cannot execute script, script has finished its processing so it no longer runs", GetType().Name));
                    }
                    NextRun = DateTime.UtcNow.Ticks + Interval;
                }
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
