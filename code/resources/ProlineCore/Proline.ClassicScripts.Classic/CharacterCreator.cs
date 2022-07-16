using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using CitizenFX.Core.UI;
using Newtonsoft.Json;
using Proline.ClassicOnline.GCharacter.Data;
using Proline.ClassicOnline.MGame;
using Proline.ClassicOnline.MScripting;

namespace Proline.ClassicOnline.SClassic
{
    public class CharacterCreator
    {
        public async Task Execute(object[] args, CancellationToken token)
        { 
            _initialCamera = World.CreateCamera(new Vector3(402.668f, -1002.000f, -98.504f), new Vector3(0, 0, 0), 50f);
            _mainCamera = World.CreateCamera(new Vector3(402.668f, -1000.000f, -98.504f), new Vector3(0, 0, 0), 50f);
            _closeUpCamera = World.CreateCamera(new Vector3(402.800f, -998.500f, -98.304f), new Vector3(0, 0, 0), 50f);
            _photoCamera = World.CreateCamera(new Vector3(402.800f, -997.000f, -98.404f), new Vector3(0, 0, 0), 50f);
            _interiorLoc = new Vector3(402.668f, -1003.000f, -98.004f);
            _spawnLocation = new Vector3(405.83f, -997.13f, -99.004f);
            _interiorId = API.GetInteriorAtCoords(_interiorLoc.X, _interiorLoc.Y, _interiorLoc.Z);
            _selectedOutfit = new CharacterOutfit();// Default outfit from json 
            ScriptStage = 0;
            MScripting.MScriptingAPI.StartNewScript("LoadDefaultOnline");
            while (MScripting.MScriptingAPI.GetInstanceCountOfScript("LoadDefaultOnline") > 0)
            {
                await BaseScript.Delay(1);
            }

            Screen.Fading.FadeOut(500);
            while (!token.IsCancellationRequested && ScriptStage != -1)
            {
                API.HideHudAndRadarThisFrame();
                switch (ScriptStage)
                {
                    case 0:
                        if (API.IsInteriorReady(_interiorId) && !API.IsPlayerSwitchInProgress() && !API.IsPedFalling(Game.PlayerPed.Handle))
                            ScriptStage = 1;
                        break;
                    case 1:
                        if (API.GetEntityAnimCurrentTime(Game.PlayerPed.Handle, "mp_character_creation@customise@male_a", "intro") >= 0.9f)
                            ScriptStage = 2;
                        break;
                    case 2:
                        if (Game.IsControlJustPressed(0, Control.Context))
                            ScriptStage = 3;
                        else if (Game.IsControlJustPressed(0, Control.PhoneUp))
                            ScriptStage = 5;
                        else if (Game.IsControlJustPressed(0, Control.FrontendAccept))
                            ScriptStage = 4;
                        break;
                    case 3:
                        if (Game.IsControlJustPressed(0, Control.FrontendCancel))
                            ScriptStage = 2;
                        break;
                    case 4:
                        if (Game.IsControlJustPressed(0, Control.FrontendCancel))
                            ScriptStage = 2;
                        break;
                    case 5:
                        if (Game.IsControlJustPressed(0, Control.FrontendCancel))
                            ScriptStage = 2;
                        if (Game.IsControlJustPressed(0, Control.Context))
                            Finish();
                        break;
                }
                await BaseScript.Delay(0);
            }

            World.RenderingCamera = null;
            Game.PlayerPed.Task.ClearAll();

            Screen.Fading.FadeOut(500);
            Screen.LoadingPrompt.Show("Loading Classic Online...");
            //API.SwitchOutPlayer(Game.PlayerPed.Handle, 1, 1); 

            MScriptingAPI.StartNewScript("PlayerSetup");
            while (MScriptingAPI.GetInstanceCountOfScript("PlayerSetup") > 0)
            {
                await BaseScript.Delay(1);
            }

            Game.PlayerPed.Position = new Vector3(0, 0, 70);
            var id = "PlayerInfo";
            if (MData.API.DoesDataFileExist(id))
            {
                MData.API.SelectDataFile(id);
                MData.API.SetDataFileValue("PlayerPosition", JsonConvert.SerializeObject(Game.PlayerPed.Position));
            }

            MScriptingAPI.StartNewScript("SaveNow");
            while (MScriptingAPI.GetInstanceCountOfScript("SaveNow") > 0)
            {
                await BaseScript.Delay(1);
            }

            MScriptingAPI.StartNewScript("PlayerLoading");
            while (MScriptingAPI.GetInstanceCountOfScript("PlayerLoading") > 0)
            {
                await BaseScript.Delay(1);
            }
            Screen.LoadingPrompt.Hide(); 
            MScriptingAPI.StartNewScript("StartIntro");

        }

