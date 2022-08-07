using CitizenFX.Core;
using System.Collections.Generic;

namespace Proline.ClassicOnline.MWord
{
     
    internal class BuildingAccessPoint
    {
        public string Id { get; set; }
        public string Function { get; set; }
        public bool VehicleRestricted { get; set; }
        public Vector3 DoorPosition { get; set; }
        public BuildingExitPoint ExitOnFoot { get; set; }
        public BuildingExitPoint ExitInVehicle { get; set; }
    }

    internal class BuildingExitPoint
    { 
        public float Heading { get; set; }
        public Vector3 Position { get; set; }

    }
    internal class BuildingMetadata
    {
        public string Id { get; set; } 
        public Vector2 WorldPos { get; set; }
        public List<BuildingAccessPoint> AccessPoints { get; set; }
    }
}