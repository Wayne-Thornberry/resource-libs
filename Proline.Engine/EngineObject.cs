

using Newtonsoft.Json;
using Proline.Engine;
using Proline.Engine.Networking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Engine
{
    public abstract class EngineObject
    {
        private static int _timeout = 1000;
        private Log _log;
        private IScriptSource _source;
        private string _type;

        public string Type => _type;

        protected EngineObject(string typeName)
        {
            _log = new Log();
            _type = typeName;
        }

   

        //public static async Task TriggerEngineEvent(string eventName, params object[] args)
        //{
        //    var cm = InternalManager.GetInstance();
        //    var components = cm.GetComponents();
        //    //Debugger.LogDebug("Engine event triggered " + eventName);
        //    foreach (var item in components)
        //    {
        //        item.OnEngineEvent(eventName, args);
        //    }
        //}

        public async Task<T> ExecuteComponentAPI<T>(string methodName, params object[] args)
        {
            var response = await ExecuteEngineMethodServerI(EngineConstraints.ExecuteComponentAPI, "", methodName, args);
            if (response.Result.ResultValue == null) return default;
            return (T)Convert.ChangeType(response.Result.ResultValue, typeof(T));
        }

        public async Task<T> ExecuteEngineMethodServer<T>(string methodName, params object[] args)
        {
            var response = await ExecuteEngineMethodServerI(methodName, args);
            if (response.Result.ResultValue == null) return default;
            return (T)Convert.ChangeType(response.Result.ResultValue, typeof(T));
        }

        public async Task ExecuteEngineMethodServer(string methodName, params object[] args)
        {
            var response = await ExecuteEngineMethodServerI(methodName, args);
        }

        //public static async Task<T> ExecuteServerMethod<T>(string methodName, params object[] args)
        //{
        //    return await ExecuteComponentAPI<T>(EngineConstraints.ExecuteComponentAPI, "", methodName, args);
        //}

        //public static async Task<T> ExecuteServerMethod<T>(string componentName, string methodName, params object[] args)
        //{
        //    return await ExecuteComponentAPI<T>(EngineConstraints.ExecuteComponentAPI, componentName, methodName, args);
        //} 


        internal async Task<NetworkResponse> ExecuteEngineMethodServerI(string methodName, params object[] args)
        {
            var nm = NetworkManager.GetInstance();
            var guid = Guid.NewGuid().ToString();
            var request = nm.CreateAndInsertRequestI(guid, methodName, args);
            request.Header.DateSent = DateTime.UtcNow;
            var jsonArgs = JsonConvert.SerializeObject(args);
            var requestParams = new EventRequestParams()
            {
                GUID = guid,
                MethodName = methodName,
                MethodArgs = jsonArgs,
            };
            var data = JsonConvert.SerializeObject(requestParams);
            //LogDebug(data);
            EngineService.GetInstance().TriggerServerEvent(NetworkManager.NetworkRequestListenerHandle, guid, methodName, JsonConvert.SerializeObject(args));
            NetworkResponse response = await MakeRequest(guid, request);

            return response;
        }

        internal async Task<NetworkResponse> ExecuteServerMethod(string componentName, string methodName, params object[] args)
        {
            var nm = NetworkManager.GetInstance();
            var guid = Guid.NewGuid().ToString();
            var request = nm.CreateAndInsertRequestI(guid, componentName, methodName, args);
            request.Header.DateSent = DateTime.UtcNow;
            var jsonArgs = JsonConvert.SerializeObject(args);
            var requestParams = new EventRequestParams()
            {
                GUID = guid,
                ComponentName = componentName,
                MethodName = methodName,
                MethodArgs = jsonArgs,
            };
            var data = JsonConvert.SerializeObject(requestParams);
            //LogDebug(data);
            EngineService.GetInstance().TriggerServerEvent(NetworkManager.NetworkRequestListenerHandle, guid, componentName, methodName, JsonConvert.SerializeObject(args));
            NetworkResponse response = await MakeRequest(guid, request);

            return response;
        }

        internal async Task<NetworkResponse> ExecuteMethod(string componentName, string methodName, params object[] args)
        {
            var nm = NetworkManager.GetInstance();
            var guid = Guid.NewGuid().ToString();
            var request = nm.CreateAndInsertRequestI(guid, componentName, methodName, args);
            request.Header.DateSent = DateTime.UtcNow;
             
            var jsonArgs = JsonConvert.SerializeObject(args);
            //LogDebug(jsonArgs);
            var requestParams =  new EventRequestParams()
            {
                GUID = guid,
                ComponentName = componentName,
                MethodName = methodName,
                MethodArgs = jsonArgs,
            };
            var data = JsonConvert.SerializeObject(requestParams);
            //LogDebug(data);
            EngineService.GetInstance().TriggerEvent(NetworkManager.NetworkRequestListenerHandle, guid, componentName, methodName, JsonConvert.SerializeObject(args));

            NetworkResponse response = await MakeRequest(guid, request);

            return response;
        }

        private async Task<NetworkResponse> MakeRequest(string guid, NetworkRequest request)
        {
            var nm = NetworkManager.GetInstance(); 
            //LogDebug("1");
            NetworkResponse response = null;
            while (request.Ticks < _timeout)
            {
                if (nm.HasResponses())
                {
                    if (nm.HasResponse(guid))
                    {
                        response = nm.GetResponse(guid);
                        break;
                    }
                }
                request.Ticks++;
                await Delay(0);
            }
            if (response == null)
            {
                nm.CreateAndInsertResponse(guid, null);
            }
            nm.CompleteRequest(guid);
            return response;
        }
         

        public static void RequestScriptStop(string scriptName)
        {
            var em = InternalManager.GetInstance();
            var extensions = em.GetExtensions();
            var sm = InternalManager.GetInstance();
            var wrapper = sm.GetScript(scriptName);
            if (wrapper == null) return;
            wrapper.KillAllInstances();
        }

        protected void LogDebug(object data)
        {
            var d = _log.LogDebug($"[{_type}] " + data);
            if (EngineConfiguration.IsClient)
                ExecuteEngineMethodServer(EngineConstraints.LogDebug, data.ToString());
            F8Console.WriteLine(d);
        }
        protected void LogWarn(object data)
        {
            var d = _log.LogWarn($"[{_type}] " + data);
            if (EngineConfiguration.IsClient)
                ExecuteEngineMethodServer(EngineConstraints.LogWarn, data.ToString());
            F8Console.WriteLine(d);
        }
        protected void LogError(object data)
        {
                var d = _log.LogError($"[{_type}] " + data);
            if (EngineConfiguration.IsClient)
                ExecuteEngineMethodServer(EngineConstraints.LogError, data.ToString());
            F8Console.WriteLine(d);
        }

        public void DumpLog()
        {
            F8Console.WriteLine("--------------------------- Begin log dump ------------------------------------");
            foreach (var item in _log.GetLog())
            {
                F8Console.WriteLine(item);
            }
            F8Console.WriteLine("--------------------------- End log dump ------------------------------------");
        }


        protected async Task Delay(int ms)
        {
            if(_source == null)
                _source = EngineService.GetInstance();
            await _source.Delay(ms);
        }
    }

}
