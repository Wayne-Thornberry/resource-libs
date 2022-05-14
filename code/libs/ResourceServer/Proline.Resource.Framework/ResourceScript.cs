using System;
using System.Threading.Tasks;
using CitizenFX.Core;
using Proline.Resource.Common;
using Proline.Resource.Logging;


namespace Proline.Resource.Framework
{
    public abstract class ResourceScript : BaseScript, IScript
    {
        private int _state;
        private int _lastState;
        protected Log _log = new Log();
        public bool EnableFrameSync { get; set; }
        public bool HasStarted => State > 0;
        public bool IsPaused { get; set; }

        public int State
        {
            get => _state;

            set
            {
                _lastState = _state;
                _state = value;
            }

        }

        public ResourceScript()
        { 
            _state = -1; 
        }

        public virtual async Task OnStart() { }

        public virtual async Task OnUpdate() { }

        [Tick]
        public async Task OnTick()
        {
            try
            {
                if(State > 0 && !IsPaused)
                { 
                    if (!HasStarted)
                    {
                        await OnStart();
                        State = 2;
                    }
                    else
                    {
                        await OnUpdate();
                    }
                }
            }
            catch (Exception e)
            {
                _log.Error(e.ToString());
                State = -1;
                // The buck stops here
            }
            //if (EnableFrameSync)
            //    await InternalManager.API.Delay(0);
        }
    }
}
