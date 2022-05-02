using System;
using System.Threading.Tasks;
using Proline.Resource.Common;
using Proline.Resource.Logging;


namespace Proline.Resource.Framework
{
    public abstract class ResourceScript : IScript
    {
        private int _state;
        private int _lastState;
        protected Log _log = new Log();
        public bool EnableFrameSync { get; set; }
        public bool HasStarted => State > 0;
        public bool IsPaused => State == -1;

        public int State
        {
            get => _state;

            set
            {
                _lastState = _state;
                _state = value;
            }

        }

        public virtual async Task OnStart() { }

        public virtual async Task OnUpdate() { }

        public async Task OnTick()
        {
            try
            {
                if (!IsPaused)
                {
                    if (!HasStarted)
                    {
                        await OnStart();
                        State = 1;
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
