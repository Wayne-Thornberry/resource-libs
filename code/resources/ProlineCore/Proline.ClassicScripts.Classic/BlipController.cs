using CitizenFX.Core;
using Proline.ClassicOnline.GCharacter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.SClassic
{
    public class BlipController
    {

        public async Task Execute(object[] args, CancellationToken token)
        {

            while (!token.IsCancellationRequested)
            {
                if (CharacterGlobals.Character != null)
                {
                    if (Game.PlayerPed.IsInVehicle())
                    {
                        var currentVehicle = Game.PlayerPed.CurrentVehicle;
                        var personalVehicle = CharacterGlobals.Character.PersonalVehicle;
                        if (personalVehicle != null)
                        {
                            if (currentVehicle == personalVehicle)
                            {
                                foreach (var blip in currentVehicle.AttachedBlips)
                                {
                                    blip.Alpha = 0;
                                }
                            }
                        }
                    }
                    else
                    { 
                        var personalVehicle = CharacterGlobals.Character.PersonalVehicle;
                        if (personalVehicle != null)
                        {
                            foreach (var blip in personalVehicle.AttachedBlips)
                            {
                                blip.Alpha = 255;
                            }
                        }
                    }
                }
                await BaseScript.Delay(0);
            }
        }
    }
}
