using CitizenFX.Core;
using Proline.ClassicOnline.MData.Internal;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Proline.Resource.Framework;
using System.Threading.Tasks;
using Console = Proline.Resource.Console;

namespace Proline.ClassicOnline.MData.Commands
{
    public class ListSaveFilesCommand : ResourceCommand
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
                Console.WriteLine(String.Format("{0},{1},{2}", saveFile.Name, saveFile.LastChanged, saveFile.HasUpdated));
            }
        } 
    }
}
