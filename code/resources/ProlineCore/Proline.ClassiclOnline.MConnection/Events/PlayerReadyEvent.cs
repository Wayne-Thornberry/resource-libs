using CitizenFX.Core;
using Proline.Resource.Eventing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.MConnection.Events
{
    internal partial class PlayerReadyEvent : LoudEvent
    {
        public PlayerReadyEvent() : base(PLAYERREADYHANDLER)
        {
        }

        private static PlayerReadyEvent _event;
        public const string PLAYERREADYHANDLER = "PlayerReadyHandler";

        public static void SubscribeEvent()
        {
            if (_event == null)
            {
                _event = new PlayerReadyEvent();
                _event.Subscribe();
            }
        }

        internal static void InvokeEvent()
        {
            var playerEvent = new PlayerReadyEvent();
            playerEvent.Invoke(Game.Player.Name);
        }
    }
}
