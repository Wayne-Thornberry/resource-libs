using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace Proline.Component.UserManagment.Web.Service.Database.Models
{
    [Table("SaveFile")]
    public class SaveFile
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long FileId { get; set; }
        public long PlayerId { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime InsertedAt { get; set; }
        public string Data { get; set; }
    }
}
