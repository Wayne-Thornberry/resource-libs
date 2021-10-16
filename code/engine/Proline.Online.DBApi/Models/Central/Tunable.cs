using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Proline.CentralEngine.DBApi.Models.Central
{
    public class Tunable
    {
        public long TunableId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}