using CitizenFX.Core;
using System.Collections.Generic;

namespace Proline.ClassicOnline.MWord
{
    internal class InteriorEntryPoints
    { 
        public float Heading { get; set; }
        public Vector3 Position { get; set; }
    }

    internal class InteriorAccessPoint
    {
        public string Id { get; set; }
        public Vector3 DoorPosition { get; set; }
        public string Tag { get; internal set; }
        public InteriorEntryPoints OnFoot { get; set; }
    }
    internal class InteriorMetadata
    {
        public string Id { get; set; }
        public Vector3 WorldPos { get; set; }
        public List<InteriorAccessPoint> AccessPoints { get; set; }
    }
}