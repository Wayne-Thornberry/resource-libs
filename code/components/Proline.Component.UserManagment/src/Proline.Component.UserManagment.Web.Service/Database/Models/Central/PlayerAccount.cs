using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace Proline.CentralEngine.DBApi.Models.Central
{
    [Table("PlayerAccount")]
    public class PlayerAccount
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long PlayerId { get; set; }
        public string Name { get; set; }
        public int Priority { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime RegisteredAt { get; set; }
    }
}