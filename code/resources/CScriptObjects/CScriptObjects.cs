using Proline.Classic.Engine.Internal;
using Proline.Game;
using Proline.Game.Component;

namespace Proline.Classic.Engine.Components.CScriptObjects
{
    public class CScriptObjects : ComponentHandler
    {
        private TrackedObjectsManager _tom;


        protected override void OnInitialized()
        {
            //var data = Resources.LoadFile(API.GetCurrentResourceName(), "data/scriptobjects.json");
            ////LogDebug(data);
            //var scriptObjects = JsonConvert.DeserializeObject<ScriptObj>(data);

            //ScriptObjectsManager.AddScriptObjectPairs(scriptObjects.scriptObjectPairs);  
        }

        //[ComponentEvent("entityTracked")]
        //public void EntityTracked(int handle)
        //{

        //    var hash = API.GetEntityModel(handle);
        //    var item = ScriptObjectsManager.GetScriptObjectPair(hash);
        //    if (item == null) return;
        //    if (hash != 0 && (hash == item.Hash))
        //    {
        //        _tom.Add(handle, item);
        //    }
        //    //LogDebug(handle);
        //}
        //[ComponentEvent("entityUntracked")]
        //public void EntityUntracked(int handle)
        //{
        //    _tom.Remove(handle);
        //    //LogDebug(handle); 
        //}

    }
}