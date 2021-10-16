using Newtonsoft.Json;
using Proline.Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Resource.Component.Framework
{
    public class ComponentEnviroment 
    {
        private Log _log => Logger.GetInstance().GetLog(); 
        private int _state;
        private IComponentHandler _handler;
        private ComponentContainer _container;
        private Dictionary<long, ComponentAPI> _apis;
        private ComponentPropertyManager _propertyManager;
        private IComponentSource _source;

        public ComponentEnviroment(ComponentContainer container, IComponentHandler handler)
        {
            _handler = handler;
            _container = container;
            _propertyManager = new ComponentPropertyManager();
            _apis = new Dictionary<long, ComponentAPI>(); 
        }   

        public object CallAPI(long x, object[] args)
        {
           return _apis[x].Invoke(args);
        } 

        public Log GetLog()
        {
            return _log;
        }

        public IEnumerable<ComponentAPI> GetAPIs()
        {
            return _apis.Values;
        }

        public void Start()
        {
            _handler.OnTick();
        }

        public void SyncProperty(PropertyInfo item)
        {
            _propertyManager.ManageProperty(item); 
        }

        public async Task<object> CallEnvAPI(int apiHash, params object[] p)
        {
            return default;// await _source?.CallServerAPI(apiHash, JsonConvert.SerializeObject(p));
        }

        public void Load()
        { 
            if (_state == 0)
            {
                _state = 1;
                _handler?.OnLoad();
            } 
        }

        public void RegisterAPI(ComponentAPI api)
        { 
            if(!_apis.ContainsKey(api.GetAPIMethodHash()))
                _apis.Add(api.GetAPIMethodHash(), api);
        }  

        public void Initialize()
        {
            _state = 2;
            _handler?.OnInitialized();
        }
    }
}
