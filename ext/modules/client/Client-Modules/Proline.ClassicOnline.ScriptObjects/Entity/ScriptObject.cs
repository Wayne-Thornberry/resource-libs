using Proline.ClassicOnline.MBrain.Data;
using System.Collections.Generic;

namespace Proline.ClassicOnline.MBrain.Entity
{
    public class ScriptObject
    {
        public int Handle { get; set; }
        public List<ScriptObjectData> Data { get; set; }
        public int State { get; internal set; }
    }
}
