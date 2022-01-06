using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace Proline.CentralEngine.DBApi.Models.Central
{
    [Table("PlayerIndentityType")]
    public class PlayerIndentityType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IdentityTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}