using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;



namespace Proline.CentralEngine.DBApi.Models.Central
{
    public class UserGroup
    {
        // 0 Admin N/A true
        // 1 Moderator N/A true
        // 2 Basic M/A false
        public long GroupId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool APIAccess { get; set; }
    }
}