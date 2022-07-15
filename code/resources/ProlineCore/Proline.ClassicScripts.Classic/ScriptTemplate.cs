using CitizenFX.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.SClassic
{
    public class ScriptTemplate
    {

        public async Task Execute(object[] args, CancellationToken token)
        {

            while (!token.IsCancellationRequested)
            {

                await BaseScript.Delay(0);
            }
        }
    }
}
