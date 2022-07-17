using CitizenFX.Core;
using Proline.ClassicOnline.MData.Internal;

using Proline.Resource.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console = Proline.Resource.Console;

namespace Proline.ClassicOnline.MData.Commands
{
    public class OutputSaveFileDataCommand : ResourceCommand
    {
        public OutputSaveFileDataCommand() : base("OutputSaveFileData")
        {
        }

        protected override void OnCommandExecute(params object[] args)
        {
            if(args.Length > 0)
            {
                var identifier = args[0].ToString();
                var sm = DataFileManager.GetInstance();
                var save = sm.GetSave(Game.Player);
                var saveFile = save.GetSaveFile(identifier);
                Console.WriteLine(saveFile.GetRawData());
            }
        } 
    }
}
