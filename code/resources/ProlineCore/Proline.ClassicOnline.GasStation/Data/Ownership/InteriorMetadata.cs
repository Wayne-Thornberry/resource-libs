using CitizenFX.Core;
using System.Collections.Generic;

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
    internal class InteriorMetadata
    {
        public string Id { get; set; }
        public Vector3 WorldPos { get; set; }
        public List<InteriorEntryPoints> EntrancePoints { get; set; }
        public List<InteriorExit> Exits { get; set; }
    }
}