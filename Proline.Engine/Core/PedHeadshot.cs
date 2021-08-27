
extern alias Client;

using Client.CitizenFX.Core.Native;
using Client.CitizenFX.Core;
using Client.CitizenFX.Core.UI;
using System;
using System.Threading.Tasks;

namespace Proline.Engine.Core
{
    public class PedHeadshot : IDisposable
    {
        public int PedHandle { get; private set; }
        public int Handle { get; private set; }
        public string HeadShotTXT => API.GetPedheadshotTxdString(Handle);
        public bool HasLoaded => API.IsPedheadshotReady(Handle);
        public bool IsValid => API.IsPedheadshotValid(Handle);

        public PedHeadshot(int pedHandle)
        {
            PedHandle = pedHandle;
        }

        public void Dispose()
        {
            if (IsValid && HasLoaded)
                API.UnregisterPedheadshot(Handle);
        }

        public async Task LoadHeadShot()
        {
            API.RegisterPedheadshot(Game.PlayerPed.Handle);
            while (!HasLoaded)
            {
                await BaseScript.Delay(0);
            }
        }

    }
}
