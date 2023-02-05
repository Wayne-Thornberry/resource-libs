using CDataStream.Internal;
using CitizenFX.Core;

using Proline.Resource.Framework;

namespace CDataStream.Commands
{
    public class SelectSaveFileCommand : ResourceCommand
    {
        public SelectSaveFileCommand() : base("SelectSaveFile")
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
                sm.ActiveFile = saveFile;
            }
        }
    }
}
