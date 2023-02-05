using CitizenFX.Core.Native;
using System.Collections.Generic;

namespace CPoolObjects.Internal
{
    internal class PickupHandleFinder : PoolHandleFinder
    {
        protected override int FindFirstType()
        {
            return API.FindFirstPickup(ref entHandle);
        }

        protected override void FindLastType()
        {
            API.EndFindPickup(handle);
        }

        protected override bool FindNextType()
        {
            return API.FindNextPickup(handle, ref entHandle);
        }
    }

    internal class VehicleHandleFinder : PoolHandleFinder
    {
        protected override int FindFirstType()
        {
            return API.FindFirstVehicle(ref entHandle);
        }

        protected override void FindLastType()
        {
            API.EndFindVehicle(handle);
        }

        protected override bool FindNextType()
        {
            return API.FindNextVehicle(handle, ref entHandle);
        }
    }

    internal class PedHandleFinder : PoolHandleFinder
    {
        protected override int FindFirstType()
        {
            return API.FindFirstPed(ref entHandle);
        }

        protected override void FindLastType()
        {
            API.EndFindPed(handle);
        }

        protected override bool FindNextType()
        {
            return API.FindNextPed(handle, ref entHandle);
        }
    }


    internal class ObjHandleFinder : PoolHandleFinder
    {
        protected override int FindFirstType()
        {
            return API.FindFirstObject(ref entHandle);
        }

        protected override void FindLastType()
        {
            API.EndFindObject(handle);
        }

        protected override bool FindNextType()
        {
            return API.FindNextObject(handle, ref entHandle);
        }
    }

    internal abstract class PoolHandleFinder
    {
        List<int> array = new List<int>();
        protected int handle;
        protected int entHandle;
        protected bool canFindOneMore;
        private bool foundFirst;
        private bool foundLast;
        private int stage = 0;

        protected abstract int FindFirstType();
        protected abstract void FindLastType();
        protected abstract bool FindNextType();

        private int FindFirst()
        {
            entHandle = -1;
            handle = FindFirstType();
            array.Add(entHandle);
            stage = 1;
            return handle;
        }

        private int FindLast()
        {
            FindLastType();
            stage = 3;
            return handle;
        }

        public int[] GetResult()
        {
            return array.ToArray();
        }

        private int FindNext()
        {
            canFindOneMore = FindNextType();
            array.Add(entHandle);
            var handle = entHandle;
            entHandle = -1;
            if (!canFindOneMore)
                stage = 2;
            return handle;
        }

        public bool FindNextProp(out int handle)
        {
            switch (stage)
            {
                case 0:
                    handle = FindFirst();
                    break;
                case 1:
                    handle = FindNext();
                    break;
                case 2:
                    handle = FindLast();
                    break;
                default:
                    handle = 0; break;
            }
            return stage < 3;
        }
    }
}
