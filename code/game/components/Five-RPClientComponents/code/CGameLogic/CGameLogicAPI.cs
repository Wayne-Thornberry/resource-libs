using CitizenFX.Core;
using CitizenFX.Core.Native;
using Newtonsoft.Json;
using Console = Proline.Resource.Console;
using Proline.Resource.IO;
using System;
using System.Collections.Generic;
using CGameLogic.Data;
using CGameLogic.Internal;
using CDataStream;
using CDebugActions;

namespace CGameLogic
{

    public class CGameLogicAPI : ICGameLogicAPI
    {

        public void SetPedLooks(int pedHandle, int mother, int father, float parent, float skin)
        {
            var api = new CDebugActionsAPI();
            try
            {
                var looks = new CharacterLooks()
                {
                    Mother = mother,
                    Father = father,
                    Resemblence = parent,
                    SkinTone = skin,
                };
                //if (CharacterGlobals.Character != null)
                //    CharacterGlobals.Character.Looks = looks;
                API.SetPedHeadBlendData(pedHandle, looks.Father, looks.Mother, 0, looks.Father, looks.Mother, 0, looks.Resemblence, looks.SkinTone, 0, true);

                if (looks.Hair != null)
                    API.SetPedHairColor(pedHandle, looks.Hair.Color, looks.Hair.HighlightColor);
                API.SetPedEyeColor(pedHandle, looks.EyeColor);

                if (looks.Overlays != null)
                {
                    for (int i = 0; i < looks.Overlays.Length; i++)
                    {
                        API.SetPedHeadOverlay(pedHandle, i, looks.Overlays[i].Index, looks.Overlays[i].Opacity);
                        API.SetPedHeadOverlayColor(pedHandle, i, looks.Overlays[i].ColorType, looks.Overlays[i].PrimaryColor, looks.Overlays[i].SecondaryColor);
                    }

                }
                if (looks.Features != null)
                {
                    for (int i = 0; i < looks.Features.Length; i++)
                    {
                        API.SetPedFaceFeature(pedHandle, i, looks.Features[i].Value);
                    }
                }
            }
            catch (Exception e)
            {
                api.LogError(e);
            }
        }


        public void SetPedOutfit(string outfitName, int handle)
        {
            var api = new CDebugActionsAPI();
            try
            {
                var outfitJson = ResourceFile.Load($"data/character/outfits/{outfitName}.json");
                var characterPedOutfitM = JsonConvert.DeserializeObject<CharacterOutfit>(outfitJson.GetData());
                var components = characterPedOutfitM.Components;
                for (int i = 0; i < components.Length; i++)
                {
                    var component = components[i];
                    API.SetPedComponentVariation(handle, i, component.ComponentIndex, component.ComponentTexture, component.ComponentPallet);
                }
            }
            catch (Exception e)
            {
                api.LogError(e);
            }
        }

        public void AddValueToBankBalance(object payout)
        {
            throw new NotImplementedException();
        }

        public CharacterLooks GetPedLooks(int pedHandle)
        {
            var api = new CDebugActionsAPI();
            try
            {
                //int x = 0;
                //API.GetPedHeadBlendData(pedHandle,ref x);
                //CDebugActions.EngineAPI.LogDebug(x);
                if (HasCharacter())
                    return Character.PlayerCharacter.Looks;
                else
                    return null;
            }
            catch (Exception e)
            {
                api.LogError(e);
            }
            return null;
        }

        public CharacterStats GetCharacterStats()
        {
            return Character.PlayerCharacter.Stats;
        }

        public void SetCharacter(int character)
        {
            if (CharacterManager.Characters == null)
                return;
            Character.PlayerCharacter = CharacterManager.Characters[character];
        }
        public bool HasCharacter()
        {
            return Character.PlayerCharacter != null;
        }

        public void SetPedGender(int handle, char gender)
        {
            try
            {

            }
            catch (Exception e)
            {

                throw;
            }
        }

        public void SetCharacterBankBalance(long value)
        {
            var api = new CDebugActionsAPI();
            try
            {
                Character.BankBalance = value;
                var bankBalanceStat = MPStat.GetStat<long>("BANK_BALANCE");
                bankBalanceStat.SetValue(Character.BankBalance);
                var id = "PlayerInfo";
                var dataAPI = new CDataStreamAPI();
                if (dataAPI.DoesDataFileExist(id))
                {
                    dataAPI.SelectDataFile(id);
                    if (dataAPI.DoesDataFileValueExist("BankBalance"))
                        dataAPI.SetDataFileValue("BankBalance", Character.BankBalance);
                    else
                        dataAPI.AddDataFileValue("BankBalance", Character.BankBalance);
                }
            }
            catch (Exception e)
            {
                api.LogError(e);
            }
        }

        public long GetCharacterMaxWalletBalance()
        {
            return Character.MaxWalletCapacity;
        }
        public void SetCharacterMaxWalletBalance(int value)
        {
            Character.MaxWalletCapacity = value;
        }


        public bool HasBankBalance(long price)
        {
            return Character.BankBalance > price;
        }



        public void AddValueToBankBalance(long value)
        {
            var api = new CDebugActionsAPI();
            try
            {

                Character.BankBalance += value;
                var bankBalanceStat = MPStat.GetStat<long>("BANK_BALANCE");
                bankBalanceStat.SetValue(Character.BankBalance);
                var id = "PlayerInfo";
                var dataAPI = new CDataStreamAPI();
                if (dataAPI.DoesDataFileExist(id))
                {
                    dataAPI.SelectDataFile(id);
                    if (dataAPI.DoesDataFileValueExist("BankBalance"))
                        dataAPI.SetDataFileValue("BankBalance", Character.BankBalance);
                    else
                        dataAPI.AddDataFileValue("BankBalance", Character.BankBalance);
                }
            }
            catch (Exception e)
            {
                api.LogError(e);
            }
        }


