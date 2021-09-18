using System;
using System.Reflection;
using System.Threading.Tasks;
using Proline.Engine.Data;
using Proline.Engine.Internals;
using Proline.Engine.Tools;

namespace Proline.Engine.AAPI
{
    public class APIMethod : EngineObject
    {
        private object _source;
        private MethodInfo _item;
        private bool _debugOnly;
        private int _type;
        private int _hash;

        public string Signature { get; private set; }

        internal APIMethod(object source, MethodInfo item, int type = 0, bool debugOnly = false) : base("API")
        {
            _source = source;
            _item = item;
            _debugOnly = debugOnly;
            _type = type; 
        }

        public string Name => _item.Name; 

        internal object Invoke(params object[] args)
        {
            //LogDebug("Invoking API " + _item.Name);
            if (_debugOnly && !EngineConfiguration.IsDebugEnabled) throw new Exception("Debug mode not enabled on debug only API");
            if (_type == -1 && EngineConfiguration.IsClient)
                throw new Exception("Cannot invoke a server method while as a client non async");
            return _item.Invoke(_source, args);
        }

        internal async Task<T> InvokeAsync<T>(Action<object> callback, params object[] args)
        {
            if (_debugOnly && !EngineConfiguration.IsDebugEnabled) throw new Exception("Debug mode not enabled on debug only API");
            if (_type == -1 && EngineConfiguration.IsClient)
                await ExecuteServerAPI<object>(_item.Name, args).ContinueWith(callback);
            return (T)Convert.ChangeType(_item.Invoke(_source, args), typeof(T));
        }


        private async Task<T> ExecuteServerAPI<T>(string methodName, params object[] args)
        {
            return default;
            //var client = new NetClient();
            //return await client.ExecuteEngineMethodServer<T>(EngineConstraints.ExecuteAPI, new object[] { methodName, args });
        }


        internal async Task<object> InvokeAsync(Action<object> callback, params object[] args)
        {
            if (_debugOnly && !EngineConfiguration.IsDebugEnabled) throw new Exception("Debug mode not enabled on debug only API");
            if (_type == -1 && EngineConfiguration.IsClient)
                await ExecuteServerAPI<object>(_item.Name, args).ContinueWith(callback);
            return _item.Invoke(_source, args);
        }

        internal async Task<T> InvokeAsync<T>(params object[] args)
        {
            if (_debugOnly && !EngineConfiguration.IsDebugEnabled) throw new Exception("Debug mode not enabled on debug only API");
            if (_type == -1 && EngineConfiguration.IsClient)
                return await ExecuteServerAPI<T>(_item.Name, args);
            return (T)Convert.ChangeType(_item.Invoke(_source, args),typeof(T));
        }

        internal async Task<object> InvokeAsync(params object[] args)
        {
            if (_debugOnly && !EngineConfiguration.IsDebugEnabled) throw new Exception("Debug mode not enabled on debug only API");
            if (_type == -1 && EngineConfiguration.IsClient)
               return await ExecuteServerAPI<object>(_item.Name, args);
            return _item.Invoke(_source, args);
        }

        public override string ToString()
        {
            return (_type == -1 ? $"[Server {Type}]" : $"[Client {Type}]") + " " + Name + " " + GetHashCode();
        }

        public override int GetHashCode()
        {
            if (string.IsNullOrEmpty(Signature))
                Signature = GenerateSignature();

            if (_hash == 0)
                _hash = JoatHashing.GenerateHash(Signature);
            return _hash;
        }

        private string GenerateSignature()
        {
            var h = _item.Name; 
            foreach (var item in _item.GetParameters())
            {
                h += item.ParameterType.Name;
            }
            h += _item.ReturnType.Name;
            return h; 
        }

        internal static void RegisterAPI(APIMethod componentAP)
        {
            InternalManager im = InternalManager.GetInstance();
            im.AddAPI(componentAP);
        }

        internal static void UnregisterAPI(APIMethod apiName)
        {
            InternalManager im = InternalManager.GetInstance();
            im.RemoveAPI(apiName);
        }

        public static async Task<object> CallAPIAsync(int apiName, params object[] inputParameters)
        {
            var cm = InternalManager.GetInstance();
            var api = cm.GetAPI(apiName);
            if (api == null)
                return null;
            return await api.InvokeAsync(inputParameters);
        }

        public static async Task<T> CallAPIAsync<T>(int apiName, params object[] inputParameters)
        {
            var cm = InternalManager.GetInstance();
            var api = cm.GetAPI(apiName);
            if (api == null)
                return default;
            return await api.InvokeAsync<T>(inputParameters);
        }

        public static object CallAPI(int apiName, params object[] inputParameters)
        {
            var cm = InternalManager.GetInstance();
            var api = cm.GetAPI(apiName);
            if (api == null) return null;
            return api.Invoke(inputParameters);
        }

        public static T CallAPI<T>(int apiName, params object[] inputParameters)
        {
            var cm = InternalManager.GetInstance();
            var api = cm.GetAPI(apiName);
            if (api == null) return default;
            return (T)Convert.ChangeType(api.Invoke(inputParameters), typeof(T));
        }
    }
}