using Proline.Resource.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.MScripting.Internal
{
    internal class ScriptingConfigSection
    {
        internal static ScriptingConfigSection ModuleConfig => Configuration.GetSection<ScriptingConfigSection>("scriptingConfigSection");
        public List<string> LevelScriptAssemblies { get; set; }
    }
}
