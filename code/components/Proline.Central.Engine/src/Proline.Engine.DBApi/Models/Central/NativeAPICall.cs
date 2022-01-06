using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace Proline.CentralEngine.DBApi.Models.Central
{
    public class NativeAPICall
    {
        // 0 100 0 1 1 {"A" : true, "B" : 0}, true, 0, 00/00/1991, 00/00/1991, 00/00/1991
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long CallId { get; set; }
        public long NativeId { get; set; }
        public long CallerId { get; set; }
        public long TargetInstanceId { get; set; }
        public long TargetPlayerId { get; set; }
        public string ArgValues { get; set; }
        public string ReturnResult { get; set; }
        public int State { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime CallCreated { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime CallUpdated { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime CallCompleted { get; set; }
    }
}