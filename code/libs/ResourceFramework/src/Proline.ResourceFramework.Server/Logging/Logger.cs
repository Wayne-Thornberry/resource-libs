using Proline.Resource.Framework.Server.Eventing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Resource.Framework.Server.Logging
{
    public class Logger
    {
        private static Logger _instance2;
        private Log _instance; 

        public Logger()
        { 
            EventManager.AddEventListener("LogHandler", new Action<string, int>(OnLog));
        }
        private void OnLog(string obj, int type)
        {
            switch (type)
            {
                case 0: _instance.Debug(obj); break;
                case 1: _instance.Info(obj); break;
                case 2: _instance.Warn(obj); break;
                case 3: _instance.Error(obj); break;
            }
        }

        public static Logger GetInstance()
        {
            if (_instance2 == null)
                _instance2 = new Logger();
            return _instance2;
        } 

        public Log GetLog()
        {
            if (_instance == null)
                _instance = new Log();
            return _instance;
        }
    }
}
