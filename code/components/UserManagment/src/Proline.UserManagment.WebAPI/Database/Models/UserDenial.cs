using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;



namespace Proline.CentralEngine.DBApi.Models.Central
{
    // Bans a user account which means all child player accounts tied to this user will be banned on either an instance or global level
    [Table("UserDeny")]
    public class UserDenial 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long DenyId { get; set; }
        public long DeniedByUserId { get; set; }
        public long UserId { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime BannedAt { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime ExpiresAt { get; set; }
        public string Reason { get; set; }
    }
}