using CitizenFX.Core;
using Proline.Resource.Framework;
using System;

namespace Proline.Resource.CPlayerManagment
{
    public class PlayerManagmentContext : ComponentContext
    {
        public override void OnLoad()
        {
            EventManager.InvokeEventV2("playerJoinedSessionHandler");
            //EventManager.InvokeEventV2("playerJoinedSession");

        }
    }
}
