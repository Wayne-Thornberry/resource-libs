using Proline.ClassicOnline.MData.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.MData.Server.Internal
{
    internal class UploadQueue : Queue<Save>
    {
        private static UploadQueue _instance;

        public static UploadQueue GetInstance()
        {
            if (_instance == null)
                _instance = new UploadQueue();
            return _instance;
        }
    }
}
