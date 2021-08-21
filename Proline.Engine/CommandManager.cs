using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Engine
{
    internal class CommandManager
    {
        private static CommandManager _instance;
        private Dictionary<string, ComponentCommand> _commands;

        CommandManager()
        {
            _commands = new Dictionary<string, ComponentCommand>();
        }

        internal static CommandManager GetInstance()
        {
            if (_instance == null)
                _instance = new CommandManager();
            return _instance;
        }

        internal void RegisterCommand(ComponentCommand command)
        {
            Debugger.LogDebug("Registered " + command.Type + " " + command.Name);
            _commands.Add(command.Name, command);
        }

        internal ComponentCommand GetCommand(string command)
        {
            throw new NotImplementedException();
        }

        internal void UnregisterCommand(ComponentCommand command)
        {
            _commands.Remove(command.Name);
        }

        internal IEnumerable<ComponentCommand> GetCommands()
        {
            return _commands.Values;
        }
    }
}
