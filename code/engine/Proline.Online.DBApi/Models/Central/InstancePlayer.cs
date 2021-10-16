using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.CentralEngine.DBApi.Models.Central
{
    [Table("InstancePlayer")]
    public class InstancePlayer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long InstancePlayerId { get; set; }
        public long PlayerId { get; set; }
        public long InstanceId { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime LastSeenAt { get; set; }
    }
}
