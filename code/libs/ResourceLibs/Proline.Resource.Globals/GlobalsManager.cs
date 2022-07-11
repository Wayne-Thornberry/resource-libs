using CitizenFX.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Resource.Globals
{
    public class GlobalsManager
    {
        private static GlobalsManager _instance; 
        public StateBag GlobalProperties { get; set; }

        public static GlobalsManager GetInstance()
        {
            if (_instance == null)
                _instance = new GlobalsManager();
            return _instance;
        }
    }
}
