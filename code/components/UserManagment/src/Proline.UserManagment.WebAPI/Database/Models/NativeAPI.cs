using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace Proline.CentralEngine.DBApi.Models.Central
{
    public class NativeAPI
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long NativeId { get; set; }
        public string Name { get; set; }
        public bool HasReturn { get; set; }
        public string ReturnType { get; set; }
        public string Hash { get; set; }
        public int ArgCount { get; set; }
        public string Args { get; set; }
        public int Type { get; set; }
        public bool HasOutArgs { get; set; }
    }
}