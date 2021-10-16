using System.Collections.Generic;
using CitizenFX.Core;
using Proline.Game;
using Proline.Game.Component;

namespace Proline.Classic.Engine.Components.CGasStation
{

    public class CGasStations : ComponentHandler
    { 

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }

        protected override void OnLoad()
        {
        }

        private List<Blip> _blips = new List<Blip>();
        private GasStation[] _x = new GasStation[0];

    }
}