        private void Finish()
        {
            var id = "CharacterLooks";
            if (!MData.API.DoesDataFileExist(id))
            {
                var pedLooks = MGameAPI.GetPedLooks(Game.PlayerPed.Handle);
                MData.API.CreateDataFile();
                MData.API.AddDataFileValue("Mother", pedLooks.Mother);
                MData.API.AddDataFileValue("Father", pedLooks.Father);
                MData.API.AddDataFileValue("Resemblance", pedLooks.Resemblence);
                MData.API.AddDataFileValue("SkinTone", pedLooks.SkinTone);
                MData.API.SaveDataFile(id);
                MDebug.MDebugAPI.LogDebug(id + " Created and saved");
            }

            ScriptStage = -1;
        }
         

        private Camera _initialCamera;
        private Camera _mainCamera;
        private Camera _closeUpCamera;
        private Camera _photoCamera; 
        private Vector3 _spawnLocation;
        private CharacterOutfit _selectedOutfit;
        private int _interiorId;
        private int _selectedMother;
        private int _selectedFather;
        private float _resemblence;
        private float _skin;
        private Vector3 _interiorLoc; 

        private void OnScriptStageChanged()
        {
            switch (ScriptStage)
            {
                case 0:
                    API.FreezeEntityPosition(Game.PlayerPed.Handle, true);
                    Game.PlayerPed.IsInvincible = true;
                    Game.PlayerPed.Position = _spawnLocation;
                    Game.PlayerPed.Heading = 90f;
                    World.RenderingCamera = _initialCamera;
                    API.FreezeEntityPosition(Game.PlayerPed.Handle, false);
                    break;
                case 1:
                    Screen.Fading.FadeIn(500);
                    Screen.LoadingPrompt.Hide();
                    if (World.RenderingCamera.Handle != _mainCamera.Handle)
                        World.RenderingCamera.InterpTo(_mainCamera, 10000, true, true);
                    Game.PlayerPed.Task.PlayAnimation("mp_character_creation@customise@male_a", "intro"); break;
                case 2:
                    if (World.RenderingCamera.Handle != _mainCamera.Handle)
                        World.RenderingCamera.InterpTo(_mainCamera, 500, true, true);
                    Game.PlayerPed.Task.PlayAnimation("mp_character_creation@customise@male_a", "loop", -1, -1, AnimationFlags.Loop);
                    break;
                case 3:
                    if (World.RenderingCamera.Handle != _closeUpCamera.Handle)
                        World.RenderingCamera.InterpTo(_closeUpCamera, 500, true, true);
                    Game.PlayerPed.Task.PlayAnimation("mp_character_creation@customise@male_a", "loop", -1, -1, AnimationFlags.Loop);
                    break;
                case 4:
                    if (World.RenderingCamera.Handle != _mainCamera.Handle)
                        World.RenderingCamera.InterpTo(_mainCamera, 500, true, true);
                    Game.PlayerPed.Task.PlayAnimation("mp_character_creation@customise@male_a", "drop_loop", -1, -1, AnimationFlags.Loop);
                    break;
                case 5:
                    if (World.RenderingCamera.Handle != _photoCamera.Handle)
                        World.RenderingCamera.InterpTo(_photoCamera, 500, true, true);
                    Game.PlayerPed.Task.PlayAnimation("mp_character_creation@customise@male_a", "outro_loop", -1, -1, AnimationFlags.Loop);
                    break;
            }
        }


        private int _scriptStage;
        public int ScriptStage { 
            get
            {
                return _scriptStage;
            }
            set
            {
                _scriptStage = value;
                UpdateHeritage();
                OnScriptStageChanged();
            }
        }
         
        public void RefreshCharacter(char gender)
        {
            MGameAPI.SetPedGender(Game.PlayerPed.Handle, gender); 
        }

        public void UpdateHeritage()
        {
            MGameAPI.SetPedLooks(Game.PlayerPed.Handle, new CharacterLooks()
            { 
                Mother = _selectedMother,
                Father = _selectedFather,
                Resemblence = _resemblence * 0.1f,
                SkinTone = _skin * 0.1f, 
            }); 
        }
         
    }
}