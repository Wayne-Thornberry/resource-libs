using CitizenFX.Core;
using CitizenFX.Core.Native;
using CitizenFX.Core.UI;
using Proline.EngineFramework.Scripting;
using Proline.ExampleClient.ComponentCharacter;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Proline.ExampleClient.Scripts
{
    public class CharacterCreator : DemandScript
    {

        public CharacterCreator(string name) : base(name)
        {
            CharacterPed = new CharacterPed();
            CharacterStats = new CharacterStats();
            SpawnLocation = new Vector3(405.83f, -997.13f, -99.004f);

            _characterPedOutfitM = new PedOutfit
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
            _characterPedOutfitF = new PedOutfit
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

            Parents = new Dictionary<string, int>
            {
                {"Adrian", 0},
                {"Alex", 1},
                {"Andrew", 2},
                {"Angel", 3},
                {"Anthony", 4},
                {"Benjamin", 5},
                {"Daniel", 6},
                {"Diego", 7},
                {"Ethan", 8},
                {"Evan", 9},
                {"Gabriel", 10},
                {"Isaac", 11},
                {"Joshua", 12},
                {"Juan", 13},
                {"Kevin", 14},
                {"Louis", 15},
                {"Michael", 16},
                {"Noah", 17},
                {"Samuel", 18},
                {"Santiago", 19},
                {"Vincent", 20},
                {"Amelia", 21},
                {"Ashley", 22},
                {"Audrey", 23},
                {"Ava", 24},
                {"Briana", 25},
                {"Camila", 26},
                {"Charlotte", 27},
                {"Elizabeth", 28},
                {"Emma", 29},
                {"Evelyn", 30},
                {"Giselle", 31},
                {"Grace", 32},
                {"Hannah", 33},
                {"Isabella", 34},
                {"Jasmine", 35},
                {"Natalie", 36},
                {"Nicole", 37},
                {"Olivia", 38},
                {"Sophie", 39},
                {"Violet", 40},
                {"Zoe", 41},
                {"Claude", 42},
                {"Niko", 43},
                {"John", 44},
                {"Misty", 45}
            };

            _characterStats = new CharacterStats
            {
                BankBalance = 1500,
                WalletBalance = 500,
                Stamina = 20, // 100
                Strength = 20,
                StealthAbility = 20,
                LungCapacity = 20,
                WheelieAbility = 100,
                SpecialAbility = 100,
                ShootingAbility = 15,
                FlyingAbility = 5,
                XpLevel = 0,
                XpExperience = 0
            };

            _character = new Character
            {
                Id = 0,
                Stats = _characterStats,
                Ped = new CharacterPed
                {
                    Name = "NEW CHARACTER",
                    Looks = new PedLooks(),
                    Outfit = _characterPedOutfitM,
                    LastPosition = new[] { 0f, 0f, 70f },
                    IsDead = false,
                    SpawnLocation = "LAST_LOCATION",
                    Gender = 'm',
                    Loadout = new PedLoadout(),
                    IsArrested = false
                },
                Brief = new CharacterBrief()
            };
            //CharacterMenu = new CharacterMenu();
        }

        public override async Task Execute(object[] args, CancellationToken token)
        {
            Screen.Fading.FadeOut(500);
            Screen.LoadingPrompt.Show("Loading Character Creator");
            InteriorId = API.GetInteriorAtCoords(402.668f, -1003.000f, -98.004f);
            InitialCamera = World.CreateCamera(new Vector3(402.668f, -1002.000f, -98.504f), new Vector3(0, 0, 0), 50f);
            MainCamera = World.CreateCamera(new Vector3(402.668f, -1000.000f, -98.504f), new Vector3(0, 0, 0), 50f);
            CloseUpCamera = World.CreateCamera(new Vector3(402.800f, -998.500f, -98.304f), new Vector3(0, 0, 0), 50f);
            PhotoCamera = World.CreateCamera(new Vector3(402.800f, -997.000f, -98.404f), new Vector3(0, 0, 0), 50f);

            SetScriptStage(0);

            while (!token.IsCancellationRequested)
            {
                API.HideHudAndRadarThisFrame();
                switch (ScriptStage)
                {
                    case 0:
                        if (API.IsInteriorReady(InteriorId) && !API.IsPlayerSwitchInProgress() && !API.IsPedFalling(Game.PlayerPed.Handle))
                            SetScriptStage(1);
                        break;
                    case 1:
                        if (API.GetEntityAnimCurrentTime(Game.PlayerPed.Handle, "mp_character_creation@customise@male_a", "intro") >= 0.9f)
                            SetScriptStage(2);
                        break;
                    case 2:
                        if (Game.IsControlJustPressed(0, Control.Context))
                            SetScriptStage(3);
                        else if (Game.IsControlJustPressed(0, Control.PhoneUp))
                            SetScriptStage(5);
                        else if (Game.IsControlJustPressed(0, Control.FrontendAccept))
                            SetScriptStage(4);
                        break;
                    case 3:
                        if (Game.IsControlJustPressed(0, Control.FrontendCancel))
                            SetScriptStage(2);
                        break;
                    case 4:
                        if (Game.IsControlJustPressed(0, Control.FrontendCancel))
                            SetScriptStage(2);
                        break;
                    case 5:
                        if (Game.IsControlJustPressed(0, Control.FrontendCancel))
                            SetScriptStage(2);
                        break;
                }
                await BaseScript.Delay(0);
            }



        }

        private void SetScriptStage(int v)
        {
            ScriptStage = v;
            OnScriptStageChanged();
        }

        public Camera InitialCamera { get; set; }
        public Camera MainCamera { get; set; }
        public Camera CloseUpCamera { get; set; }
        public Camera PhotoCamera { get; set; }

        public Vector3 SpawnLocation { get; set; }
        public CharacterPed CharacterPed { get; set; }
        public CharacterStats CharacterStats { get; set; }
        public int InteriorId { get; set; }

        private void OnScriptStageChanged()
        {
            switch (ScriptStage)
            {
                case 0:
                    API.FreezeEntityPosition(Game.PlayerPed.Handle, true);
                    Game.PlayerPed.IsInvincible = true;
                    Game.PlayerPed.Position = SpawnLocation;
                    Game.PlayerPed.Heading = 90f;
                    World.RenderingCamera = InitialCamera;
                    API.FreezeEntityPosition(Game.PlayerPed.Handle, false);
                    break;
                case 1:
                    Screen.Fading.FadeIn(500);
                    Screen.LoadingPrompt.Hide();
                    if (World.RenderingCamera.Handle != MainCamera.Handle)
                        World.RenderingCamera.InterpTo(MainCamera, 10000, true, true);
                    Game.PlayerPed.Task.PlayAnimation("mp_character_creation@customise@male_a", "intro"); break;
                case 2:
                    if (World.RenderingCamera.Handle != MainCamera.Handle)
                        World.RenderingCamera.InterpTo(MainCamera, 500, true, true);
                    Game.PlayerPed.Task.PlayAnimation("mp_character_creation@customise@male_a", "loop", -1, -1, AnimationFlags.Loop);
                    break;
                case 3:
                    if (World.RenderingCamera.Handle != CloseUpCamera.Handle)
                        World.RenderingCamera.InterpTo(CloseUpCamera, 500, true, true);
                    Game.PlayerPed.Task.PlayAnimation("mp_character_creation@customise@male_a", "loop", -1, -1, AnimationFlags.Loop);
                    break;
                case 4:
                    if (World.RenderingCamera.Handle != MainCamera.Handle)
                        World.RenderingCamera.InterpTo(MainCamera, 500, true, true);
                    Game.PlayerPed.Task.PlayAnimation("mp_character_creation@customise@male_a", "drop_loop", -1, -1, AnimationFlags.Loop);
                    break;
                case 5:
                    if (World.RenderingCamera.Handle != PhotoCamera.Handle)
                        World.RenderingCamera.InterpTo(PhotoCamera, 500, true, true);
                    Game.PlayerPed.Task.PlayAnimation("mp_character_creation@customise@male_a", "outro_loop", -1, -1, AnimationFlags.Loop);
                    break;
            }
        }


        private readonly Character _character;
        private readonly PedOutfit _characterPedOutfitF;
        private readonly PedOutfit _characterPedOutfitM;
        private readonly CharacterStats _characterStats;
         
       // public CharacterMenu CharacterMenu { get; set; }
        public Dictionary<string, int> Parents { get; }
        public int ScriptStage { get; private set; }


        public void Enter()
        {
            RefreshCharacter('m');
           // CharacterMenu.OpenMenu();
        }

        public void RefreshCharacter(char gender)
        {
            _character.Ped.Gender = gender;
            _character.Ped.Outfit = gender == 'm' ? _characterPedOutfitM : _characterPedOutfitF;
          //  ConnectionController.LoadCharacter(_character);
            //OnGenderChange?.Invoke(gender);
        }

        public void UpdateHeritage(Heritage heritage)
        {
            _character.Ped.Looks.Mother = Parents[heritage.Mother];
            _character.Ped.Looks.Father = Parents[heritage.Father];
            _character.Ped.Looks.Resemblence = heritage.Resemblance * 0.1f;
            _character.Ped.Looks.SkinTone = heritage.Skin * 0.1f;
           // ConnectionController.LoadLooks(_character.Ped.Looks);
        }

        public void UpdateAppearance(Appearance appearance)
        {
            //_character.Ped.Outfit.Components[2].ComponentIndex = DataController.ShopData.Hairstyles
            //    .First(i => i.Name == appearance.Hair).Index;
           // ConnectionController.LoadOutfit(_character.Ped.Outfit);
        }
    }
}