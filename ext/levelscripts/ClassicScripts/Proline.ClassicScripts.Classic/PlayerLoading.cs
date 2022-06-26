using CitizenFX.Core;
using Newtonsoft.Json;
using Proline.ClassicOnline.MGame;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Proline.ClassicOnline.MData;
using Proline.ClassicOnline.MDebug;
using CitizenFX.Core.Native;
using Proline.ClassicOnline.MGame.Data;
using Proline.Resource;
using Proline.CFXExtended.Core;

namespace Proline.LevelScripts.Classic
{
    internal class PlayerLoading
    {
        public async Task Execute(object[] args, CancellationToken token)
        {

            if (!MDataAPI.IsSaveInProgress())
            {
                MDebugAPI.LogDebug("Load Request put in");
                await MDataAPI.LoadFile(16); // Sends a load request to the server
                if (MDataAPI.IsFileLoaded())
                { 
                    Game.PlayerPed.Health = MDataAPI.GetFileValue<int>("PlayerHealth");
                    Game.PlayerPed.Position = MDataAPI.GetFileValue<Vector3>("PlayerPosition");
                    var x = MDataAPI.GetFileValue<int>("MP0_WALLET_BALANCE");
                    var y = MDataAPI.GetFileValue<int>("BANK_BALANCE");
                    var stat = MPStat.GetStat<long>("MP0_WALLET_BALANCE");
                    var stat2 = MPStat.GetStat<long>("BANK_BALANCE");
                    stat.SetValue(x);
                    stat2.SetValue(y); 
                    var outfit = MDataAPI.GetFileValue<PedOutfit>("CharacterOutfit");
                    var components = outfit.Components;
                    for (int i = 0; i < components.Length; i++)
                    {
                        var component = components[i];
                        API.SetPedComponentVariation(Game.PlayerPed.Handle, i, component.ComponentIndex, component.ComponentTexture, component.ComponentPallet);

                    }
                    if (MDataAPI.DoesValueExist("PersonalVehicle"))
                    {
                        var pv = MDataAPI.GetFileValue<PersonalVehicle>("PersonalVehicle");
                        var vehicle = await World.CreateVehicle(pv.ModelHash, pv.LastPosition);
                        vehicle.PlaceOnNextStreet();
                        var blip = vehicle.AttachBlip();
                        //blip.IsFlashing = true;
                    }


                    if (MDataAPI.DoesValueExist("PersonalWeapons"))
                    {
                        var list = MDataAPI.GetFileValue<List<PersonalWeapon>>("PersonalWeapons");
                        foreach (var item in list)
                        {
                            Console.WriteLine(item.Hash);
                            Game.PlayerPed.Weapons.Give((WeaponHash)item.Hash, item.AmmoCount, true, true);
                        }
                    }
                }
                else
                { 
                    MDebugAPI.LogDebug("No save file to load from");
                }

            }

        }
    }
}
