using CDebugActions;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using CShopCatalogue.Internal;
using Newtonsoft.Json;

using Proline.Resource.Framework;
using Proline.Resource.IO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CShopCatalogue.Commands
{
    public class DoOutfitThingCommand : ResourceCommand
    {
        public DoOutfitThingCommand() : base("DoOutfitThing")
        {
        }

        protected override void OnCommandExecute(params object[] args)
        {
            var api = new CDebugActionsAPI();
            try
            {
                var data2 = ResourceFile.Load("data/mp_m_ped_clothing.json");
                var x = JsonConvert.DeserializeObject<List<ClothingItem>>(data2.Load());
                var y = x.FirstOrDefault();


                for (int i = 0; i < 12; i++)
                {
                    API.SetPedComponentVariation(Game.PlayerPed.Handle, i, y.locate, y.textureIndex, 0);
                }
                api.LogDebug(API.GetLabelText(y.textLabel));
                if (y.forcedComponents == null)
                    throw new Exception("Forced components is not set");
                foreach (var item in y.forcedComponents.Item)
                {
                    var z = x.FirstOrDefault(e => e.uniqueNameHash.Equals(item.nameHash));
                    if (z == null) continue;
                    API.SetPedComponentVariation(Game.PlayerPed.Handle, item.enumValue, z.locate, z.textureIndex, 0);
                    api.LogDebug(API.GetLabelText(z.textLabel));
                }
            }
            catch (Exception e)
            {
                api.LogError(e);
            }
        }
    }
}
