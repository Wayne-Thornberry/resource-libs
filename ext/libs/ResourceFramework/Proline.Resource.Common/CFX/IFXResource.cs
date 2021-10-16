using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Resource.Common.CFX
{
    public interface IFXResource : IFXWrapper
    {
        string GetResourceName();
        string LoadFile(string fileName);
    }
}
