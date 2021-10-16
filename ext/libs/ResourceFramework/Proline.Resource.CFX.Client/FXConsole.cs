using CitizenFX.Core;
using Proline.Resource.Common;
using Proline.Resource.Common.CFX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Resource.CFX
{
    public class FXConsole : IFXConsole
    {
        public void WriteLine(object data)
        {
            Debug.WriteLine(data.ToString());
        }

        public void Write(object data)
        {
            Debug.Write(data.ToString());
        }
    }
}
