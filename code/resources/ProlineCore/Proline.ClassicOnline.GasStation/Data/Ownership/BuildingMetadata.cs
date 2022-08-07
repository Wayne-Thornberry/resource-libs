using CitizenFX.Core;
using System.Collections.Generic;

namespace Proline.ClassicOnline.MWord
{
     
    internal class BuildingEntrance
    {
        public string Id { get; set; }
        public string Function { get; set; }
        public bool VehicleRestricted { get; set; }
        public Vector3 DoorPosition { get; set; }
    }

    internal class BuildingExitPoint
    {
        public string Id { get; set; }
        public float Heading { get; set; }
        public Vector3 Position { get; set; }

    }
    internal class BuildingMetadata
    {
        public string Id { get; set; } 
        public Vector2 WorldPos { get; set; }
        public List<BuildingEntrance> Entrances { get; set; }
        public List<BuildingExitPoint> ExitPoints { get; set; } 
    }
}