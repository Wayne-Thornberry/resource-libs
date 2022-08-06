using CitizenFX.Core;
using Proline.ClassicOnline.GCharacter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.MGame
{
    public static partial class MGameAPI
    {
        public static bool IsInPersonalVehicle()
        {
            try
            {
                if (CharacterGlobals.Character == null)
                    return false;
                return Game.PlayerPed.IsInVehicle() && Game.PlayerPed.CurrentVehicle == CharacterGlobals.Character.PersonalVehicle;
            }
            catch (Exception e)
            {
                MDebug.MDebugAPI.LogError(e);
            }
            return false;
        }
    }
}
