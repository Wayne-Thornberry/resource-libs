using CDataStream.Internal;
using CitizenFX.Core;

using Proline.Resource.Framework;
using Console = Proline.Resource.Console;

namespace CDataStream.Commands
{
    public class OutputSaveFileDataCommand : ResourceCommand
    {
        public OutputSaveFileDataCommand() : base("OutputSaveFileData")
        {
        }

        protected override void OnCommandExecute(params object[] args)
        {
            if (args.Length > 0)
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
