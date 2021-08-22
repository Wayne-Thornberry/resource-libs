using Proline.Engine;
using Proline.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Example.Components.CExample
{
    public class ExampleComponentScript : ComponentScript
    {
        public override void Update()
        {
            base.Update();
           // Debugger.LogDebug("Woah");
        }
        public override void FixedUpdate()
        {
            //Debugger.LogDebug("Woah");
        }
    }
}
