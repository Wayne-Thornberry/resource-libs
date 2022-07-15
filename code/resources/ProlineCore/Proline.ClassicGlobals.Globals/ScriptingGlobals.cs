using Proline.Resource.Globals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.GScripting
{
    public static class ScriptingGlobals
    {
        public static float Testing
        {
            get
            {
                var instance = GlobalsManager.GetInstance();
                return instance.GetGlobal<float>("Testing", false);
            }
            set
            {
                var instance = GlobalsManager.GetInstance();
                instance.SetGlobal("Testing", value, false);
            }
        }
    }
}
