using CitizenFX.Core;
using Proline.Resource.Console;
using Proline.Resource.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.MScripting
{
    internal class ScriptContainer
    {
        private Log _log => new Log();
        private int _id => _scriptTask == null ? -1 : _scriptTask.Id;
        private int _status;
        private Type _type;
        private string _name;
        private object _instance;
        private object[] _args;
        private Task _scriptTask;
        private CancellationToken _token;
        private MethodInfo _eMethod;
        private int _terminationCode;

        public ScriptContainer(object instance, object[] args = null)
        {
            _type = instance.GetType();
            _name = _type.Name;
            _instance = instance;
            _args = args;
            _token = new CancellationToken();
            _status = 0;
        }

        private void Load()
        {
            if (_status == 0)
            {
                var type = _instance.GetType();
                _eMethod = type.GetMethod("Execute");
                _status = 1;
            }
        }

        public int Start()
        {
            if (_status == 0)
            {
                Load();
            }
            if (_status == 1)
            {
                _scriptTask = new Task(async () =>
                {
                    try
                    {
                        EConsole.WriteLine(_log.Debug(string.Format("{0} Script Started", _name, _terminationCode)));
                        var task = (Task) _eMethod.Invoke(_instance, new object[] { _args, _token });
                        while (!task.IsCompleted)
                            await BaseScript.Delay(0);

                        if (task.Exception != null)
                        {
                            throw task.Exception;
                        }
                        _terminationCode = 0;
                    }
                    catch (ScriptTerminatedException e)
                    {
                        _terminationCode = 2;
                    }
                    catch (Exception e)
                    {
                        _terminationCode = 1;
                        _log.Error(e.ToString());
                    }
                    finally
                    {
                        EConsole.WriteLine(_log.Debug(string.Format("{0} Script Finished, Termination Code: {1}", _name, _terminationCode)));
                        BaseScript.TriggerEvent("ScriptTerminatedHandler", _type.Name);
                        BaseScript.TriggerServerEvent("ScriptTerminatedHandler", _type.Name);
                    }
                }, _token);
                _scriptTask.Start();
                BaseScript.TriggerEvent("ScriptStartedHandler", _type.Name);
                BaseScript.TriggerServerEvent("ScriptStartedHandler", _type.Name);
                _status = 2;
                return _scriptTask.Id;
            }
            return -1;
        }

        public void Terminate()
        {
            if (_status == 2)
            {
            }
        }
    }
}
