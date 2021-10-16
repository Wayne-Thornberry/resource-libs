using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Proline.CentralEngine.DBApi.Models.Central
{
    [Table("InstanceUserAllow")]
    public class InstanceUserAllow
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long InstanceAllowId { get; set; }
        public long UserId { get; set; }
        public DateTime AllowedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
        public string Note { get; set; }
    }
}