﻿using Proline.Resource.Eventing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.MScripting
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
    }
}