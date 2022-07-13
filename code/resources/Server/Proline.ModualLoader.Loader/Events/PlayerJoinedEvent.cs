using CitizenFX.Core;
using Proline.Resource.Eventing;
using ProlineCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProlineCore.Events
{
    internal partial class PlayerJoinedEvent : LoudEvent
    {
        public PlayerJoinedEvent() : base(PLAYERJOINEDHANDLER)
        {
        }

        private static PlayerJoinedEvent _event;
        public const string PLAYERJOINEDHANDLER = "PlayerJoinedHandler";

        public static void SubscribeEvent()
        {
            if (_event == null)
            {
                _event = new PlayerJoinedEvent();
                _event.Subscribe();
            }
        }

        public static void UnsubscribeEvent()
        {
            if (_event != null)
            {
                _event.Unsubscribe();
                _event = null;
            }
        }
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
