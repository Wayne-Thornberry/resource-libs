using CitizenFX.Core;
using Newtonsoft.Json;
using Proline.ClassicOnline.MData.Entity;
using Proline.Modularization.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console = Proline.Resource.Console;

namespace Proline.ClassicOnline.MData.Commands
{
    public class OutputActiveSaveFilePropertiesCommand : ModuleCommand
    {
        public OutputActiveSaveFilePropertiesCommand() : base("OutputActiveSaveFileProperties")
        {
        }

        protected override void OnCommandExecute(params object[] args)
        {
            var sm = DataFileManager.GetInstance(); 
            var saveFile = sm.ActiveFile;
            foreach (var item in saveFile.Properties.Keys)
            {
                Console.WriteLine(JsonConvert.SerializeObject(saveFile.Properties));
            }
        } 
    }
}
