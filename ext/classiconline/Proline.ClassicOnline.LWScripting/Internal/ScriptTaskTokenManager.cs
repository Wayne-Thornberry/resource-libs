using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.MScripting.Internal
{
    internal class ScriptTaskTokenManager 
    {
        private static ScriptTaskTokenManager _instance;

        public static ScriptTaskTokenManager GetInstance()
        {
            if (_instance == null)
                _instance = new ScriptTaskTokenManager();
            return _instance;
        }
    }
}
