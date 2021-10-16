using Proline.Classic.Engine.Components.CScriptObjects;
using Proline.Classic.Engine.Internal;
using Proline.Game;
using Proline.Game.Component;

namespace Proline.Classic.Engine.Components.CWorld
{
    public class CWorld : ComponentHandler
    {
        private HandleTracker _ht;
        
        protected override void OnInitialized()
        {
            _ht = HandleTracker.GetInstance();
        }

       

    }
}
