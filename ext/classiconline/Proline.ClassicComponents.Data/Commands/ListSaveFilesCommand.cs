using CitizenFX.Core;
using Proline.ClassicOnline.MData.Internal;
using Proline.Modularization.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console = Proline.Resource.Console;

namespace Proline.ClassicOnline.MData.Commands
{
    public class ListSaveFilesCommand : ModuleCommand
    {
        public ListSaveFilesCommand() : base("ListSaveFiles")
        {
        }

        protected override void OnCommandExecute(params object[] args)
        {
            var sm = DataFileManager.GetInstance();
            var save = sm.GetSave(Game.Player);
            foreach (var saveFile in save.GetSaveFiles())
            {
                Console.WriteLine(saveFile.Identifier);
            }
        } 
    }
}
