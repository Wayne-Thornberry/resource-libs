using System.Collections.Generic;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.MScripting.Internal
{
    internal class ScriptInstanceManager
    {
        private static ScriptInstanceManager _instance;

        public static ScriptInstanceManager GetInstance()
        {
            if (_instance == null)
                _instance = new ScriptInstanceManager();
            return _instance;
        }

        internal void StopAllInstances()
        {
            var sm = ListOfLiveScripts.GetInstance();
            foreach (var item in sm)
            {
                item.Terminate();
            }
        }
    }
}
