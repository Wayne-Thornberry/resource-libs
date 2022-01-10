using System.Collections.Generic;

namespace Proline.Classic.Engine.Components.CScriptObjects
{
    public class ScriptObject
    { 
        public int Handle { get; set; }
        public List<ScriptObjectData> Data { get; set; }
        public int State { get; internal set; }
    }
}
