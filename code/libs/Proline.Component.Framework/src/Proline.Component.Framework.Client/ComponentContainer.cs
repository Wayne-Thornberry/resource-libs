using System.Threading.Tasks; 
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Reflection;
using System;
using System.Linq;
using Proline.Resource.Component.Interaction;
using Proline.Resource.Client.Logging;

namespace Proline.Resource.Client.Component
{
    public class ComponentContainer
    {
        private static ComponentContainer _instance; 
        public static ComponentContainer GetInstance()
        {
            if (_instance == null)
                _instance = new ComponentContainer();
            return _instance;
        }

        public ComponentFlag ComponentFlags { get; }

        private Log _log => Logger.GetInstance().GetLog();
        private bool _hasClientSide;
        private bool _hasServerSide;
        private string _componentName;


        private ComponentType _componentType; 
        //private List<ComponentApiController> _controllers;
        //private List<ComponentHandler> _handlers;  
        private NetworkListener _listener;

        private ComponentContainer()
        { 
            //_controllers = new List<ComponentApiController>();
            //_handlers = new List<ComponentHandler>();
            _listener = new NetworkListener();
        }
        public ComponentType GetComponentType()
        {
            return _componentType;
        }


        internal void SetHasClientSide(bool v)
        {
            _hasClientSide = v;
        }

        internal void SetHasServerSide(bool v)
        {
            _hasServerSide = v;
        }

        internal void SetComponentType(ComponentType type)
        {
            _componentType = type;
        }

        public string GetComponentName()
        {
            return _componentName;
        }

        internal void SetComponentName(string componentName)
        {
            _componentName = componentName;
        }
 
    }
}
