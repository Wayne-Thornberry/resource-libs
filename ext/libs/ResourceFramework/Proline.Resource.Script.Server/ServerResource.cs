﻿using CitizenFX.Core;
using CitizenFX.Core.Native;
using Newtonsoft.Json;
using Proline.Common.Logging;
using Proline.Resource.Common; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Proline.Resource.Component.Framework;
using Proline.Resource.Component;
using Proline.Resource.Common.Component;
using Proline.Resource.CFX;
using Proline.Resource.Common.Script;
using Proline.Resource.Common.CFX;

namespace Proline.Resource.Script
{
    public class ServerResource : BaseScript, IScriptSource
    {
        private ComponentContainer _component;
        private bool _isSetup;

        public ServerResource()
        {
            Tick += OnTick;
            Resource = new FXResource(API.GetCurrentResourceName());
            Task = new FXTick();
            EventHandler = new FXEventHandler(EventHandlers);
            EventTrigger = new FXEventTrigger();
            Console = new FXConsole();
            Logger.GetInstance().SetOutput(Console);
            ScriptSource.Source = this;
        }

        public IFXConsole Console { get; }
        public IFXEvent EventTrigger { get; }
        public IFXEventHandlerDictionary EventHandler { get; }
        public IFXResource Resource { get; }
        public IFXTask Task { get; }

        private async Task OnTick()
        {
            try
            {
                if (!_isSetup)
                {
                    _component = new ComponentContainer(this);
                    var config = _component.LoadConfig();
                    _component.LoadEnviroment(config.Client, ComponentEnviromentType.CLIENT);
                    _isSetup = true;
                }
            }
            catch (Exception)
            {
                _isSetup = true;
                Tick -= OnTick;
                throw;
            }
        }

        //[EventHandler(NetConstraints.NetworkResponseListenerHandler)]
        //public void NetworkResponseListener(string guid, string value, bool isException)
        //{
        //    //_service.ExecuteEngineMethod("CreateAndInsertResponse", guid, value, isException);
        //}

        //[EventHandler(NetConstraints.NetworkRequestListenerHandler)]
        //public void NetworkRequestListener(string guid, string componentName, string methodName, string methodArgs)
        //{
        //    var args = JsonConvert.DeserializeObject<object[]>(methodArgs);
        //    object result = null;
        //    bool isException = false;
        //    try
        //    {
        //        //result = _service.ExecuteEngineMethod(methodName, args);
        //        if (result != null)
        //        {
        //            if (!result.GetType().IsPrimitive)
        //            {
        //                result = JsonConvert.SerializeObject(result);
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        //Debugger.LogError(e.ToString());
        //        isException = true;
        //        throw;
        //    }
        //    finally
        //    {
        //        TriggerEvent(NetConstraints.NetworkResponseListenerHandler, guid, result, isException);
        //    }
        //}

    }
}
