using CitizenFX.Core;
using Proline.CFXExtended.Core;
using Proline.Modularization.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Proline.Resource.Framework;
using System.Threading.Tasks;
using Console = Proline.Resource.Console;

namespace Proline.ClassicOnline.MConnection.Commands
{
    public class BuyRandomWeaponCommand : ResourceCommand
    {
        public BuyRandomWeaponCommand() : base("BuyRandomWeapon")
        {
        }

        protected override void OnCommandExecute(params object[] args)
        {
            var stat = MPStat.GetStat<long>("MP0_WALLET_BALANCE");
            var stat2 = MPStat.GetStat<long>("BANK_BALANCE");

            if (stat2.GetValue() > 250)
            {
                stat2.SetValue(stat2.GetValue() - 250);


                Array values = Enum.GetValues(typeof(WeaponHash));
                Random random = new Random();
                WeaponHash randomBar = (WeaponHash)values.GetValue(random.Next(values.Length));
                var ammo = random.Next(1, 250);
                Game.PlayerPed.Weapons.Give(randomBar, ammo, true, true);

                var id = "PlayerWeapon";
                MData.API.CreateDataFile();
                MData.API.AddDataFileValue("WeaponHash", randomBar);
                MData.API.AddDataFileValue("WeaponAmmo", ammo);
                MData.API.SaveDataFile(id);
            }
        }
    }
}
