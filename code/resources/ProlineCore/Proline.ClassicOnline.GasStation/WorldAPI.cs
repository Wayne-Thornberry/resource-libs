using CitizenFX.Core;
using Proline.ClassicOnline.MDebug;
using Proline.ClassicOnline.MWord;
using Proline.ClassicOnline.MWorld.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.MWorld
{
    public static partial class WorldAPI
    {
        public static void PlaceVehicleInGarageSlot(string garage, int index, Entity vehicle)
        {
            //try
            //{
            //    var manager = InteriorManager.GetInstance();
            //    var interior = (GarageProperty) manager.GetInterior(garage);
            //    if(interior != null)
            //    {
            //        MDebugAPI.LogDebug(interior.VehicleSlots.Count());
            //        MDebugAPI.LogDebug(index);
            //        var slot = interior.VehicleSlots[index];
            //        if (slot == null)
            //            throw new Exception($"Slot not found {interior.Title} {interior.Type}");
            //        vehicle.Position = slot.Position;
            //        vehicle.Heading = slot.Heading;
            //    }
            //}
            //catch (Exception e)
            //{
            //    MDebugAPI.LogError(e);
            //}
        }


        public static void DrawMarker()
        {

        }

        public static Vector3 GetBuildingWorldPos(object neariestBulding)
        {
            throw new NotImplementedException();
        }
    }
}
