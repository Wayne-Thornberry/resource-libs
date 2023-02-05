using CitizenFX.Core;
using CScriptObjs.Tasks;
using System.Threading.Tasks;

namespace CScriptObjs.Scripts
{
    public class InitSession
    {
        public async Task Execute()
        {
            var gc = new ProcessScriptObjs();
            while (true)
            {
                var task = Task.Factory.StartNew(gc.Execute);
                await BaseScript.Delay(1000);
            }
        }
    }
}
