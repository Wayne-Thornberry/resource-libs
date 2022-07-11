using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.MScripting.Internal
{

    internal class ScriptTaskManager : Dictionary<object, Task>
    {
        private static ScriptTaskManager _instance;

        public static ScriptTaskManager GetInstance()
        {
            if (_instance == null)
                _instance = new ScriptTaskManager();
            return _instance;
        }
    }
}
