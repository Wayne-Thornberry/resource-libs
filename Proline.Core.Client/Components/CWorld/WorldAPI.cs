using CitizenFX.Core;
using CitizenFX.Core.Native;
using Proline.Engine;
using Proline.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Core.Client.Components.CWorld
{
    public class WorldAPI : ComponentAPI
    {
        [Client]
        [ComponentAPI]
        public int TestMethod()
        {
            return 1;
        }

        [Client]
        [ComponentAPI]
        public void FindAllPeds(out int[] entities)
        {

            int entHandle = -1;
            int handle;
            List<int> handles = new List<int>();

            handle = API.FindFirstPed(ref entHandle);
            handles.Add(entHandle);
            entHandle = -1;
            while (API.FindNextPed(handle, ref entHandle))
            {
                handles.Add(entHandle);
                entHandle = -1;
            }
            API.EndFindPed(handle);
            entities = handles.ToArray();


            return;

        }

        [Client]
        [ComponentAPI]
        public void FindAllPickups(out int[] entities)
        {

            int entHandle = -1;
            int handle;
            List<int> handles = new List<int>();

            handle = API.FindFirstPickup(ref entHandle);
            handles.Add(entHandle);
            entHandle = -1;
            while (API.FindNextPickup(handle, ref entHandle))
            {
                handles.Add(entHandle);
                entHandle = -1;
            }
            API.EndFindPickup(handle);
            entities = handles.ToArray();


            //LogDebug($"Pickups Found {newEntityFound} new and {oldEntitiesDeleted} old Entities ");
            return;
        }

        [Client]
        [ComponentAPI]
        public void FindAllProps(out int[] entities)
        {

            int entHandle = -1;
            int handle;
            List<int> handles = new List<int>();

            handle = API.FindFirstObject(ref entHandle);
            handles.Add(entHandle);
            entHandle = -1;
            while (API.FindNextObject(handle, ref entHandle))
            {
                handles.Add(entHandle);
                entHandle = -1;
            }
            API.EndFindObject(handle);
            entities = handles.ToArray();

            //LogDebug($"Pickups Found {newEntityFound} new and {oldEntitiesDeleted} old Entities ");

            return;
        }

        [Client]
        [ComponentAPI]
        public void FindAllVehicles(out int[] entities)
        {

            int entHandle = -1;
            int handle;
            List<int> handles = new List<int>();

            handle = API.FindFirstVehicle(ref entHandle);
            handles.Add(handle);
            entHandle = -1;
            while (API.FindNextVehicle(handle, ref entHandle))
            {
                handles.Add(entHandle);
                entHandle = -1;
            }
            API.EndFindVehicle(handle);

            entities = handles.ToArray();


            //LogDebug($"Pickups Found {newEntityFound} new and {oldEntitiesDeleted} old Entities ");

            return;
        }

        private void SpawnEntity(EntityType type, object SpawnEntityParameters, out int handle)
        {
            handle = -1;
            //ReturnCode rc = ReturnCode.Success;
            switch (type)
            {
                case EntityType.PED:
                    handle = World.CreatePed("", new Vector3()).Result.Handle;
                    break;
                case EntityType.PROP:
                    handle = World.CreateProp("", new Vector3(), new Vector3(), true, true).Result.Handle;
                    break;
                case EntityType.VEHICLE:
                    handle = World.CreateVehicle("", new Vector3()).Result.Handle;
                    break;
                case EntityType.PICKUP:
                    handle = World.CreatePickup(PickupType.AmmoBulletMP, new Vector3(), "", 0).Result.Handle;
                    break;
            }
            //WriteLine(handle);
            return;// rc;
        }
    }
}
