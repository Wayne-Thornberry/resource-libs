using CitizenFX.Core.Native;
using Proline.ClassicOnline.GScripting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.ClassicOnline.MBrain.Entity
{
    internal static partial class HandleManager
    {
        internal static int[] EntityHandles => GetEntityHandlesByTypes(EntityType.PED, EntityType.VEHICLE, EntityType.PROP);
        internal static int[] PedHandles => GetEntityHandlesByTypes(EntityType.PED);
        internal static int[] VehicleHandles => GetEntityHandlesByTypes(EntityType.VEHICLE);
        internal static int[] PropHandles => GetEntityHandlesByTypes(EntityType.PROP);

        private static int[] GetEntityHandlesByTypes(params EntityType[] types)
        {
            var array = new List<int>();
            foreach (var type in types)
            {
                int entHandle = -1;
                int handle;
                switch (type)
                {
                    case EntityType.PROP:
                        handle = API.FindFirstObject(ref entHandle);
                        array.Add(entHandle);
                        entHandle = -1;
                        while (API.FindNextObject(handle, ref entHandle))
                        {
                            array.Add(entHandle);
                            entHandle = -1;
                        }
                        API.EndFindObject(handle);
                        break;
                    case EntityType.PED:
                        handle = API.FindFirstPed(ref entHandle);
                        array.Add(entHandle);
                        entHandle = -1;
                        while (API.FindNextPed(handle, ref entHandle))
                        {
                            array.Add(entHandle);
                            entHandle = -1;
                        }

                        API.EndFindPed(handle);
                        break;
                    case EntityType.VEHICLE:
                        handle = API.FindFirstVehicle(ref entHandle);
                        array.Add(entHandle);
                        entHandle = -1;
                        while (API.FindNextVehicle(handle, ref entHandle))
                        {
                            array.Add(entHandle);
                            entHandle = -1;
                        }

                        API.EndFindVehicle(handle);
                        break;
                }
            }
            return array.ToArray();
        }
    }
}
