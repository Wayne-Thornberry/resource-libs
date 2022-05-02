using Proline.Resource.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Resource.Framework
{
    public abstract class ResourceScript : IResourceScript
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

        public ResourceScript()
        {
            _state = 0;
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

        internal void Disable()
        {
            State = -1;
        }

        internal void Enable()
        {
            State = _lastState;
        }
    }
}
