using CitizenFX.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProlineCore.Internal
{
    internal class DownloadQueue : Queue<Player>
    {
        private static DownloadQueue _instance;

        public static DownloadQueue GetInstance()
        {
            if (_instance == null)
                _instance = new DownloadQueue();
            return _instance;
        }
    }
}
