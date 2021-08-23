using CitizenFX.Core;
using CitizenFX.Core.Native;
using CitizenFX.Core.UI;
using Proline.Core.Client;
using Proline.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Proline.Core.Client.Components.CDebugInterface
{
    public class DebugInterfaceScript : ComponentScript
    {
        private List<int> _handles;
        
        public override void Update()
        {
            API.SetTextFont(0);
            API.SetTextProportional(true);
            API.SetTextScale(0.0f, 0.3f);
            API.SetTextColour(255, 255, 255, 255);
            //API.SetTextDropshadow(0, 0, 0, 0, 255);
            //API.SetTextEdge(1, 0, 0, 0, 255);
            API.SetTextDropShadow();
            API.SetTextOutline();
            API.SetTextEntry("STRING");
            API.AddTextComponentString(Game.PlayerPed.Position.ToString() + "H:" + Game.PlayerPed.Heading + "\n"
                + Game.PlayerPed.Model.Hash + "\n"
                + Game.PlayerPed.Health + "\n"
                + Game.PlayerPed.Handle + "\n");
            API.DrawText(0.005f, 0.05f);
             

            foreach (var handle in _handles)
            {
                var entity = Entity.FromHandle(handle);
                if (entity == null) continue;
                if (!API.IsEntityVisible(entity.Handle) || World.GetDistance(entity.Position, Game.PlayerPed.Position) > 10f) continue;
                var pos = entity.Model.GetDimensions();
                var d = entity.Position + new Vector3(0, 0, (entity.Model.GetDimensions().Z * 0.8f));
                var x = $"{entity.Handle}\n" +
                    $"{entity.Model.Hash}\n" +
                    $"{Math.Round(entity.Heading, 2)}\n" +
                    $"{entity.Health}\n"; 
                ExampleAPI.DrawEntityBoundingBox(entity.Handle, 125, 125, 125, 100);
                ExampleAPI.DrawDebugText3D(x, d, 3f, 0); 
            }
        }

        public override void OnEngineEvent(string eventName, params object[] args)
        { 
            if (((bool)args[0]))
            {
                _handles.Add((int)args[1]);
                //Debugger.LogDebug((int)args[0]);
            }
            else
            {
                _handles.Remove((int)args[1]);
                //Debugger.LogDebug((int)args[0]);
            }
        }



        public override void Start()
        {
            _handles = new List<int>();
        }
       
    }
}
