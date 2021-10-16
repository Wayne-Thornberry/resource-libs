using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.UI;
using Proline.CFXExtended.Core;
using Proline.CScripting.Framework;

namespace Proline.Classic.LevelScripts.Object
{
    public class ObBankTruck : ScriptInstance
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
                while (true)
                {
                    Screen.DisplayHelpTextThisFrame("Press ~INPUT_CONTEXT~ to recive money");
                    if (CitizenFX.Core.Game.IsControlJustPressed(0, Control.Context))
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
