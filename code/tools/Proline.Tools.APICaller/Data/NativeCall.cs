using Proline.CentralEngine.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Tools.APICaller.Data
{
    public class NativeCall : INativeCall
    {
        public DateTime CallCompleted { get; set; }
        public DateTime CallCreated { get; set; }
        public long CallId { get; set; }
        public DateTime CallUpdated { get; set; }
        public bool HasCallCompleted { get; set; }
        public string InArgs { get; set; }
        public long NativeId { get; set; }
        public string OutArgs { get; set; }
        public string ReturnResult { get; set; }
    }
}
