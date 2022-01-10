
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Proline.CentralEngine.DBApi.Models.Central
{
    [Table("InstanceUserDeny")]
    public class InstanceUserDeny
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long InstanceDenyId { get; set; }
        public long UserId { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime BannedAt { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime ExpiresAt { get; set; }
        public string Reason { get; set;}
    }
}