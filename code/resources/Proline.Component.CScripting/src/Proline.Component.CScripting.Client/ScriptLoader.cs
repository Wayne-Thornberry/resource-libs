using Proline.CScripting.Framework;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Classic.Client.Engine.CScripting
{
    public class ScriptLoader
    {
        private ScriptLibrary _library;

        public ScriptLoader(ScriptLibrary library)
        {
            _library = library;
        }


        public ScriptInstance LoadScript(string scriptName)
        {
            var scriptType = _library.GetScriptType(scriptName);  
            if (scriptType == null)
                throw new Exception("Script not found "  + scriptName);
            var si = CreateScriptInstance(scriptType);
            return si;
        }


        private ScriptInstance CreateScriptInstance(Type type)
        {
            return (ScriptInstance) Activator.CreateInstance(type, null);
        }
    }
}
