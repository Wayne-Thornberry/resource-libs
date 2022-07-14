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

namespace Proline.ClassicOnline.MScripting.Internal
{
    internal class ListOfLiveScripts : List<LiveScript>
    {
        private static ListOfLiveScripts _instance;
        internal static ListOfLiveScripts GetInstance()
        {
            if (_instance == null)
                _instance = new ListOfLiveScripts();
            return _instance;
        }

    }
}
