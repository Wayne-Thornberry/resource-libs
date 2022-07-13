using CitizenFX.Core;
using Proline.Resource.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Console = Proline.Resource.Console;

namespace Proline.ClassicOnline.MScripting
{
    internal class ScriptManager : List<LiveScript>
    {
        private static ScriptManager _instance;
        internal static ScriptManager GetInstance()
        {
            if (_instance == null)
                _instance = new ScriptManager();
            return _instance;
        } 
         
    }
}
