using System;
using System.Collections.Generic;

namespace Proline.ClassicOnline.MShop.Internal
{
    internal class CatalougeManager
    {
        private static CatalougeManager _instance;
        private Dictionary<string, Catalouge> _catalouges;

        public CatalougeManager()
        {
            _catalouges = new Dictionary<string, Catalouge>();
        }

        internal void Register(string catalougeName, Catalouge catalouge)
        {
            if (!_catalouges.ContainsKey(catalougeName))
                _catalouges.Add(catalougeName, catalouge);
        }

        internal Catalouge GetCatalouge(string catalougeName)
        {
            if (!_catalouges.ContainsKey(catalougeName))
                return null;
            return _catalouges[catalougeName];
        }

        internal static CatalougeManager GetInstance()
        {
            if (_instance == null)
                _instance = new CatalougeManager();
            return _instance;
        }
    }
}