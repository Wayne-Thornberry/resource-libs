using CitizenFX.Core;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Proline.ClassicOnline.MDebug;
using CitizenFX.Core.Native;
using Proline.ClassicOnline.MGame.Data;
using Proline.Resource;
using Proline.CFXExtended.Core;
using Proline.ClassicOnline.GScripting;

namespace Proline.ClassicOnline.SClassic
{
    internal class PlayerLoading
    {
        public async Task Execute(object[] args, CancellationToken token)
        {

            if (!MData.API.IsSaveInProgress())
            {
                // attempt to get the player id
                // fish for the save files from the player id
                await LoadDefaultOnlinePlayer();
                await MData.API.PullSaveFromCloud(); // Sends a load request to the server
                if (MData.API.HasSaveLoaded())
                {
                    MData.API.SelectDataFile("PlayerInfo");
                    Game.PlayerPed.Health = MData.API.GetDataFileValue<int>("PlayerHealth");
                    Game.PlayerPed.Position = MData.API.GetDataFileValue<Vector3>("PlayerPosition");

                    MData.API.SelectDataFile("PlayerStats");
                    var x = MData.API.GetDataFileValue<int>("MP0_WALLET_BALANCE");
                    var y = MData.API.GetDataFileValue<int>("BANK_BALANCE");
                    var stat = MPStat.GetStat<long>("MP0_WALLET_BALANCE");
                    var stat2 = MPStat.GetStat<long>("BANK_BALANCE");
                    stat.SetValue(x);
                    stat2.SetValue(y);

                    MData.API.SelectDataFile("PlayerOutfit");
                    var outfit = MData.API.GetDataFileValue<PedOutfit>("CharacterOutfit");
                    var components = outfit.Components;
                    for (int i = 0; i < components.Length; i++)
                    {
                        var component = components[i];
                        API.SetPedComponentVariation(Game.PlayerPed.Handle, i, component.ComponentIndex, component.ComponentTexture, component.ComponentPallet);

                    }

                    MData.API.SelectDataFile("PlayerVehicle");
                    if (MData.API.DoesDataFileValueExist("VehicleHash"))
                    {
                        var pv = (VehicleHash)MData.API.GetDataFileValue<uint>("VehicleHash");
                        var position = MData.API.GetDataFileValue<Vector3>("VehiclePosition");
                        var vehicle = await World.CreateVehicle(new Model(pv), Game.PlayerPed.Position);
                        vehicle.PlaceOnNextStreet();
                        vehicle.IsPersistent = true;
                        if (vehicle.AttachedBlips.Length == 0)
                            vehicle.AttachBlip();
                        //blip.IsFlashing = true;
                    }

                    MData.API.SelectDataFile("PlayerWeapon");
                    if (MData.API.DoesDataFileValueExist("WeaponHash"))
                    {
                        var hash = (WeaponHash)MData.API.GetDataFileValue<uint>("WeaponHash");
                        var ammo = MData.API.GetDataFileValue<int>("WeaponAmmo");
                        Game.PlayerPed.Weapons.Give(hash, ammo, true, true);
                    }

                    Console.WriteLine(ScriptingGlobals.Testing);
                }
                else
                {
                    MDebugAPI.LogDebug("No save file to load from");
                }

            }

        }
        private static async Task LoadDefaultOnlinePlayer()
        {
            await Game.Player.ChangeModel(new Model(1885233650));
            PedOutfit _characterPedOutfitM = CreateDefaultOutfit();
            if (!MData.API.HasSaveLoaded())
            {
                var stat = MPStat.GetStat<long>("MP0_WALLET_BALANCE");
                var stat2 = MPStat.GetStat<long>("BANK_BALANCE");
                stat.SetValue(0);
                stat2.SetValue(0);
                MData.API.AddDataFileValue("MP0_WALLET_BALANCE", stat.GetValue());
                MData.API.AddDataFileValue("BANK_BALANCE", stat.GetValue());

            }


            var components = _characterPedOutfitM.Components;
            for (int i = 0; i < components.Length; i++)
            {
                var component = components[i];
                API.SetPedComponentVariation(Game.PlayerPed.Handle, i, component.ComponentIndex, component.ComponentTexture, component.ComponentPallet);
            }
        }

