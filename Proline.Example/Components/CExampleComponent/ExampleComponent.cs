
using Proline.Engine;
using Proline.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Example.Components.CExample
{
    public class ExampleComponent : EngineComponent
    {
        [SyncedProperty]
        public int SyncedProperty { get; set; }

        protected override void OnInitialize()
        {
            TriggerComponentEvent("X", 1);
            base.OnInitialized();
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }

        protected override void OnLoad()
        {
            base.OnLoad();
        }

        protected override void OnStart()
        {
            TriggerComponentEvent("ExampleEvent");
            base.OnStart();
        }

        protected override void OnUpdate()
        {
            base.OnStart();
        }

        protected override void OnFixedUpdate()
        {
            base.OnStart();
        }

        protected override void OnStop()
        {
            base.OnStop();
        }


        [ComponentEvent("")]
        public void ExampleEvent()
        {
            return;
        }

        [Client]
        [ComponentAPI]
        public int ExampleAPI()
        {
            return 1;
        }

        [Client]
        [ComponentAPI]
        [Debug]
        public int ExampleAPIDebug()
        {
            return 1;
        }

        [Server]
        [ComponentAPI]
        public int ExampleAPI2()
        {
            return 1;
        }

        [Client]
        [ComponentCommand("X")]
        public void ExampleCommand()
        {

        }

        [Server]
        [ComponentCommand("X2")]
        public void ExampleCommand2()
        {

        }
    }
}
