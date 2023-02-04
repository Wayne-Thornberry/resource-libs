using CitizenFX.Core.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Resource.Framework
{
    public abstract class ResourceCommand
    {
        public static string PREFIX = "cc:";

        private string _command;
        private bool _isRestricted;
        private bool _isRegistered;

        public ResourceCommand(string commandName, bool restricted = false)
        { 
            _command = string.Format(PREFIX + "{0}", commandName).ToLower();
            _isRestricted = restricted;
        }

        public void RegisterCommand()
        {
            if (!_isRegistered)
            {
                API.RegisterCommand(_command, new Action<int, List<object>, string>(OnCommandExecute), _isRestricted);
                _isRegistered = true;
            }
        }

        private void OnCommandExecute(int arg1, List<object> arg2, string arg3)
        {
            Console.WriteLine(String.Format("Command {0} executed with {1} args, raw: {2}", _command, arg2.Count, arg3));
            OnCommandExecute(arg2.ToArray());
        }

        protected abstract void OnCommandExecute(params object[] args);
         
    }
}
