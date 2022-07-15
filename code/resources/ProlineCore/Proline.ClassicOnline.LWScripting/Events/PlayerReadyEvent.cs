using CitizenFX.Core;
using Proline.ClassicOnline.MScripting.Internal;
using Proline.Resource.Eventing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.MScripting.Events
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
            _event.Invoke(username);
        }

        protected override object OnEventTriggered(params object[] args)
        {
            if(args.Length > 0)
            {
                var username = args[0].ToString();
                Console.WriteLine(username);
                if (Game.Player.Name.Equals(username, StringComparison.CurrentCultureIgnoreCase)) return null;
               
            }
            return null;

        }
    }
}
