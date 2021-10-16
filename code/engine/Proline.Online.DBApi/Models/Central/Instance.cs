using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace Proline.CentralEngine.DBApi.Models.Central
{
    [Table("Instance")]
    public class Instance
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long InstanceId { get; set; }
        public string Name { get; set; } // Can be whatever a person wants
        public long InstanceIdentityId{ get; set; }
        public string Type { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime LastSeenOnlineAt { get; set; }
        public string IpAddress { get; set; }  
        public bool IsWhitelisted { get; set; }
        public int MaxPlayers { get; set; }
    }
}