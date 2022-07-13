using CitizenFX.Core;
using Proline.Resource.Eventing;
using ProlineCore;
using ProlineCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProlineCore.Events
{
    internal partial class PlayerConnectedEvent : LoudEvent
    {
        public PlayerConnectedEvent() : base(PLAYERCONNECTEDHANDLER)
        {
        }

        private static PlayerConnectedEvent _event;
        public const string PLAYERCONNECTEDHANDLER = "PlayerConnectedHandler";

        public static void SubscribeEvent()
        {
            if (_event == null)
            {
                _event = new PlayerConnectedEvent();
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

        public static void InvokeEvent(string username)
        {
            var events = new PlayerConnectedEvent();
            events.Invoke(null, username);
        }

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
