using Newtonsoft.Json;
using Proline.Resource.IO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CInputOutput.Scripts
{
    public class InitCore
    {
        private ConsoleWriteAction _consoleWriteAction;
        private ConsoleWriteAction _consoleWriteLineAction;
        private ConsoleWriteAction _readFileAction;
        private ConsoleWriteAction _writeFileAction;

        public async Task Execute()
        { 
            try
            {
                _consoleWriteAction = new ConsoleWriteAction();
                _consoleWriteLineAction = new ConsoleWriteAction();
                _readFileAction = new ConsoleWriteAction();
                _writeFileAction = new ConsoleWriteAction();

                _consoleWriteAction.Subscribe();
                _consoleWriteLineAction.Subscribe();
                _readFileAction.Subscribe();
                _writeFileAction.Subscribe();
            }
            catch (System.Exception e)
            {

            } 
        }


    }
}
