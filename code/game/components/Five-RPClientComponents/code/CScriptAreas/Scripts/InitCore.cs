using CDebugActions;
using CScriptAreas.Data;
using CScriptAreas.Entity;
using Newtonsoft.Json;

using Proline.Resource.IO;
using System.Threading.Tasks;

namespace CScriptAreas.Scripts
{
    public class InitCore
    {

        public async Task Execute()
        {
            var instance = ScriptPositionManager.GetInstance();
            var data = ResourceFile.Load("data/brain/script_pos.json");
            var api = new CDebugActionsAPI();
            api.LogDebug(data);
            var scriptPosition = JsonConvert.DeserializeObject<ScriptPositions>(data.Load());
            instance.AddScriptPositionPairs(scriptPosition.scriptPositionPairs);
            PosBlacklist.Create();
        }
    }
}
