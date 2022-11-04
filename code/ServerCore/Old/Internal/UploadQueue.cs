using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProlineCore.Internal
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
