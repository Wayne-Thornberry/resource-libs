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
        private string _command;
        private bool _isRegistered;

        public ResourceCommand(string commandName)
        {
            _command = commandName;
        }

        public void RegisterCommand()
        {
            if (!_isRegistered)
            {
                API.RegisterCommand(_command, GetCommandHandler(), false);
                _isRegistered = true;
            }
        }

        protected abstract Delegate GetCommandHandler();
    }
}
