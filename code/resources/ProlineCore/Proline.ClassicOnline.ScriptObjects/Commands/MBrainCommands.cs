using CitizenFX.Core;
using Newtonsoft.Json;
using Proline.CFXExtended.Core;
using Proline.ClassicOnline.GCharacter.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.MBrain.Commands
{
    public class MBrainCommands
    {
        [Command("SaveCurrentVehicle")]
        public void SaveCurrentVehicle()
        {
            if (Game.PlayerPed.IsInVehicle())
            {
                var pv = new PersonalVehicle();
                var cv = Game.PlayerPed.CurrentVehicle;
                pv.ModelHash = cv.Model.Hash;
                pv.LastPosition = cv.Position;
                var json = JsonConvert.SerializeObject(pv);
                MData.API.AddDataFileValue("PersonalVehicle", json);
            }
        }

        [Command("BuyRandomWeapon")]
        public void BuyRandomWeapon()
        {
            var stat = MPStat.GetStat<long>("MP0_WALLET_BALANCE");
            var stat2 = MPStat.GetStat<long>("BANK_BALANCE");

            if (stat.GetValue() > 250)
            {
                Array values = Enum.GetValues(typeof(WeaponHash));
                Random random = new Random();
                WeaponHash weaponHash = (WeaponHash)values.GetValue(random.Next(values.Length));
                var pw = new PersonalWeapon();
                pw.AmmoCount = random.Next(1, 100);
                pw.Hash = (uint)weaponHash;
                Game.PlayerPed.Weapons.Give(weaponHash, pw.AmmoCount, true, true);
                var list = new List<PersonalWeapon>();
                if (MData.API.DoesDataFileValueExist("PersonalWeapons"))
                {
                    list = MData.API.GetDataFileValue<List<PersonalWeapon>>("PersonalWeapons");
                }
                list.Add(pw);
                MData.API.AddDataFileValue("PersonalWeapons", JsonConvert.SerializeObject(list));
                stat.SetValue(stat.GetValue() - 250);

            }
        }

    }
}
