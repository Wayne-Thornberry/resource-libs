using CitizenFX.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Engine.Debugging
{
    public class DebugConsole : IDebugConsole
    {
        private static List<string> _history = new List<string>();

        public void Write(object data, bool replicate)
        {
            RecordWrite(data);
            Debug.WriteLine(data.ToString());
        }

        public void WriteLine(object data, bool replicate)
        {
            RecordWrite(data);
            Debug.WriteLine(data.ToString());
        }

        private static void RecordWrite(object data)
        {
            var d = data.ToString();
            _history.Add(d);
        }
    }
}
