using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace Proline.CentralEngine.DBApi.Models.Central
{
    [Table("UserAccount")]
    public class UserAccount
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long UserId { get; set; }
        public long GroupId { get; set; }
        public string Username { get; set; }
        public int Priority { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime CreatedOn { get; set; }
    }
}