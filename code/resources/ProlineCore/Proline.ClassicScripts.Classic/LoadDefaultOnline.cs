using CitizenFX.Core;
using CitizenFX.Core.Native;
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
            CharacterOutfit _characterPedOutfitM = CreateDefaultOutfit();
            if (!MData.API.HasSaveLoaded())
            {
                PlayerCharacter character = CreateNewCharacter();
                MScripting.MScriptingAPI.StartNewScript("LoadDefaultStats");
                while (MScripting.MScriptingAPI.GetInstanceCountOfScript("LoadDefaultStats") > 0)
                {
                    await BaseScript.Delay(1);
                }
            }


            var components = _characterPedOutfitM.Components;
            for (int i = 0; i < components.Length; i++)
            {
                var component = components[i];
                API.SetPedComponentVariation(Game.PlayerPed.Handle, i, component.ComponentIndex, component.ComponentTexture, component.ComponentPallet);
            } 
        }

        private static PlayerCharacter CreateNewCharacter()
        {
            var character = new PlayerCharacter(Game.PlayerPed.Handle);
            character.Stats = new CharacterStats();
            return character;
        }

        private static CharacterOutfit CreateDefaultOutfitF()
        {
            var _characterPedOutfitF = new CharacterOutfit
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

        private static CharacterOutfit CreateDefaultOutfit()
        {
            return new CharacterOutfit
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
