using Newtonsoft.Json;
using Proline.Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Resource.Component.Framework
{
    public abstract class ComponentHandler : ComponentObject, IComponentHandler, ILogMethods
    {
        private Log _log => Logger.GetInstance().GetLog();
        protected ComponentContainer Component => ComponentContainer.GetInstance();
        protected ComponentHandler() 
        {

        }

        
        public virtual async Task OnLoad() { }
        public virtual async Task OnInitialized() { }
        public virtual async Task OnTick() { }

        public void LogDebug(string data)
        {
            _log.LogDebug(data);
        }

        public void LogError(string data)
        {
            _log.LogError(data);
        }

        public void LogInfo(string data)
        {
            _log.LogInfo(data);
        }
        public async Task<object> CallServerAPI(int apiHash, params object[] p)
        {
            return default;
        }

        public void LogWarn(string data)
        {
            _log.LogWarn(data);
        }
    }
}
