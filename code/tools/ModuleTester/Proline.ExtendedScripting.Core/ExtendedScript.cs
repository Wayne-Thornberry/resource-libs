using CitizenFX.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ExtendedScripting.Core
{
    public abstract class ExtendedScript : BaseScript
    {
        private int _state;
        private int _lastState;
        private bool _isPaused;
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
                _state = (int)value;
            }

        }

        public ExtendedScript(bool autoStart = false)
        {
            IsAutoStartEnabled = autoStart;
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
                if (Flags.HasFlag(ScriptFlag.MarkedForTermination))
                {
                    State = ScriptState.Stopped; 
                }
            }
        }
        private bool CanScriptPerformNormalFunctions() => !Flags.HasFlag(ScriptFlag.MarkedForTermination) && Flags.HasFlag(ScriptFlag.Loaded);
        private bool IsScriptInValidFunctionalState() => (State != ScriptState.Inactive || State != ScriptState.Stopped);

        private void AddFlag(ScriptFlag flagToAdd)
        {
            Flags |= flagToAdd;
        }
        private void RemoveFlag(ScriptFlag flagToAdd)
        {
            Flags &= flagToAdd;
        }
        public void Enable()
        {
            AddFlag(ScriptFlag.RequestToStart);
        }

        public void Pause()
        {
            _isPaused = true;
        }

        public void Disable()
        {
            AddFlag(ScriptFlag.MarkedForTermination);
        }
    }
}
