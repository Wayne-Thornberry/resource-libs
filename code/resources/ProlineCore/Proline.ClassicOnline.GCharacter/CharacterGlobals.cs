using Proline.ClassicOnline.GCharacter.Data;
using Proline.Resource.Globals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.GCharacter
{
    public static class CharacterGlobals
    {
        public static PlayerCharacter Character
        {
            get;
                //var instance = GlobalsManager.GetInstance();
                //return instance.GetGlobal<PlayerCharacter>("PlayerCharacter", false);
            set;
                //var instance = GlobalsManager.GetInstance();
                //instance.SetGlobal("PlayerCharacter", value, false);
        }
    }
}