        private static PedOutfit CreateDefaultOutfitF()
        {
            var _characterPedOutfitF = new PedOutfit
            {
                OutfitName = "",
                Components = new[]
                {
                new OutfitComponent
                {
                    ComponentIndex = 21,
                    ComponentPallet = 0,
                    ComponentTexture = 0
                },
                new OutfitComponent
                {
                    ComponentIndex = 0,
                    ComponentPallet = 0,
                    ComponentTexture = 0
                },
                new OutfitComponent
                {
                    ComponentIndex = 6,
                    ComponentPallet = 0,
                    ComponentTexture = 1
                },
                new OutfitComponent
                {
                    ComponentIndex = 0,
                    ComponentPallet = 0,
                    ComponentTexture = 0
                },
                new OutfitComponent
                {
                    ComponentIndex = 0,
                    ComponentPallet = 0,
                    ComponentTexture = 0
                },
                new OutfitComponent
                {
                    ComponentIndex = 0,
                    ComponentPallet = 0,
                    ComponentTexture = 0
                },
                new OutfitComponent
                {
                    ComponentIndex = 0,
                    ComponentPallet = 0,
                    ComponentTexture = 0
                },
                new OutfitComponent
                {
                    ComponentIndex = 0,
                    ComponentPallet = 0,
                    ComponentTexture = 0
                },
                new OutfitComponent
                {
                    ComponentIndex = 2,
                    ComponentPallet = 0,
                    ComponentTexture = 0
                },
                new OutfitComponent
                {
                    ComponentIndex = 0,
                    ComponentPallet = 0,
                    ComponentTexture = 0
                },
                new OutfitComponent
                {
                    ComponentIndex = 0,
                    ComponentPallet = 0,
                    ComponentTexture = 0
                },
                new OutfitComponent
                {
                    ComponentIndex = 0,
                    ComponentPallet = 0,
                    ComponentTexture = 1
                }
            }
            };

            return _characterPedOutfitF;

        }

        private static PedOutfit CreateDefaultOutfit()
        {
            return new PedOutfit
            {
                OutfitName = "",
                Components = new[]
   {
                    new OutfitComponent
                    {
                        ComponentIndex = 0,
                        ComponentPallet = 0,
                        ComponentTexture = 0
                    },
                    new OutfitComponent
                    {
                        ComponentIndex = 0,
                        ComponentPallet = 0,
                        ComponentTexture = 0
                    },
                    new OutfitComponent
                    {
                        ComponentIndex = 1,
                        ComponentPallet = 0,
                        ComponentTexture = 1
                    },
                    new OutfitComponent
                    {
                        ComponentIndex = 0,
                        ComponentPallet = 0,
                        ComponentTexture = 0
                    },
                    new OutfitComponent
                    {
                        ComponentIndex = 0,
                        ComponentPallet = 0,
                        ComponentTexture = 0
                    },
                    new OutfitComponent
                    {
                        ComponentIndex = 0,
                        ComponentPallet = 0,
                        ComponentTexture = 0
                    },
                    new OutfitComponent
                    {
                        ComponentIndex = 1,
                        ComponentPallet = 0,
                        ComponentTexture = 0
                    },
                    new OutfitComponent
                    {
                        ComponentIndex = 0,
                        ComponentPallet = 0,
                        ComponentTexture = 0
                    },
                    new OutfitComponent
                    {
                        ComponentIndex = 0,
                        ComponentPallet = 0,
                        ComponentTexture = 0
                    },
                    new OutfitComponent
                    {
                        ComponentIndex = 0,
                        ComponentPallet = 0,
                        ComponentTexture = 0
                    },
                    new OutfitComponent
                    {
                        ComponentIndex = 0,
                        ComponentPallet = 0,
                        ComponentTexture = 0
                    },
                    new OutfitComponent
                    {
                        ComponentIndex = 0,
                        ComponentPallet = 0,
                        ComponentTexture = 1
                    }
                }
            };
        }
    }
}
