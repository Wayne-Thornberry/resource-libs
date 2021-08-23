﻿

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
    public static class EngineAccess
    {
        private static int _timeout = 1000;

        //internal static async Task<NetworkResponse> ExecuteComponentAPI(AbstractComponent component, string methodName, params object[] args)
        //{ 
        //    return await ExecuteServerMethod(component.Name, methodName, args);
        //}

        public static void TriggerComponentEvent(string componentName, string eventName, params object[] args)
        {
            var cm = InternalManager.GetInstance();
            var components = cm.GetComponent(componentName);
            components.TriggerComponentEvent(eventName, args);
        }

        public static async Task TriggerEngineEvent(string eventName, params object[] args)
        {
            var cm = InternalManager.GetInstance();
            var components = cm.GetComponents();
            //Debugger.LogDebug("Engine event triggered " + eventName);
            foreach (var item in components)
            {
                item.OnEngineEvent(eventName, args);
            }
        }

        public static async Task<T> ExecuteComponentAPI<T>(string methodName, params object[] args)
        {
            var response = await ExecuteEngineMethodServerI("ExecuteComponentControl", "", methodName, args);
            if (response.Result.ResultValue == null) return default;
            return (T)Convert.ChangeType(response.Result.ResultValue, typeof(T));
        }
        public static async Task<T> ExecuteEngineMethodServer<T>(string methodName, params object[] args)
        {
            var response = await ExecuteEngineMethodServerI(methodName, args);
            if (response.Result.ResultValue == null) return default;
            return (T)Convert.ChangeType(response.Result.ResultValue, typeof(T));
        }

        public static async Task ExecuteEngineMethodServer(string methodName, params object[] args)
        {
            var response = await ExecuteEngineMethodServerI(methodName, args);
        }

        public static async Task<T> ExecuteServerMethod<T>(string methodName, params object[] args)
        {
            return await ExecuteComponentAPI<T>("ExecuteComponentControl", "", methodName, args);
        }

        public static async Task<T> ExecuteServerMethod<T>(string componentName, string methodName, params object[] args)
        {
            return await ExecuteComponentAPI<T>("ExecuteComponentControl", componentName, methodName, args);
        } 

        public static IEnumerable<string> GetAllAPIs()
        {
            var apis = InternalManager.GetInstance();
            var x = apis.GetAPIs();
            var l = new List<string>();
            var g = new List<string>();
            foreach (var item in x)
            {
                l.Add(item.Name + ((l.Where(e=>e.Contains(item.Name)).Count() > 0) ? "_" + l.Where(e => e.Contains(item.Name)).Count() : "") + "=" + item.GetHashCode());
            }
            return l;
        }

        internal static async Task<NetworkResponse> ExecuteEngineMethodServerI(string methodName, params object[] args)
        {
            var nm = NetworkManager.GetInstance();
            var guid = Guid.NewGuid().ToString();
            var request = nm.CreateAndInsertRequestI(guid, methodName, args);
            request.Header.DateSent = DateTime.UtcNow;

            var log = new Log();
            var jsonArgs = JsonConvert.SerializeObject(args);
            var requestParams = new EventRequestParams()
            {
                GUID = guid,
                MethodName = methodName,
                MethodArgs = jsonArgs,
            };
            var data = JsonConvert.SerializeObject(requestParams);
            Debugger.LogDebug(data);
            CitizenAccess.GetInstance().TriggerServerEvent(NetworkManager.NetworkRequestListenerHandle, guid, methodName, JsonConvert.SerializeObject(args));
            NetworkResponse response = await MakeRequest(guid, request);

            return response;
        }

        internal static async Task<NetworkResponse> ExecuteServerMethod(string componentName, string methodName, params object[] args)
        {
            var nm = NetworkManager.GetInstance();
            var guid = Guid.NewGuid().ToString();
            var request = nm.CreateAndInsertRequestI(guid, componentName, methodName, args);
            request.Header.DateSent = DateTime.UtcNow;

            var log = new Log();
            var jsonArgs = JsonConvert.SerializeObject(args);
            var requestParams = new EventRequestParams()
            {
                GUID = guid,
                ComponentName = componentName,
                MethodName = methodName,
                MethodArgs = jsonArgs,
            };
            var data = JsonConvert.SerializeObject(requestParams);
            Debugger.LogDebug(data);
            CitizenAccess.GetInstance().TriggerServerEvent(NetworkManager.NetworkRequestListenerHandle, guid, componentName, methodName, JsonConvert.SerializeObject(args));
            NetworkResponse response = await MakeRequest(guid, request);

            return response;
        }

        internal static async Task<NetworkResponse> ExecuteMethod(string componentName, string methodName, params object[] args)
        {
            var nm = NetworkManager.GetInstance();
            var guid = Guid.NewGuid().ToString();
            var request = nm.CreateAndInsertRequestI(guid, componentName, methodName, args);
            request.Header.DateSent = DateTime.UtcNow;
             
            var jsonArgs = JsonConvert.SerializeObject(args);
            Debugger.LogDebug(jsonArgs);
            var requestParams =  new EventRequestParams()
            {
                GUID = guid,
                ComponentName = componentName,
                MethodName = methodName,
                MethodArgs = jsonArgs,
            };
            var data = JsonConvert.SerializeObject(requestParams);
            Debugger.LogDebug(data);
            CitizenAccess.GetInstance().TriggerEvent(NetworkManager.NetworkRequestListenerHandle, guid, componentName, methodName, JsonConvert.SerializeObject(args));

            NetworkResponse response = await MakeRequest(guid, request);

            return response;
        }

        private static async Task<NetworkResponse> MakeRequest(string guid, NetworkRequest request)
        {
            var nm = NetworkManager.GetInstance(); 
            Debugger.LogDebug("1");
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
                await CitizenAccess.GetInstance().Delay(0);
            }
            if (response == null)
            {
                nm.CreateAndInsertResponse(guid, null);
            }
            nm.CompleteRequest(guid);
            return response;
        }

        public static int StartNewScript(string scriptName, params object[] args)
        {
            var em = InternalManager.GetInstance();
            var extensions = em.GetExtensions();
            var sm = InternalManager.GetInstance();
            var wrapper = sm.GetScript(scriptName);
            if (wrapper == null) return -1;
            return wrapper.StartNew(args); 
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
    }

}
