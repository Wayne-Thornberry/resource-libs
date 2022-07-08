using CitizenFX.Core;
using Proline.ClassicOnline.MData.Entity;
using Proline.ClassicOnline.MData.Server.Internal;
using Proline.Resource.Eventing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.MData
{
    internal partial class PlayerConnectedEvent : LoudEvent
    {
        protected override object OnEventTriggered(Player player, params object[] args)
        {
            var playerName = args[0].ToString();
            // Lookup player 
            var sm = DataFileManager.GetInstance();
            API.PullSaveFromLocal(player);
            if (!sm.DoesPlayerHaveSave(player))
            { 
                sm.CreateSave(player);
                var dq = DownloadQueue.GetInstance();
                dq.Enqueue(player);
            }
            return null;
        }
    }
}
