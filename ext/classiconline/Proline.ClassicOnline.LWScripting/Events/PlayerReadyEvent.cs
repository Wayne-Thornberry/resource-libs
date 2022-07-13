using Proline.ClassicOnline.MScripting.Config;
using Proline.ClassicOnline.MScripting.Internal;
using Proline.Resource.Eventing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.MScripting
{
    internal partial class PlayerReadyEvent : LoudEvent
    {
        public PlayerReadyEvent() : base(PLAYERREADYHANDLER)
        {
        }

        private static PlayerReadyEvent _event;
        public const string PLAYERREADYHANDLER = "PlayerReadyHandler";

        public static void SubscribeEvent()
        {
            if (_event == null)
            {
                _event = new PlayerReadyEvent();
                _event.Subscribe();
            }
        }

        public static void UnsubscribeEvent()
        {
            if (_event != null)
            {
                _event.Unsubscribe();
                _event = null;
            }
        }


        public static void InvokeEvent(string username)
        {
            _event.Invoke(username);
        }

        protected override object OnEventTriggered(params object[] args)
        {
            var lsAssembly = ScriptingConfigSection.ModuleConfig;
            Console.WriteLine("Retrived config section");
            var _lwScriptManager = ScriptTypeLibrary.GetInstance();

            if (lsAssembly != null)
            {
                Console.WriteLine($"Loading level scripts. from {lsAssembly.LevelScriptAssemblies.Count()} assemblies");
                foreach (var item in lsAssembly.LevelScriptAssemblies)
                {
                    _lwScriptManager.ProcessAssembly(item);
                }
                ScriptTypeLibrary.HasLoadedScripts = true;
            }


            MScriptingAPI.StartNewScript("Main");
            return null;
        }
    }
}
