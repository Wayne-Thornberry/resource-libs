using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Common.Logging
{
    public interface ILogCallback
    {
        void WriteFormatedData(string original, string entry);
    }
}
