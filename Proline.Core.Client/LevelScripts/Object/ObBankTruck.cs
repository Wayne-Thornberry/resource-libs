
using CitizenFX.Core.UI;
using Proline.Freemode;
using Proline.Engine;
using Proline.Engine.Scaleforms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CitizenFX.Core;
using Proline.Core;

namespace Proline.Freemode.LevelScripts.Objects
{
    public class ObBankTruck : LevelScript
    {
        private Blip _blip;

        public override async Task Execute(params object[] args)
        {
            if(args.Length > 0)
            {
                var entityHandle = (int)args[0];
                LogDebug(entityHandle);
                var entity = Entity.FromHandle(entityHandle);
                _blip = entity.AttachBlip();
                var stat = MPStat.GetStat<long>("MP0_WALLET_BALANCE");
                var stat2 = MPStat.GetStat<long>("BANK_BALANCE");
                LogDebug(stat.GetValue());
                LogDebug(stat2.GetValue());
                while (ExampleAPI.IsEntityInActivationRange(entity.Handle))
                {
                    Screen.DisplayHelpTextThisFrame("Press ~INPUT_CONTEXT~ to recive money");
                    if (Game.IsControlJustPressed(0, Control.Context))
                    {
                        stat.SetValue(stat.GetValue() + 1000);
                    }
                    await BaseScript.Delay(0);
                }
            }

            if (_blip != null)
                _blip.Delete();
        }
    }
}
