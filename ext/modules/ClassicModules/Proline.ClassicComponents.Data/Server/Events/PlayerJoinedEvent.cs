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
    internal partial class PlayerJoinedEvent : LoudEvent
    {
        protected override object OnEventTriggered(Player player, params object[] args)
        {
            var sm = DataFileManager.GetInstance();
            if (!sm.DoesPlayerHaveSave(player))
            {
                API.PullSaveFromLocal(player);
            }

            var dq = DownloadQueue.GetInstance();
            dq.Enqueue(player);
            return null;
        }

    }
}
