using CitizenFX.Core;
using Proline.Resource.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proline.Resource.Eventing;
using Proline.Resource.Globals;

namespace Proline.Resource.Scripting
{
    public abstract class ExtendedScript : BaseScript
    {
        private int _state;
        private int _lastState;
        private bool _isPaused;
        protected Log _log;

        public bool HasNoFlags => Flags == ScriptFlag.None;
        public bool EnableFrameSync { get; set; }
        public bool IsAutoStartEnabled { get; }
        public bool HasLoaded => Flags.HasFlag(ScriptFlag.Loaded);
        public bool HasStarted => Flags.HasFlag(ScriptFlag.Started);
        public bool IsPaused => _isPaused;
        public ScriptFlag Flags { get; private set; }
        public ScriptState State
        {
            get => (ScriptState)_state;

            set
            {
                if (State == ScriptState.Stopped)
                    return;
                _lastState = _state;
                //OutputToConsole(String.Format("State changed from {0} [{1}] to {2} [{3}]", _lastState, ((ScriptState)_lastState), _state, ((ScriptState)_state)));
                _state = (int)value;
            }

        }

        public ExtendedScript(bool autoStart = false)
        {
            IsAutoStartEnabled = autoStart;
            _log = new Log();
            var globalManager = GlobalsManager.GetInstance();
            var manager = EventDictionaryManager.GetInstance();
            manager.SetEventHandlerDictionary(EventHandlers);
            globalManager.GlobalProperties = GlobalState;
            manager.PlayerList = Players;
        }

        public virtual async Task OnLoad() { }

        public virtual async Task OnStart() { }

        public virtual async Task OnUpdate() { }

        [Tick]
        public async Task OnTick()
        {
            try
            {
                /// Loading happens regardless of if the script is paused, front facing functions would be disabled from updates 
                /// Loading occures when the assembly has loaded since this inherits of base script and tick is automaticly applied
                /// If the script is marked for termination the script should just set its state to stopped
                if (!Flags.HasFlag(ScriptFlag.MarkedForTermination))
                {
                    if (!HasLoaded)
                    {
                        State = ScriptState.Loading;
                        await OnLoad();
                        AddFlag(ScriptFlag.Loaded);
                        if (IsAutoStartEnabled)
                            AddFlag(ScriptFlag.RequestToStart);
                    }

                    /// This functionality should only be called when the observer of these scripts is confident all other scripts have loaded in. 
                    /// This is core functionaly to the game.
                    if (!IsPaused && HasLoaded && Flags.HasFlag(ScriptFlag.RequestToStart))
                    {
                        State = ScriptState.Running;
                        if (IsScriptInValidFunctionalState() && CanScriptPerformNormalFunctions())
                        {
                            if (!HasStarted)
                            {
                                await OnStart();
                                AddFlag(ScriptFlag.Started);
                            }

                            if (HasStarted)
                            {
                                await OnUpdate();
                            }
                            else
                            {
                                // Cannot perform updates if the script has not started
                            }
                        }
                    }
                    else if (HasLoaded && IsPaused && HasStarted)
                    {
                        State = ScriptState.Paused;
                    }
                    else
                    {
                        State = ScriptState.AwaitingStart;

                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                AddFlag(ScriptFlag.MarkedForTermination);
            }
            finally
            {
                if (Flags.HasFlag(ScriptFlag.MarkedForTermination) && State != ScriptState.Stopped)
                {
                    OutputToConsole(String.Format("Script marked for termination"));
                    State = ScriptState.Stopped;
                }
            }
        }

        private void OutputToConsole(string v)
        {
            var type = GetType();
            Console.WriteLine(String.Format("{0}{1}", type.Name, v));
        }

        private bool CanScriptPerformNormalFunctions() => !Flags.HasFlag(ScriptFlag.MarkedForTermination) && Flags.HasFlag(ScriptFlag.Loaded);
        private bool IsScriptInValidFunctionalState() => State != ScriptState.Inactive || State != ScriptState.Stopped;

        private void AddFlag(ScriptFlag flagToAdd)
        {
            Flags |= flagToAdd;
            OutputToConsole(String.Format("State flag added {0}, flag state: ", flagToAdd, Flags));
        }
        private void RemoveFlag(ScriptFlag flagToAdd)
        {
            Flags &= flagToAdd;
            OutputToConsole(String.Format("State flag removed {0}, flag state: ", flagToAdd, Flags));
        }
        public void Enable()
        {
            AddFlag(ScriptFlag.RequestToStart);
            RegisterScript(this);
            OutputToConsole(String.Format("Script enabled"));
        }

        public void Pause()
        {
            _isPaused = true;
            OutputToConsole(String.Format("Script paused"));
        }

        public void Disable()
        {
            AddFlag(ScriptFlag.MarkedForTermination);
            UnregisterScript(this);
            OutputToConsole(String.Format("Script disabled"));
        }
    }
}
