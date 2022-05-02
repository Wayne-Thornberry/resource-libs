using Proline.Resource.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Resource.Framework
{
    public class ResourceEnvironment : IResourceEnvironment
    {
        private Log _log = new Log();
        private Dictionary<string, Type> _scriptTypes;
        private Dictionary<string, Type> _apiTypes;
        private Dictionary<string, Type> _controllerTypes;

        private List<object> _controllers;
        private List<IResourceScript> _scripts;


        public ResourceEnvironment()
        {
            _controllers = new List<object>();
            _scripts = new List<IResourceScript>();

            _scriptTypes = new Dictionary<string, Type>();
            _controllerTypes = new Dictionary<string, Type>();
            _apiTypes = new Dictionary<string, Type>();
        }

        //internal void RegisterScriptType(Type type)
        //{
        //    _scriptTypes.Add(type.Name, type);
        //}

        //internal void RegisterControllerType(Type type)
        //{
        //    _scriptTypes.Add(type.Name, type);
        //}

        //private void InitializeControllers()
        //{
        //    foreach (var type in _controllerTypes.Values)
        //    {
        //        var instance = Activator.CreateInstance(type, null);
        //        var methods = instance.GetType().GetMethods().Where(e => e.GetCustomAttribute<ControllerMethodAttribute>() != null);
        //        foreach (var method in methods)
        //        {
        //            var parameters = method.GetParameters().Select(p => p.ParameterType).ToArray();
        //            var actionType = Expression.GetDelegateType(parameters.Concat(new[] { typeof(void) }).ToArray());
        //            var attribute = method.GetCustomAttribute<ControllerMethodAttribute>();

        //            if (attribute == null) continue;
        //            if (attribute.Type == "Export" || attribute.Type == "Event")
        //            {

        //                if (method.IsStatic)
        //                    _basescript.AddEventListener(attribute.Name, Delegate.CreateDelegate(actionType, method));
        //                else
        //                    _basescript.AddEventListener(attribute.Name, Delegate.CreateDelegate(actionType, instance, method.Name));
        //            }
        //            else if (attribute.Type == "API")
        //            {
        //                if (method.IsStatic)
        //                    _basescript.RegisterExport(Guid.NewGuid().ToString(), Delegate.CreateDelegate(actionType, method));
        //                else
        //                    _basescript.RegisterExport(method.Name, Delegate.CreateDelegate(actionType, instance, method.Name));
        //            }
        //        }
        //        _controllers.Add(instance);
        //    }
        //}

        //public void InitializeResource()
        //{
        //    InitializeScripts();
        //    InitializeControllers();
        //}

        //private void InitializeScripts()
        //{
        //    foreach (var type in _scriptTypes.Values)
        //    {
        //        var instance = (ResourceScript)Activator.CreateInstance(type, null);
        //        _scripts.Add(instance);
        //        _basescript.AddTick(instance.OnTick);
        //    }
        //}


        //public void InitializeFeatures()
        //{ 
        //    LogManager.Initialize(InitializeAPI<IDebugMethods>("DebugAPI"));
        //    NetworkManager.Initialize(InitializeAPI<ITaskMethods>("TaskAPI"), InitializeAPI<IEventMethods>("EventAPI"));
        //}

        //private T InitializeAPI<T>(string apiName)
        //{
        //    if (!_apiTypes.ContainsKey(apiName))
        //        throw new Exception("API " + apiName + " Does not exist");
        //    var api = Activator.CreateInstance(_apiTypes[apiName]);
        //    return (T) api;
        //}

    }
}
