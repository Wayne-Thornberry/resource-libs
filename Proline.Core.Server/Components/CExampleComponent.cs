using Proline.Engine;
using Proline.Framework;
using Proline.Online.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Core.Server.Components
{
    public class CExampleComponent : ComponentHandler
    {
        public override void OnComponentInitialized()
        {
            base.OnComponentInitialized();
        }

        public override void OnComponentLoad()
        {
            base.OnComponentLoad();
        }

        public override void OnComponentStart()
        {
           // EngineAccess.ExecuteComponentAPI(this, "ExampleControl", new object[] { "X", "Y", "X" });
            base.OnComponentStart();
        }

        public override void OnComponentStop()
        {
            base.OnComponentStop();
        }

        [ComponentCommand("X")]
        public void ExampleCommand()
        {

        }

        [ComponentAPI]
        public void ExampleControl(string x, string y, string z)
        {
            Debugger.LogDebug(x);
            Debugger.LogDebug(y);
            Debugger.LogDebug(z);
        }

        [ComponentAPI]
        public void PlayerAPI(string userName)
        {
            try
            { 
                var client = new OnlineClient();
                client.InsertUser(userName);
            }
            catch (Exception e)
            {
                Debugger.LogDebug("Client error, probably offline, dunno lol");
                
            }
        }
    }
}
