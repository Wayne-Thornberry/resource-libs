using CitizenFX.Core;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Proline.ClassicOnline.MDebug;
using CitizenFX.Core.Native;

using Proline.Resource;
using Proline.CFXExtended.Core;
using Proline.ClassicOnline.GScripting;
using Proline.ClassicOnline.GCharacter;
using Proline.ClassicOnline.GCharacter.Data;
using Proline.ClassicOnline.MGame;

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
                MScripting.MScriptingAPI.StartNewScript("LoadDefaultOnline");
                while (MScripting.MScriptingAPI.GetInstanceCountOfScript("LoadDefaultOnline") > 0)
                {
                    await BaseScript.Delay(1);
                }
                await MData.API.PullSaveFromCloud(); // Sends a load request to the server
                if (MData.API.HasSaveLoaded())
                {
                    PlayerCharacter character = CreateNewCharacter();
                    CharacterGlobals.Character = character; 

                    if (MData.API.DoesDataFileExist("PlayerInfo"))
                    {
                        MData.API.SelectDataFile("PlayerInfo");
                        character.Health = MData.API.GetDataFileValue<int>("PlayerHealth");
                        character.Position = MData.API.GetDataFileValue<Vector3>("PlayerPosition");
                        character.BankBalance = MData.API.GetDataFileValue<long>("BankBalance");
                        character.WalletBalance = MData.API.GetDataFileValue<long>("WalletBalance");
                    }

                    if (MData.API.DoesDataFileExist("PlayerStats"))
                    {
                        MData.API.SelectDataFile("PlayerStats");
                        var x = MData.API.GetDataFileValue<int>("MP0_WALLET_BALANCE");
                        var y = MData.API.GetDataFileValue<int>("BANK_BALANCE");
                        character.Stats.SetStat("WALLET_BALANCE", x);
                        character.Stats.SetStat("BANK_BALANCE", y);
                    }
                     
                    if (MData.API.DoesDataFileExist("CharacterLooks"))
                    {
                        MData.API.SelectDataFile("CharacterLooks");
                        var mother = MData.API.GetDataFileValue<int>("Mother");
                        var father = MData.API.GetDataFileValue<int>("Father");
                        var resemblence = MData.API.GetDataFileValue<float>("Resemblance");
                        var skintone = MData.API.GetDataFileValue<float>("SkinTone");

                        MGameAPI.SetPedLooks(Game.PlayerPed.Handle, new CharacterLooks()
                        {
                            Mother = mother,
                            Father = father,
                            Resemblence = resemblence * 0.1f,
                            SkinTone = skintone * 0.1f,
                        });
                    }

                    if (MData.API.DoesDataFileExist("PlayerOutfit"))
                    {
                        MData.API.SelectDataFile("PlayerOutfit");
                        var outfit = MData.API.GetDataFileValue<CharacterOutfit>("CharacterOutfit");
                        var components = outfit.Components;
                        for (int i = 0; i < components.Length; i++)
                        {
                            var component = components[i];
                            API.SetPedComponentVariation(Game.PlayerPed.Handle, i, component.ComponentIndex, component.ComponentTexture, component.ComponentPallet);

                        }
                    }

                    if (MData.API.DoesDataFileExist("PlayerVehicle"))
                    { 
                        MData.API.SelectDataFile("PlayerVehicle");
                        if (MData.API.DoesDataFileValueExist("VehicleHash"))
                        {
                            var pv = (VehicleHash) (uint) MData.API.GetDataFileValue<int>("VehicleHash");
                            var position = MData.API.GetDataFileValue<Vector3>("VehiclePosition");
                            var vehicle = await World.CreateVehicle(new Model(pv), Game.PlayerPed.Position);
                            vehicle.PlaceOnNextStreet();
                            vehicle.IsPersistent = true;
                            if (vehicle.AttachedBlips.Length == 0)
                                vehicle.AttachBlip();
                            CharacterGlobals.Character.PersonalVehicle = new CharacterPersonalVehicle(vehicle.Handle);
                        }
                    }

                    if (MData.API.DoesDataFileExist("PlayerWeapon"))
                    {
                        MData.API.SelectDataFile("PlayerWeapon");
                        if (MData.API.DoesDataFileValueExist("WeaponHash"))
                        {
                            var hash = (WeaponHash)MData.API.GetDataFileValue<uint>("WeaponHash");
                            var ammo = MData.API.GetDataFileValue<int>("WeaponAmmo");
                            Game.PlayerPed.Weapons.Give(hash, ammo, true, true);
                        }
                    }

                    MScripting.MScriptingAPI.StartNewScript("LoadStats");

                    Console.WriteLine(ScriptingGlobals.Testing);
                }
                else
                {
                    MDebugAPI.LogDebug("No save file to load from, attempting to create a save...");
                    /// If the player doesnt have basic info, that means the player does not have a save
                    if (!MData.API.DoesDataFileExist("PlayerInfo"))
                    {
                        MScripting.MScriptingAPI.StartNewScript("PlayerSetup");
                        while (MScripting.MScriptingAPI.GetInstanceCountOfScript("PlayerSetup") > 0)
                        {
                            await BaseScript.Delay(1);
                        }
                        MScripting.MScriptingAPI.StartNewScript("SaveNow");
                        while (MScripting.MScriptingAPI.GetInstanceCountOfScript("SaveNow") > 0)
                        {
                            await BaseScript.Delay(1);
                        }
                    }
                    MScripting.MScriptingAPI.StartNewScript("PlayerLoading");
                }

            }

        }

        private static PlayerCharacter CreateNewCharacter()
        {
            var character = new PlayerCharacter(Game.PlayerPed.Handle);
            character.Stats = new CharacterStats();
            return character;
        }
         
    }
}
