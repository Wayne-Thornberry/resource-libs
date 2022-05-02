using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace Proline.CentralEngine.DBApi.Models.Central
{
    [Table("UserAllow")]
    public class UserAllow
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long AllowId { get; set; }
        public long UserId { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime AllowedAt { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime ExpiresAt { get; set; }
        public string Note { get; set; }
    }
}