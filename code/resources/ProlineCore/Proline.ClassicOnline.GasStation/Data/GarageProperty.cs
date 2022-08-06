using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CitizenFX.Core;

namespace Proline.ClassicOnline.MWord
{

    internal class InteriorEntryPoints
    { 
        public string Id { get; set; }
        public float Heading { get; set; }
        public Vector3 Position { get; set; }
    }

    internal class InteriorExit
    {
        public string Id { get; set; } 
        public Vector3 DoorPosition { get; set; }
    }

    internal class Interior
    {
        public string Id { get; set; }
        public List<InteriorEntryPoints> EntrancePoints { get; set; }
        public List<InteriorExit> Exits { get; set; }
    }

    internal class PropertyEntrance
    { 
        public string Id { get; set; } 
        public bool VehicleRestricted { get; set; }
        public Vector3 DoorPosition { get; set; }
    }

    internal class PropertyExitPoint
    {
        public string Id { get; set; }
        public float Heading { get; set; }
        public Vector3 Position { get; set; }

    }

    internal abstract class PropertyBuilding<T> : IPropertyBuilding
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public List<PropertyEntrance> Entrances { get; set; }
        public List<PropertyExitPoint> ExitPoints { get; set; }
        public List<T> Properties { get; set; }
    }

    internal class ApartmentBuilding : PropertyBuilding<ApartmentProperty>
    { 
    }

    internal abstract class Property
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public int Type { get; set; }
        public string Interior { get; set; }
        public Dictionary<string, string> LinkedEntrances { get; set; }
        public Dictionary<string, string> LinkedExits { get; set; }
    }

    internal class GarageLayout
    { 
        public List<GarageSlot> VehicleSlots { get; set; }
    }

    internal class ApartmentPropertyGarage
    {
        public string GarageInteriorId { get; set; }
        public string Layout { get; set; }
        public int VehicleCap { get; set; }
        public Dictionary<string,string> LinkedEntrances { get; set; }
        public Dictionary<string, string> LinkedExits { get; set; }
    }

    internal class ApartmentProperty : Property
    {
        public bool HasGarage { get; set; }
        public string Garage { get; set; }
    }

    internal class GarageBuilding : PropertyBuilding<GarageProperty>
    {

    }

    internal class GarageProperty : Property
    {
        public string Layout { get; set; }
        public int VehicleCap { get; set; } 

    }
}