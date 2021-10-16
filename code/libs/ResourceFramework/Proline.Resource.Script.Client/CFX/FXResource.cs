using CitizenFX.Core.Native;
using Proline.Resource.CFX;
using Proline.Resource.Common;
using Proline.Resource.Common.CFX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proline.Resource.Script.CFX
{
    public class FXResource : CFXObject, IFXResource
    {
        private string _name;

        public FXResource(string resourceName)
        {
            _name = resourceName;
        }

        public string GetResourceName()
        {
            return _name;
        }

        public string LoadFile(string fileName)
        {
            return API.LoadResourceFile(_name, fileName);
        }
    }
}
