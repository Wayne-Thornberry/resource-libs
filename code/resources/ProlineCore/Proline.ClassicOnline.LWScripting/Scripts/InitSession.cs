using Proline.ClassicOnline.MScripting.Events;
using Proline.ClassicOnline.MScripting.Internal;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.MScripting.Client.Scripts
{
    public class InitSession
    {
        public async Task Execute()
        {   
            MScriptingAPI.StartNewScript("Main"); 
        }
    }
}
