using CitizenFX.Core;
using Proline.ClassicOnline.MData.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.MData.Server.Internal
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
