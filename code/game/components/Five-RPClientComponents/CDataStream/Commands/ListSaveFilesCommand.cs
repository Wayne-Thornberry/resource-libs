using CDataStream.Internal;
using CitizenFX.Core;
using Proline.Resource.Framework;
using Console = Proline.Resource.Console;

namespace CDataStream.Commands
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
                Console.WriteLine(string.Format("{0},{1},{2}", saveFile.Name, saveFile.LastChanged, saveFile.HasUpdated));
            }
        }
    }
}
