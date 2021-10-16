using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proline.CentralEngine.DBApi.ProlineAudit
{
    public class LogEntry
    {
        public long EntryId { get; set; }
        public string EntryValue { get; set; }
        public long InstanceId { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}