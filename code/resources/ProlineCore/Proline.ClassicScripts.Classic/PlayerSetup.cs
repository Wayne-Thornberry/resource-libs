using CitizenFX.Core;
using CitizenFX.Core.Native;
using Newtonsoft.Json;
using Proline.CFXExtended.Core;
using Proline.ClassicOnline.GCharacter.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.SClassic
{
    public class PlayerSetup
    {
        public async Task Execute(object[] args, CancellationToken token)
        {
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

            var id = "PlayerInfo";
            if (!MData.API.DoesDataFileExist(id))
            {
                MData.API.CreateDataFile();
                MData.API.AddDataFileValue("PlayerHealth", Game.PlayerPed.Health);
                MData.API.AddDataFileValue("PlayerPosition", JsonConvert.SerializeObject(Game.PlayerPed.Position));
                MData.API.AddDataFileValue("BankBalance", 0);
                MData.API.AddDataFileValue("WalletBalance", 0);
                MData.API.SaveDataFile(id);
                MDebug.MDebugAPI.LogDebug(id + " Created and saved");
            }



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

            id = "PlayerOutfit";
            if (!MData.API.DoesDataFileExist(id))
            {
                MData.API.CreateDataFile();
                var outfit = new CharacterOutfit();
                outfit.Components = list;
                var json = JsonConvert.SerializeObject(outfit);
                MData.API.AddDataFileValue("CharacterOutfit", json);
                MData.API.SaveDataFile(id);
                MDebug.MDebugAPI.LogDebug(id + " Created and saved");
            }

            id = "PlayerStats";
            if (!MData.API.DoesDataFileExist(id))
            {
                MData.API.CreateDataFile();
                MData.API.AddDataFileValue("MP0_WALLET_BALANCE", stat.GetValue());
                MData.API.AddDataFileValue("BANK_BALANCE", stat2.GetValue());
                MData.API.SaveDataFile(id);
                MDebug.MDebugAPI.LogDebug(id + " Created and saved");
            }

        }
    }
}
