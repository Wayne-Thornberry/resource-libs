using CitizenFX.Core;
using CitizenFX.Core.UI;
using Newtonsoft.Json;
using Proline.ClassicOnline.MGame;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Proline.CFXExtended.Core;
using CitizenFX.Core.Native;
using Proline.ClassicOnline.MGame.Data;

namespace Proline.ClassicOnline.SClassic
{
    public class SaveNow
    {

        public async Task Execute(object[] args, CancellationToken token)
        {
            Screen.LoadingPrompt.Show("Saving...", LoadingSpinnerType.SocialClubSaving);
            var stat = MPStat.GetStat<long>("MP0_WALLET_BALANCE");
            var stat2 = MPStat.GetStat<long>("BANK_BALANCE");

            var list = new OutfitComponent[12];
            for (int i = 0; i < list.Length; i++)
            {
                var component = list[i];
                component.ComponentIndex = API.GetPedDrawableVariation(Game.PlayerPed.Handle, i);
                component.ComponentPallet = API.GetPedPaletteVariation(Game.PlayerPed.Handle, i);
                component.ComponentTexture = API.GetPedTextureVariation(Game.PlayerPed.Handle, i);

            }

            var outfit = new PedOutfit();
            outfit.Components = list;
            var json = JsonConvert.SerializeObject(outfit);
            var id = "PlayerInfo";
            MData.API.CreateDataFile();
            MData.API.AddDataFileValue("PlayerHealth", Game.PlayerPed.Health);
            MData.API.AddDataFileValue("PlayerPosition", JsonConvert.SerializeObject(Game.PlayerPed.Position));
            MData.API.SaveDataFile(id);


            id = "PlayerOutfit";
            MData.API.CreateDataFile();
            MData.API.AddDataFileValue("CharacterOutfit", json);
            MData.API.SaveDataFile(id);


            id = "PlayerStats";
            MData.API.CreateDataFile();
            MData.API.AddDataFileValue("MP0_WALLET_BALANCE", stat.GetValue());
            MData.API.AddDataFileValue("BANK_BALANCE", stat2.GetValue());
            MData.API.SaveDataFile(id);

            //ClassicOnline.MData.API.DoesDataFileExist("PlayerVehicle");
            //if (ClassicOnline.MData.API.DoesDataFileValueExist("VehicleHash"))
            //{
            //    var pv = (VehicleHash)ClassicOnline.MData.API.GetDataFileValue<uint>("VehicleHash");
            //    var position = ClassicOnline.MData.API.GetDataFileValue<Vector3>("VehiclePosition");
            //    var vehicle = await World.CreateVehicle(new Model(pv), position);
            //    vehicle.PlaceOnNextStreet();
            //    vehicle.IsPersistent = true;
            //    if (vehicle.AttachedBlips.Length == 0)
            //        vehicle.AttachBlip();
            //    //blip.IsFlashing = true;
            //}

            await MData.API.SendSaveToCloud();
            await BaseScript.Delay(1000);
            Screen.LoadingPrompt.Hide();
        }
    }
}
