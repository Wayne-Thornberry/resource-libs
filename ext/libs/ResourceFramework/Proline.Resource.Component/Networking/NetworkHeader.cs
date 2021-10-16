using System;

namespace Proline.Resource.Component.Networking
{
    public class NetworkHeader
    {
        public string Guid { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateRecived { get; set; }
        public DateTime DateSent { get; set; }
        public int PlayerId { get; internal set; }
    }
}
