using CitizenFX.Core;
using Proline.Resource;
using Proline.Resource.Configuration;
using Proline.Resource.Framework;
using Proline.Resource.Logging;
using Proline.ServerAccess.IO.Actions;
using ProlineCore.Events;
using ProlineCore.Events.Special;
using ProlineCore.Internal;
using System.Reflection;
using System.Threading.Tasks;
using Console = Proline.Resource.Console;

namespace ProlineCore
{
    public class MainScript : ResourceScript
    {
        public override async Task OnLoad()
        {
            ReadFileAction.SubscribeEvent();
            WriteFileAction.SubscribeEvent();
            ConsoleWriteLineAction.SubscribeEvent();
            ConsoleWriteAction.SubscribeEvent();
            LoadResources();
        }

        public override async Task OnStart()
        {

        }

        public override async Task OnUpdate()
        {
            //var instance = ConnectionQueue.GetInstance();
            //while (instance.Count > 0)
            //{
            //    var connection = instance.Dequeue();
            //    PlayerConnectingEvent.InvokeEvent(connection.Player.Name);
            //    connection.Defferal.done();
            //    PlayerConnectedEvent.InvokeEvent(connection.Player.Name);
            //}


            //var disconnectionQueue = DisconnectionQueue.GetInstance();
            //while (disconnectionQueue.Count > 0)
            //{
            //    var connection = disconnectionQueue.Dequeue();
            //    PlayerDroppedEvent.InvokeEvent(connection.Player.Name);
            //}

            //var dq = DownloadQueue.GetInstance();
            //while (dq.Count > 0)
            //{
            //    var player = dq.Dequeue();
            //    await API.PullSaveFromCloudAsync(player, player.Name);
            //    PlayerReadyEvent.InvokeEvent();
            //}

            //var uq = UploadQueue.GetInstance();
            //while (uq.Count > 0)
            //{
            //    var save = uq.Dequeue();
            //    await API.SendSaveToCloudAsync(save.Owner, save);
            //}
        }

        private void LoadResources()
        {
            Console.WriteLine("Loading Resources...");
            foreach (var item in Configuration.GetSection<string[]>("Resources"))
            {
                Assembly.Load(item);
            }
            Console.WriteLine("Loaded Resources");
        }
    }
}
