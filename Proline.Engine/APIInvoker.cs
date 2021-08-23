using System;
using System.Reflection;
using System.Threading.Tasks;

namespace Proline.Engine
{
    internal class APIInvoker
    {
        private object _source;
        private MethodInfo _item;
        private bool _debugOnly;
        private int _type;
        private int _hash;

        public string Signature { get; private set; }

        public APIInvoker(object source, MethodInfo item, int type = 0, bool debugOnly = false)
        {
            _source = source;
            _item = item;
            _debugOnly = debugOnly;
            _type = type; 
            Type = "API";
        }

        public string Name => _item.Name;
        public string Type { get; private set; }

        internal object Invoke(params object[] args)
        {
            if (_debugOnly && !EngineConfiguration.IsDebugEnabled) throw new Exception("Debug mode not enabled on debug only API");
            if (_type == -1 && EngineConfiguration.IsClient)
                throw new Exception("Cannot invoke a server method while as a client non async");
            return _item.Invoke(_source, args);
        }

        internal async Task<T> InvokeAsync<T>(Action<object> callback, params object[] args)
        {
            if (_debugOnly && !EngineConfiguration.IsDebugEnabled) throw new Exception("Debug mode not enabled on debug only API");
            if (_type == -1 && EngineConfiguration.IsClient)
                await EngineAccess.ExecuteComponentAPI<object>(_item.Name, args).ContinueWith(callback);
            return (T)Convert.ChangeType(_item.Invoke(_source, args), typeof(T));
        }

        internal async Task<object> InvokeAsync(Action<object> callback, params object[] args)
        {
            if (_debugOnly && !EngineConfiguration.IsDebugEnabled) throw new Exception("Debug mode not enabled on debug only API");
            if (_type == -1 && EngineConfiguration.IsClient)
                await EngineAccess.ExecuteComponentAPI<object>(_item.Name, args).ContinueWith(callback);
            return _item.Invoke(_source, args);
        }

        internal async Task<T> InvokeAsync<T>(params object[] args)
        {
            if (_debugOnly && !EngineConfiguration.IsDebugEnabled) throw new Exception("Debug mode not enabled on debug only API");
            if (_type == -1 && EngineConfiguration.IsClient)
                return await EngineAccess.ExecuteComponentAPI<T>(_item.Name, args);
            return (T)Convert.ChangeType(_item.Invoke(_source, args),typeof(T));
        }

        internal async Task<object> InvokeAsync(params object[] args)
        {
            if (_debugOnly && !EngineConfiguration.IsDebugEnabled) throw new Exception("Debug mode not enabled on debug only API");
            if (_type == -1 && EngineConfiguration.IsClient)
               return await EngineAccess.ExecuteComponentAPI<object>(_item.Name, args);
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

        internal static void RegisterAPI(APIInvoker componentAP)
        {
            try
            {
                InternalManager im = InternalManager.GetInstance();
                Debugger.LogDebug("Registered " + componentAP.ToString());
                //_apisAndComponents.Add(apiName, component);
                im.AddAPI(componentAP);
            }
            catch (ArgumentException e)
            {
                Debugger.LogDebug("Could not add API, same API already exists");
            }
        }

        internal static void UnregisterAPI(APIInvoker apiName)
        {
            InternalManager im = InternalManager.GetInstance();
            im.RemoveAPI(apiName);
        }
    }
}