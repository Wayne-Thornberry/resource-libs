using CitizenFX.Core;
using Proline.CFXExtended.Core;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Proline.Resource.Framework;
using System.Threading.Tasks;
using Console = Proline.Resource.Console;
using Proline.ClassicOnline.MGame;

namespace Proline.ClassicOnline.MConnection.Commands
{
    public class BuyRandomWeaponCommand : ResourceCommand
    {
        public BuyRandomWeaponCommand() : base("BuyRandomWeapon")
        {
        }

        protected override void OnCommandExecute(params object[] args)
        { 
            if (MGameAPI.GetCharacterBankBalance() > 250)
            { 

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

                MGameAPI.SubtractValueFromBankBalance(250);
            }
        }
    }
}
