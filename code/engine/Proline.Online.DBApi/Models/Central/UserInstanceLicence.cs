using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.CentralEngine.DBApi.Models.Central
{
    [Table("UserInstanceLicence")]
    public class UserInstanceLicence
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long InstanceIdentityId { get; set; }
        public string Key { get; set; }
        public long UserId { get; set; }
    }
}