        public void SubtractValueFromBankBalance(long value)
        {
            var api = new CDebugActionsAPI();
            try
            {

                Character.BankBalance -= value;
                var bankBalanceStat = MPStat.GetStat<long>("BANK_BALANCE");
                bankBalanceStat.SetValue(Character.BankBalance);
                var id = "PlayerInfo";
                var dataAPI = new CDataStreamAPI();
                if (dataAPI.DoesDataFileExist(id))
                {
                    dataAPI.SelectDataFile(id);
                    if (dataAPI.DoesDataFileValueExist("BankBalance"))
                        dataAPI.SetDataFileValue("BankBalance", Character.BankBalance);
                    else
                        dataAPI.AddDataFileValue("BankBalance", Character.BankBalance);
                }
            }
            catch (Exception e)
            {
                api.LogError(e);
            }
        }

        public void SetCharacterWalletBalance(long value)
        {
            var api = new CDebugActionsAPI();
            try
            {

                Character.WalletBalance = value;
                var walletBalanceStat = MPStat.GetStat<long>("MP0_WALLET_BALANCE");
                walletBalanceStat.SetValue(Character.WalletBalance);
                var id = "PlayerInfo";
                var dataAPI = new CDataStreamAPI();
                if (dataAPI.DoesDataFileExist(id))
                {
                    dataAPI.SelectDataFile(id);
                    if (dataAPI.DoesDataFileValueExist("WalletBalance"))
                        dataAPI.SetDataFileValue("WalletBalance", Character.WalletBalance);
                    else
                        dataAPI.AddDataFileValue("WalletBalance", Character.WalletBalance);
                }
            }
            catch (Exception e)
            {
                api.LogError(e);
            }
        }

        public void AddValueToWalletBalance(long value)
        {
            var api = new CDebugActionsAPI();
            try
            {

                Character.WalletBalance += value;
                var walletBalanceStat = MPStat.GetStat<long>("MP0_WALLET_BALANCE");
                walletBalanceStat.SetValue(Character.WalletBalance);
                var id = "PlayerInfo";
                var dataAPI = new CDataStreamAPI();
                if (dataAPI.DoesDataFileExist(id))
                {
                    dataAPI.SelectDataFile(id);
                    if (dataAPI.DoesDataFileValueExist("WalletBalance"))
                        dataAPI.SetDataFileValue("WalletBalance", Character.WalletBalance);
                    else
                        dataAPI.AddDataFileValue("WalletBalance", Character.WalletBalance);
                }
            }
            catch (Exception e)
            {
                api.LogError(e);
            }
        }

        public void SubtractValueFromWalletBalance(long value)
        {
            var api = new CDebugActionsAPI();
            try
            {

                Character.WalletBalance -= value;
                var walletBalanceStat = MPStat.GetStat<long>("MP0_WALLET_BALANCE");
                walletBalanceStat.SetValue(Character.WalletBalance);
                var id = "PlayerInfo";
                var dataAPI = new CDataStreamAPI();
                if (dataAPI.DoesDataFileExist(id))
                {
                    dataAPI.SelectDataFile(id);
                    if (dataAPI.DoesDataFileValueExist("WalletBalance"))
                        dataAPI.SetDataFileValue("WalletBalance", Character.WalletBalance);
                    else
                        dataAPI.AddDataFileValue("WalletBalance", Character.WalletBalance);
                }
            }
            catch (Exception e)
            {
                api.LogError(e);
            }
        }


        public long GetCharacterWalletBalance()
        {
            var api = new CDebugActionsAPI();
            try
            {
                return Character.WalletBalance;
            }
            catch (Exception e)
            {
                api.LogError(e);
            }
            return 0;
        }

        public long GetCharacterBankBalance()
        {
            var api = new CDebugActionsAPI();
            try
            {

                return Character.BankBalance;

            }
            catch (Exception e)
            {
                api.LogError(e);
            }
            return 0;
        }

        public bool IsInPersonalVehicle()
        {
            var api = new CDebugActionsAPI();
            try
            {
                if (Character.PersonalVehicle == null)
                    return false;
                return Game.PlayerPed.IsInVehicle() && Game.PlayerPed.CurrentVehicle == Character.PersonalVehicle;
            }
            catch (Exception e)
            {
                api.LogError(e);
            }
            return false;
        }
        public Entity GetPersonalVehicle()
        {
            return Character.PersonalVehicle;
        }

        public void DeletePersonalVehicle()
        {
            foreach (var item in Character.PersonalVehicle.AttachedBlips)
            {
                item.Delete();
            }
            Character.PersonalVehicle.IsPersistent = false;
            Character.PersonalVehicle.Delete();
        }


        public void SetCharacterPersonalVehicle(int handle)
        {
            Character.PersonalVehicle = new CharacterPersonalVehicle(handle);
        }

        public int CreateNewCharacter()
        {
            if (CharacterManager.Characters == null)
                CharacterManager.Characters = new List<PlayerCharacter>();
            var character = new PlayerCharacter(Game.PlayerPed.Handle)
            {
                Stats = new CharacterStats()
            };
            CharacterManager.Characters.Add(character);
            var index = CharacterManager.NextCharacterIndex;
            CharacterManager.NextCharacterIndex++;
            return index;
        }

        public void SetStatLong(string stat, long value)
        {
            if (Character.PlayerCharacter.Stats == null)
                Character.PlayerCharacter.Stats = new CharacterStats();
            Character.PlayerCharacter.Stats.SetStat(stat, value);
        }
    }
}
