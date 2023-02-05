using CitizenFX.Core;
using CScriptAreas.Tasks;
using System.Threading.Tasks;

namespace CScriptAreas.Scripts
{
    public class InitSession
    {
        public async Task Execute()
        {
            var gc = new ProcessScriptAreas();
            while (true)
            {
                var task = Task.Factory.StartNew(gc.Execute);
                await BaseScript.Delay(1000);
            }
        }
    }
}
