using Proline.Classic.Engine.Components.CScriptObjects;
using Proline.Resource.Client.Framework;

namespace Proline.Classic.Engine.Components.CWorld
{
    public class CWorld : ComponentContext
    {
        private HandleTracker _ht;

        public override void OnStart()
        {
            _ht = HandleTracker.GetInstance();
        }

       

    }
}
