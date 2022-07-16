using CitizenFX.Core;
using CitizenFX.Core.Native;
using Proline.ClassicOnline.GCharacter;
using Proline.ClassicOnline.GCharacter.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.SClassic
{
    public class LoadDefaultOnline
    {
        public async Task Execute(object[] args, CancellationToken token)
        {
            await Game.Player.ChangeModel(new Model(1885233650)); 
            if (!MData.API.HasSaveLoaded())
            {
                PlayerCharacter character = CreateNewCharacter();
                MScripting.MScriptingAPI.StartNewScript("LoadDefaultStats");
                while (MScripting.MScriptingAPI.GetInstanceCountOfScript("LoadDefaultStats") > 0)
                {
                    await BaseScript.Delay(1);
                }
                CharacterGlobals.Character = character;
            }

            MGame.MGameAPI.SetPedOutfit("mp_m_defaultoutfit", Game.PlayerPed.Handle);
        }

        private static PlayerCharacter CreateNewCharacter()
        {
            var character = new PlayerCharacter(Game.PlayerPed.Handle);
            character.Stats = new CharacterStats();
            return character;
        }
    
    }
}
