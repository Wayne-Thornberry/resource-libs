using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace Proline.CentralEngine.DBApi.Models.Central
{
    [Table("PlayerIndentity")]
    public class LinkedIdentity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IdentityId { get; set; }
        public long PlayerId { get; set; }
        public int IdentityTypeId { get; set; }
        // if any of the identifiers match, then link that player with the user
        public long UserId { get; set; }
        public string Identifier { get; set; }
    }
}